using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleScene;
using BattleScene.BattleLogic;
///Yod term
using UnityEngine.SceneManagement;
/*
    *** Overview of this class ***
    
    Initial
    -ให้ Object ทุกตัวใน Class มี BoxCollider2D Component (เช่น Eggplant , Carrot , Garlik , all skills , Yuak , NormalPrik , ... )
    -ใช้ Raycast เพื่อ detect object ที่ mouse คลิก ถ้าคลิกโดนจะทำการ resize ให้ใหญ่ขึ้นเพื่อบอกว่าถูกเลือก
    -เปลี่ยน state ในการเลือกตามการกด ( default -> pakSelected -> skillSelected -> enemySelected -> pressOkButton )
*/

public class PakSelection : MonoBehaviour
{

    enum InputState
    {
        DEFAULT, CHARCTER_SELECTED, SKILL_SELECTED, SKILL_SELECTED_ONE_ALLY,
        SKILL_SELECTED_ALL_ALLIANCES, SKILL_SELECTED_ALL_ENEMIES, SKILL_SELECTED_WHOLE_FIELD,
        ENEMY_SELECTED, COMFIRMED, END_TURN
    };

    private InputState currentState = InputState.END_TURN;
    private InputState nextState;
    private bool actionFinished;

    private string selectedPak = "";        // current selectd ally 
    private string selectedEnemy = "";      // current selected enemy
    private int selectedSkill = -1;         // current selected skill

    // buffer field using to check state after the player clicked some of the field-related buttons
    private bool okPressed = false, endTurnPressed = false, backPressed = false, cancelPressed = false;


    private int selectSkillBuffer = -1;     // buffer for storing user's click input

    private CharacterManager characterManager;

    [SerializeField]
    private SkillMenuUI skillMenu;

    [SerializeField]
    private Button okButton, backButton, endTurnButton, cancelButton;

    [SerializeField]
    private GameObject supportMenu;

    [SerializeField]
    private GameObject Backdrop;

    private List<string> result;

    private void OnEnable()
    {
        ActionCommandHandler.OnComplete += SetActionFinished;
    }

    private void OnDisable()
    {
        ActionCommandHandler.OnComplete -= SetActionFinished;
    }

    void Start()
    {
        Spawner spawn = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        characterManager = spawn.characters;

        // Set callback function for skill buttons
        for (int i = 0; i < skillMenu.skills.Length; ++i)
        {
            int k = i;
            skillMenu.skills[i].AddListener(() => SelectSkill(k));
        }

        // Set callback function for game button
        okButton.onClick.AddListener(() => okPressed = true);
        backButton.onClick.AddListener(() => backPressed = true);
        endTurnButton.onClick.AddListener(() => endTurnPressed = true);
        cancelButton.onClick.AddListener(() => cancelPressed = true);

        reset();
    }

    // Update is called once per frame
    void Update()
    {
        // Change state and update UI
        UpdateUI();
        currentState = nextState;

        if (backPressed)
        {
            if (currentState == InputState.ENEMY_SELECTED &&
                selectedPak.CompareTo("") != 0)
            {
                var holder = characterManager.GetCharacter(selectedPak);

                // if select character is in action set it back to action state
                if (holder.InAction)
                {
                    var pakRender = holder.character.GetComponent<PakRender>();
                    pakRender.DisplayInAction(true);
                }
                reset();
            }
            else if (currentState > InputState.DEFAULT && currentState < InputState.COMFIRMED)
                reset();
            return;
        }


        // waiting for user inputs according to current state
        switch (currentState)
        {
            case InputState.CHARCTER_SELECTED:
                // An ally character was choosed
                nextState = ChooseSkill();
                if (nextState != currentState)
                    UpdateCharacterLayer(nextState);
                break;
            case InputState.SKILL_SELECTED:
                // An skill to be add to commnad list was selected
                nextState = ChooseSkillTarget();
                if (nextState != currentState)
                    UpdateCharacterLayer(nextState);
                break;

            //TODO AOF DO

            case InputState.SKILL_SELECTED_ONE_ALLY:
                nextState = ChooseSkillTargetOneAlliance();
                break;
            //? Peem Do
            case InputState.SKILL_SELECTED_ALL_ENEMIES:
                nextState = ChooseSkillTargetAllEnemies();
                break;
            case InputState.SKILL_SELECTED_ALL_ALLIANCES:
                nextState = ChooseSkillTargetAllAlliances();
                break;
            case InputState.SKILL_SELECTED_WHOLE_FIELD:
                nextState = ChooseSkillTargetWholeField();
                break;

            //TODO -----------------------------------------------------------------

            case InputState.ENEMY_SELECTED:
                // An target for skilled was selected
                nextState = ConfirmAction();
                if (nextState == InputState.DEFAULT)
                    reset();
                break;
            case InputState.COMFIRMED:
                // Player clicked OK button
                SendCommand();
                reset();
                break;
            case InputState.END_TURN:
                // Player clicked end-turn button
                if (actionFinished)
                {
                    reset();
                }
                break;
            default:
                nextState = PlayerEndTurn();
                if (nextState != InputState.END_TURN)
                {
                    //Is adding check win condition here is fine?
                    bool isPlayerWin = BattleManager.instance.IsPlayerWin();
                    bool isPlayerLose = BattleManager.instance.IsPlayerLose();
                    bool isGameOver = isPlayerWin || isPlayerLose;
                    if (isGameOver)
                    {
                        if (isPlayerWin)
                        {
                            //do victory stuff
                            SceneManager.LoadScene("VictoryScene");
                        }
                        else
                        {
                            //do defeat stuff

                            //! Don't forget to move to victory stuff 
                            LevelManager.instance.unlockStatus[LevelManager.instance.thislevel - 1 + 1] = true;
                            //! /////////////////////////////////////
                            SceneManager.LoadScene("LoseScene");
                        }
                    }
                    else
                    {
                        nextState = chooseCharacter();
                    }
                }
                else
                {
                    characterManager.ResetAction();
                    UpdateCharacterLayer(nextState);
                }
                break;
        }

    }

    private void LateUpdate()
    {
        if (okPressed)
        {
            okPressed = false;
        }

        if (endTurnPressed)
        {
            endTurnPressed = false;
        }

        if (backPressed)
        {
            backPressed = false;
        }

        if (cancelPressed)
        {
            cancelPressed = false;
        }
        selectSkillBuffer = -1;
    }

    private InputState chooseCharacter()
    {
        // Player select ally character to use skill
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {

                if (hit.collider.CompareTag("Plant1") || hit.collider.CompareTag("Plant2")
                    || hit.collider.CompareTag("Plant3") || hit.collider.CompareTag("Chaam"))
                {
                    // find game object data with tag
                    if (!characterManager.hasCharacter(hit.collider.tag)) return InputState.DEFAULT;

                    CharacterHolder ally = characterManager.GetCharacter(hit.collider.tag);

                    if (ally != null)
                    {
                        // Select the ally
                        selectedPak = hit.collider.tag;
                        ally.Select(true);

                        // Send character to update on Skill menu
                        UpdateSkillUIImage(ally.character);

                        if (ally.InAction)
                        {
                            // Get old action data
                            ActionCommand action = BattleManager.instance.actionCommandHandler.GetAction(selectedPak);

                            // get index of the called skill in the caller pak
                            selectedSkill = action.selectedSkill;
                            skillMenu.ToggleSkillUI(selectedSkill);

                            // get game tag of the target
                            selectedEnemy = action.targets[0].gameObject.tag;
                            characterManager.SetSelect(selectedEnemy, true);

                            // Change UI from DEFAULT scene to ENEMY_SELECTED
                            skillMenu.ToggleMenu(true);
                            endTurnButton.gameObject.SetActive(false);
                            Backdrop.SetActive(true);
                            UpdateCharacterLayer(InputState.ENEMY_SELECTED);
                            action.caller.DisplayInAction(false);
                            return InputState.ENEMY_SELECTED;
                        }

                        // Add value to result
                        result.Add(hit.collider.name);

                        return InputState.CHARCTER_SELECTED;
                    }
                }
            }
        }

        return InputState.DEFAULT;
    }

    private InputState ChooseSkill()
    {

        if (selectSkillBuffer > -1)
        {
            try
            {
                selectedSkill = selectSkillBuffer;
                skillMenu.ToggleSkillUI(selectedSkill);
                result.Add(string.Format("Skill {0}", selectedSkill + 1));
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.LogError(e.Message);
                nextState = InputState.DEFAULT;
            }
            Debug.Log("Skill selected");

            //? Yod do
            PakRender selectedPakRender = GameObject.Find(selectedPak).transform.GetChild(0).GetComponent<PakRender>();
            Skill pakSkill = selectedPakRender.skill[selectedSkill];



            switch (pakSkill.ActionType)
            {
                case "TargetAllAlliances":
                    return InputState.SKILL_SELECTED_ALL_ALLIANCES; //TODO we have to for loop ทุกตัวใส่ List target.
                case "TargetOneAlliance":
                    return InputState.SKILL_SELECTED_ONE_ALLY;
                case "TargetAllEnemies":
                    return InputState.SKILL_SELECTED_ALL_ENEMIES;
                case "TargetOneEnemy":
                    return InputState.SKILL_SELECTED;
                case "TargetWholeField":
                    return InputState.SKILL_SELECTED_WHOLE_FIELD;   //TODO we have to for loop ทุกตัวใส่ List target.
                default:
                    Debug.LogError("Wrong Skill Type");
                    break;
            }



            //return InputState.SKILL_SELECTED;
        }

        // Check if player has change ally targets
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null) return InputState.CHARCTER_SELECTED;


            if ((selectedPak.CompareTo("") != 0) &&
                    (hit.collider.CompareTag("Plant1") || hit.collider.CompareTag("Plant2")
                    || hit.collider.CompareTag("Plant3") || hit.collider.CompareTag("Chaam")))
            {

                characterManager.SetSelect(selectedPak, false);
                result.Clear();

                if (hit.collider.CompareTag(selectedPak))
                {
                    selectedPak = "";
                    result.Add(hit.collider.name);
                    return InputState.DEFAULT;
                }


                selectedPak = hit.collider.tag;
                GameObject ally = hit.collider.gameObject;
                characterManager.SetSelect(selectedPak, true);

                // Send character to update on Skill menu
                UpdateSkillUIImage(ally);

                // Add value to result
                result.Add(hit.collider.name);
            }
        }
        return InputState.CHARCTER_SELECTED;
    }

    private InputState ChooseSkillTargetOneAlliance()
    {
        if ((selectedSkill > -1)
            && (selectSkillBuffer > -1))
        {

            if (selectedSkill == selectSkillBuffer)
            {
                selectedSkill = -1;
                result.RemoveAt(result.Count - 1);
                return InputState.CHARCTER_SELECTED;
            }

            result.Remove(string.Format("Skill {0}", selectedSkill + 1));
            result.Add(string.Format("Skill {0}", selectSkillBuffer + 1));
            selectedSkill = selectSkillBuffer;
            skillMenu.ToggleSkillUI(selectedSkill);

            return InputState.SKILL_SELECTED_ONE_ALLY;
        }

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null) return InputState.SKILL_SELECTED_ONE_ALLY;

            if (hit.collider.CompareTag("Plant1") || hit.collider.CompareTag("Plant2")
                    || hit.collider.CompareTag("Plant3") || hit.collider.CompareTag("Chaam"))
            {


                if (!characterManager.hasCharacter(hit.collider.tag))
                {
                    Debug.Log(hit.collider.tag + " selected");
                    return currentState;
                }

                selectedEnemy = hit.collider.tag;
                characterManager.SetSelect(selectedEnemy, true);
                result.Add(hit.collider.name);
                return InputState.ENEMY_SELECTED;
            }
        }
        return InputState.SKILL_SELECTED_ONE_ALLY;
    }


    private InputState ChooseSkillTargetAllEnemies()
    {
        if ((selectedSkill > -1)
            && (selectSkillBuffer > -1))
        {

            if (selectedSkill == selectSkillBuffer)
            {
                selectedSkill = -1;
                result.RemoveAt(result.Count - 1);
                return InputState.CHARCTER_SELECTED;
            }

            result.Remove(string.Format("Skill {0}", selectedSkill + 1));
            result.Add(string.Format("Skill {0}", selectSkillBuffer + 1));
            selectedSkill = selectSkillBuffer;
            skillMenu.ToggleSkillUI(selectedSkill);

            return InputState.SKILL_SELECTED_ALL_ENEMIES;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // if (hit.collider == null) return InputState.SKILL_SELECTED_ALL_ENEMIES;

        // if (!characterManager.hasCharacter(hit.collider.tag))
        // {
        //     Debug.Log(hit.collider.tag + " selected");
        //     return currentState;
        // }

        string selectedEnemySum = "";
        foreach (CharacterHolder e in characterManager.getTeamHolders(1))
        {
            selectedEnemy = e.character.tag;
            selectedEnemySum += e.character.tag;
            selectedEnemySum += ",";
            characterManager.SetSelect(selectedEnemy, true);
            result.Add(e.character.name);
        }
        selectedEnemy = selectedEnemySum;
        // selectedEnemy = hit.collider.tag;
        // characterManager.SetSelect(selectedEnemy, true);
        // result.Add(hit.collider.name);
        return InputState.ENEMY_SELECTED;
    }

    private InputState ChooseSkillTargetAllAlliances()
    {
        if ((selectedSkill > -1)
            && (selectSkillBuffer > -1))
        {

            if (selectedSkill == selectSkillBuffer)
            {
                selectedSkill = -1;
                result.RemoveAt(result.Count - 1);
                return InputState.CHARCTER_SELECTED;
            }

            result.Remove(string.Format("Skill {0}", selectedSkill + 1));
            result.Add(string.Format("Skill {0}", selectSkillBuffer + 1));
            selectedSkill = selectSkillBuffer;
            skillMenu.ToggleSkillUI(selectedSkill);

            return InputState.SKILL_SELECTED_ALL_ALLIANCES;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // if (hit.collider == null) return InputState.SKILL_SELECTED_ALL_ENEMIES;

        // if (!characterManager.hasCharacter(hit.collider.tag))
        // {
        //     Debug.Log(hit.collider.tag + " selected");
        //     return currentState;
        // }

        string selectedEnemySum = "";
        foreach (CharacterHolder e in characterManager.getTeamHolders(0))
        {
            selectedEnemy = e.character.tag;
            selectedEnemySum += e.character.tag;
            selectedEnemySum += ",";
            characterManager.SetSelect(selectedEnemy, true);
            result.Add(e.character.name);
        }
        selectedEnemy = selectedEnemySum;
        // selectedEnemy = hit.collider.tag;
        // characterManager.SetSelect(selectedEnemy, true);
        // result.Add(hit.collider.name);
        return InputState.ENEMY_SELECTED;
    }

    private InputState ChooseSkillTargetWholeField()
    {
        if ((selectedSkill > -1)
            && (selectSkillBuffer > -1))
        {

            if (selectedSkill == selectSkillBuffer)
            {
                selectedSkill = -1;
                result.RemoveAt(result.Count - 1);
                return InputState.CHARCTER_SELECTED;
            }

            result.Remove(string.Format("Skill {0}", selectedSkill + 1));
            result.Add(string.Format("Skill {0}", selectSkillBuffer + 1));
            selectedSkill = selectSkillBuffer;
            skillMenu.ToggleSkillUI(selectedSkill);

            return InputState.SKILL_SELECTED_ALL_ALLIANCES;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // if (hit.collider == null) return InputState.SKILL_SELECTED_ALL_ENEMIES;

        // if (!characterManager.hasCharacter(hit.collider.tag))
        // {
        //     Debug.Log(hit.collider.tag + " selected");
        //     return currentState;
        // }

        string selectedEnemySum = "";
        List<CharacterHolder> chars = new List<CharacterHolder>();
        chars.AddRange(characterManager.getTeamHolders(0));
        chars.AddRange(characterManager.getTeamHolders(1));

        foreach (CharacterHolder e in chars)
        {
            selectedEnemy = e.character.tag;
            selectedEnemySum += e.character.tag;
            selectedEnemySum += ",";
            characterManager.SetSelect(selectedEnemy, true);
            result.Add(e.character.name);
        }
        selectedEnemy = selectedEnemySum;
        // selectedEnemy = hit.collider.tag;
        // characterManager.SetSelect(selectedEnemy, true);
        // result.Add(hit.collider.name);
        return InputState.ENEMY_SELECTED;
    }

    //! Original
    private InputState ChooseSkillTarget()
    {
        if ((selectedSkill > -1)
            && (selectSkillBuffer > -1))
        {

            if (selectedSkill == selectSkillBuffer)
            {
                selectedSkill = -1;
                result.RemoveAt(result.Count - 1);
                return InputState.CHARCTER_SELECTED;
            }

            result.Remove(string.Format("Skill {0}", selectedSkill + 1));
            result.Add(string.Format("Skill {0}", selectSkillBuffer + 1));
            selectedSkill = selectSkillBuffer;
            skillMenu.ToggleSkillUI(selectSkillBuffer);

            return InputState.SKILL_SELECTED;
        }

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null) return InputState.SKILL_SELECTED;

            if (hit.collider.CompareTag("Enemy1") || hit.collider.CompareTag("Enemy2") || hit.collider.CompareTag("Enemy3")
                || hit.collider.CompareTag("Enemy4") || hit.collider.CompareTag("Boss"))
            {


                if (!characterManager.hasCharacter(hit.collider.tag))
                {
                    Debug.Log(hit.collider.tag + " selected");
                    return currentState;
                }



                selectedEnemy = hit.collider.tag;
                characterManager.SetSelect(selectedEnemy, true);
                result.Add(hit.collider.name);
                return InputState.ENEMY_SELECTED;
            }
        }
        return InputState.SKILL_SELECTED;
    }


    private InputState ConfirmAction()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null) return currentState;

            if ((selectedEnemy != "")
                && (hit.collider.CompareTag("Enemy1") || hit.collider.CompareTag("Enemy2") || hit.collider.CompareTag("Enemy3")
                || hit.collider.CompareTag("Enemy4") || hit.collider.CompareTag("Boss")))
            {
                if (!characterManager.hasCharacter(hit.collider.tag)) return InputState.SKILL_SELECTED;


                characterManager.SetSelect(selectedEnemy, false);
                GameObject oldEnemy = characterManager.GetCharacter(selectedEnemy).character;
                result.Remove(oldEnemy.name);

                if (hit.collider.tag != selectedEnemy)
                {

                    selectedEnemy = hit.collider.tag;
                    characterManager.SetSelect(selectedEnemy, true);
                    result.Add(hit.collider.name);
                }
                else
                {
                    return InputState.SKILL_SELECTED;
                }

                return InputState.ENEMY_SELECTED;
            }

            else if ((selectedEnemy != "")
                && (hit.collider.CompareTag("Plant1") || hit.collider.CompareTag("Plant2") || hit.collider.CompareTag("Plant3")
                || hit.collider.CompareTag("Chaam")))
            {
                if (!characterManager.hasCharacter(hit.collider.tag)) return InputState.SKILL_SELECTED_ONE_ALLY;


                characterManager.SetSelect(selectedEnemy, false);
                GameObject oldEnemy = characterManager.GetCharacter(selectedEnemy).character;
                result.Remove(oldEnemy.name);

                if (hit.collider.tag != selectedEnemy)
                {

                    selectedEnemy = hit.collider.tag;
                    characterManager.SetSelect(selectedEnemy, true);
                    result.Add(hit.collider.name);
                }
                else
                {
                    return InputState.SKILL_SELECTED_ONE_ALLY;
                }

                return InputState.ENEMY_SELECTED;
            }
        }

        var holder = characterManager.GetCharacter(selectedPak);
        // Player presses cancel button
        if (cancelPressed && holder.InAction)
        {
            // Remove action
            var commandHandler = BattleManager.instance.actionCommandHandler;
            commandHandler.RemoveAction(selectedPak);
            holder.Action(false);
            return InputState.DEFAULT;
        }

        // Player presses ok button
        if (okPressed)
        {
            // If holder is In action remove old action
            // then waiting for the new one
            if (holder.InAction)
            {
                var commandHandler = BattleManager.instance.actionCommandHandler;
                commandHandler.RemoveAction(selectedPak);
                holder.Action(false);
            }

            // Add result string to output log
            string output = "Add command ";
            foreach (string name in result)
            {
                output += name;
                output += " ";
            }
            Debug.Log(output);
            return InputState.COMFIRMED;
        }

        return currentState;
    }

    private InputState PlayerEndTurn()
    {
        if (endTurnPressed)
        {
            BattleManager.instance.RunCommand();
            return InputState.END_TURN;
        }
        return InputState.DEFAULT;
    }

    private void UpdateUI()
    {

        switch (nextState)
        {
            case InputState.CHARCTER_SELECTED:
                skillMenu.ToggleMenu(true);
                //? Yod do
                PakRender pak = GameObject.Find(selectedPak).transform.GetChild(0).GetComponent<PakRender>();
                skillMenu.skills[0].GetComponent<Tooltiptrigger>().setContent(pak.skill[0].Description);
                skillMenu.skills[1].GetComponent<Tooltiptrigger>().setContent(pak.skill[1].Description);
                skillMenu.skills[2].GetComponent<Tooltiptrigger>().setContent(pak.skill[2].Description);
                // Debug.Log("Skill class is" + pak.GetType()); // GetType() return original type of this obj.
                //?

                backButton.gameObject.SetActive(true);
                endTurnButton.gameObject.SetActive(false);
                Backdrop.SetActive(false);
                break;
            case InputState.SKILL_SELECTED:
                backButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(false);
                Backdrop.SetActive(true);
                break;

            case InputState.SKILL_SELECTED_ONE_ALLY:
                backButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(false);
                break;
            case InputState.SKILL_SELECTED_ALL_ENEMIES:
                endTurnButton.gameObject.SetActive(false);
                TooltipScreenSpaceUI.hideTooltip_Static();
                break;
            case InputState.SKILL_SELECTED_ALL_ALLIANCES:
                endTurnButton.gameObject.SetActive(false);
                TooltipScreenSpaceUI.hideTooltip_Static();
                break;
            case InputState.SKILL_SELECTED_WHOLE_FIELD:
                endTurnButton.gameObject.SetActive(false);
                TooltipScreenSpaceUI.hideTooltip_Static();
                break;
            case InputState.ENEMY_SELECTED:
                okButton.gameObject.SetActive(true);
                backButton.gameObject.SetActive(true);

                // if the selected pak is already in action show cancel button
                var pakHolder = characterManager.GetCharacter(selectedPak);
                if (pakHolder.InAction)
                {
                    cancelButton.gameObject.SetActive(true);
                }
                break;
            case InputState.COMFIRMED:
                endTurnButton.gameObject.SetActive(false);
                okButton.gameObject.SetActive(false);
                backButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(false);
                break;
            case InputState.END_TURN:
                endTurnButton.gameObject.SetActive(false);
                okButton.gameObject.SetActive(false);
                backButton.gameObject.SetActive(false);
                break;
            default:
                skillMenu.ToggleMenu(false);
                backButton.gameObject.SetActive(false);
                okButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(false);
                endTurnButton.gameObject.SetActive(true);
                supportMenu.SetActive(false);
                Backdrop.SetActive(false);
                break;
        }
    }

    private void UpdateCharacterLayer(InputState state)
    {
        if (state == InputState.SKILL_SELECTED ||
            (currentState == InputState.DEFAULT && state == InputState.ENEMY_SELECTED))
        {
            var characterTags = new List<string> { "Enemy1", "Enemy2", "Enemy3", "Enemy4", "Boss" };
            characterTags.Add(selectedPak);
            characterManager.HighLightCharacters(characterTags);
        }
        else if (state == InputState.CHARCTER_SELECTED ||
                 state == InputState.DEFAULT ||
                 state == InputState.END_TURN)
        {
            // Reset all highlight to default
            characterManager.ResetHighLight();
        }
    }

    public void UpdateSkillUIImage(GameObject ally)
    {

        PakRender pakRender = ally.GetComponent<PakRender>();

        // Check  if the ally gameobject has PakRende or ChaamRender
        // WSprite is null if the ally does not have both
        if (pakRender != null)
        {
            skillMenu.UpdateCharacterUI(pakRender.pak.Image);
            skillMenu.UpdateSkillUI(pakRender);

        }
    }


    public void SendCommand()
    {
        GameObject caller = characterManager.GetCharacter(selectedPak).character;
        if (caller != null)
        {
            Debug.Log(selectedEnemy);
            if (selectedEnemy.Contains(","))
            {
                List<string> selectedEnemyList = new List<string>(selectedEnemy.Split(','));
                selectedEnemyList.RemoveAt(selectedEnemyList.Count - 1);
                GameObject[] targets = new GameObject[selectedEnemyList.Count];
                for (int i = 0; i < selectedEnemyList.Count; i++)
                {
                    Debug.Log(selectedEnemyList[i]);
                    targets[i] = characterManager.GetCharacter(selectedEnemyList[i]).character;
                }
                BattleManager.instance.AddNewCommand(caller, selectedSkill, targets);
            }
            else
            {
                GameObject[] targets = { characterManager.GetCharacter(selectedEnemy).character };
                BattleManager.instance.AddNewCommand(caller, selectedSkill, targets);
            }

        }
    }

    public void reset()
    {
        nextState = InputState.DEFAULT;

        characterManager.ResetSelect();

        selectedPak = "";
        selectedEnemy = "";

        if (selectedSkill > -1)
            skillMenu.ToggleSkillUI(selectedSkill);
        selectedSkill = -1;
        selectSkillBuffer = -1;


        if (result == null)
            result = new List<string>();

        actionFinished = false;
        UpdateCharacterLayer(InputState.DEFAULT);
    }

    public void SelectSkill(int index)
    {
        selectSkillBuffer = index;
    }

    private void SetActionFinished()
    {
        actionFinished = true;
    }
}
