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
    private bool okPressed = false;
    private bool endTurnPressed = false;
    private bool backPressed = false;

    private int selectSkillBuffer = -1;     // buffer for storing user's click input

    private CharacterManager playerTeam;
    private CharacterManager enemyTeam;

    [SerializeField]
    private SkillMenuUI skillMenu;

    [SerializeField]
    private Button okButton, backButton, endTurnButton;

    [SerializeField]
    private GameObject supportMenu;

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
        playerTeam = GameObject.Find("PlayerTeam").GetComponent<CharacterManager>();
        enemyTeam = GameObject.Find("EnemyTeam").GetComponent<CharacterManager>();

        // Set callback function for skill buttons
        for (int i = 0; i < skillMenu.skills.Length; ++i)
        {
            int k = i;
            skillMenu.skills[i].onClick.AddListener(() => SelectSkill(k));
        }

        // Set callback function for game button
        okButton.onClick.AddListener(() => okPressed = true);
        backButton.onClick.AddListener(() => backPressed = true);
        endTurnButton.onClick.AddListener(() => endTurnPressed = true);

        reset();
    }

    // Update is called once per frame
    void Update()
    {
        // Change state and update UI
        if (currentState != nextState)
        {
            currentState = nextState;
            UpdateUI(currentState);
        }

        if (backPressed &&
            (currentState > InputState.DEFAULT && currentState < InputState.COMFIRMED))
        {
            reset();
            return;
        }

        // waiting for user inputs according to current state
        switch (currentState)
        {
            case InputState.CHARCTER_SELECTED:
                // An ally character was choosed
                nextState = ChooseSkill();
                break;
            case InputState.SKILL_SELECTED:
                // An skill to be add to commnad list was selected
                nextState = ChooseSkillTarget();
                break;
            case InputState.ENEMY_SELECTED:
                // An target for skilled was selected
                nextState = ConfirmAction();
                break;
            case InputState.COMFIRMED:
                // Player clicked OK button
                SendCommand();
                reset();
                break;
            case InputState.END_TURN:
                // Player clicked end-turn button
                if (actionFinished)
                    reset();
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

        selectSkillBuffer = -1;
    }

    private InputState chooseCharacter()
    {
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
                    if (!playerTeam.hasCharacter(hit.collider.tag)) return InputState.DEFAULT;

                    selectedPak = hit.collider.tag;
                    GameObject ally = hit.collider.gameObject;

                    playerTeam.SetSelect(selectedPak, true);

                    // Send character to update on Skill menu
                    SendCharacterImage(ally);

                    // Add value to result
                    result.Add(hit.collider.name);


                    return InputState.CHARCTER_SELECTED;
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
                case "TargetAllAlliance":
                    return InputState.SKILL_SELECTED_ALL_ALLIANCES;
                case "TargetOneAlliance":
                    return InputState.SKILL_SELECTED_ONE_ALLY;
                case "TargetAllEnemies":
                    return InputState.SKILL_SELECTED_ALL_ENEMIES;
                case "TargetOneEnemy":
                    return InputState.SKILL_SELECTED;
                case "TargetWholeField":
                    return InputState.SKILL_SELECTED_WHOLE_FIELD;
                default:
                    Debug.LogError("Wrong Skill Type");
                    break;
            }



            //return InputState.SKILL_SELECTED;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null) return InputState.CHARCTER_SELECTED;


            if ((selectedPak.CompareTo("") != 0) &&
                    (hit.collider.CompareTag("Plant1") || hit.collider.CompareTag("Plant2")
                    || hit.collider.CompareTag("Plant3") || hit.collider.CompareTag("Chaam")))
            {

                playerTeam.SetSelect(selectedPak, false);
                result.Clear();

                if (hit.collider.CompareTag(selectedPak))
                {
                    selectedPak = "";
                    result.Add(hit.collider.name);
                    return InputState.DEFAULT;
                }
                skillMenu.ToggleSkillUI(selectedSkill);
                selectedSkill = -1;
                selectSkillBuffer = -1;

                selectedPak = hit.collider.tag;
                GameObject ally = hit.collider.gameObject;
                playerTeam.SetSelect(selectedPak, true);

                // Send character to update on Skill menu
                SendCharacterImage(ally);

                // Add value to result
                result.Add(hit.collider.name);
            }
        }
        return InputState.CHARCTER_SELECTED;
    }




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
            skillMenu.ToggleSkillUI(selectedSkill);

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


                if (!enemyTeam.hasCharacter(hit.collider.tag))
                {
                    Debug.Log(hit.collider.tag + " selected");
                    return currentState;
                }


                selectedEnemy = hit.collider.tag;
                enemyTeam.SetSelect(selectedEnemy, true);
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
                if (!enemyTeam.hasCharacter(hit.collider.tag)) return InputState.SKILL_SELECTED;


                enemyTeam.SetSelect(selectedEnemy, false);
                GameObject oldEnemy = enemyTeam.GetCharacter(selectedEnemy).character;
                result.Remove(oldEnemy.name);

                if (hit.collider.tag != selectedEnemy)
                {

                    selectedEnemy = hit.collider.tag;
                    enemyTeam.SetSelect(selectedEnemy, true);
                    result.Add(hit.collider.name);
                }
                else
                {
                    return InputState.SKILL_SELECTED;
                }

                return InputState.ENEMY_SELECTED;
            }
        }

        if (okPressed)
        {
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

    private void UpdateUI(InputState currentState)
    {
        switch (currentState)
        {
            case InputState.CHARCTER_SELECTED:
                skillMenu.ToggleMenu(true);
                //? Yod do
                PakRender pak = GameObject.Find(selectedPak).transform.GetChild(0).GetComponent<PakRender>();
                GameObject.Find("Skill1").GetComponent<Tooltiptrigger>().setContent(pak.skill[0].Description);
                GameObject.Find("Skill2").GetComponent<Tooltiptrigger>().setContent(pak.skill[1].Description);
                GameObject.Find("Skill3").GetComponent<Tooltiptrigger>().setContent(pak.skill[2].Description);
                // Debug.Log("Skill class is" + pak.GetType()); // GetType() return original type of this obj.
                //?

                backButton.gameObject.SetActive(true);
                endTurnButton.gameObject.SetActive(false);
                break;
            case InputState.SKILL_SELECTED:
                backButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(false);
                break;
            case InputState.ENEMY_SELECTED:
                okButton.gameObject.SetActive(true);
                backButton.gameObject.SetActive(true);
                break;
            case InputState.COMFIRMED:
                endTurnButton.gameObject.SetActive(false);
                okButton.gameObject.SetActive(false);
                backButton.gameObject.SetActive(false);
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
                endTurnButton.gameObject.SetActive(true);
                supportMenu.SetActive(false);
                break;
        }
    }


    public void SendCharacterImage(GameObject ally)
    {

        PakRender pakRender = ally.GetComponent<PakRender>();
        ChaamRender chaamRender = ally.GetComponent<ChaamRender>();

        // Check  if the ally gameobject has PakRende or ChaamRender
        // WSprite is null if the ally does not have both
        if (pakRender != null)
        {
            skillMenu.UpdateCharacterUI(pakRender.pak.Image);

        }
        else if (chaamRender != null)
        {
            // skillMenu.UpdateCharacterUI(chaamRender.chaam.image);
        }
    }

    public void SendCommand()
    {
        GameObject caller = playerTeam.GetCharacter(selectedPak).character;
        if (caller != null)
        {
            string toCallSkill = string.Format("skill {0}", selectedSkill + 1);
            GameObject[] targets = { enemyTeam.GetCharacter(selectedEnemy).character };
            BattleManager.instance.AddNewCommand(caller, toCallSkill, targets);
        }
    }

    public void reset()
    {
        nextState = InputState.DEFAULT;

        playerTeam.Reset();
        enemyTeam.Reset();

        selectedPak = "";
        selectedEnemy = "";

        if (selectedSkill > -1)
            skillMenu.ToggleSkillUI(selectedSkill);
        selectedSkill = -1;
        selectSkillBuffer = -1;


        if (result == null)
            result = new List<string>();

        actionFinished = false;
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
