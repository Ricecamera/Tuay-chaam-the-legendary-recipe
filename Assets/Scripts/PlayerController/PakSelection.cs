﻿using System;
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


    //Text
    public Text selectTargetText;
    public Text selectSkillText;

    private List<GameObject> alreadySelectSkill;

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

        //Set text during pak selection to not active
        selectTargetText.gameObject.SetActive(false);
        selectSkillText.gameObject.SetActive(false);

        alreadySelectSkill = new List<GameObject>(); //create a empty list

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
                var pakRender = characterManager.GetCharacter(selectedPak);

                // if select character is in action set it back to action state
                if (pakRender.InAction())
                {
                    pakRender.DisplayInAction(true);
                }

                selectedPak = "";
                LockChar();

                reset();
            }
            else if (currentState == InputState.SKILL_SELECTED || currentState == InputState.SKILL_SELECTED_ALL_ALLIANCES || currentState == InputState.SKILL_SELECTED_ALL_ENEMIES || currentState == InputState.SKILL_SELECTED_ONE_ALLY || currentState == InputState.SKILL_SELECTED_WHOLE_FIELD)
            {
                selectedPak = "";
                LockChar();
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
                selectSkillText.gameObject.SetActive(true); //set select skill text to actives

                nextState = ChooseSkill();
                // if (nextState != currentState)
                UpdateCharacterLayer(nextState);
                break;

            //************************************************************************** Select target state
            case InputState.SKILL_SELECTED: //now is Skill_Selected_one_enemy
                // An skill to be add to commnad list was selected
                selectSkillText.gameObject.SetActive(false); //set select skill text to not actives
                UnlockChar(); //let player select apporpriate target.

                nextState = ChooseSkillTarget(); //now is function to select one enemy.
                if (nextState != currentState)
                    UpdateCharacterLayer(nextState);
                break;
            case InputState.SKILL_SELECTED_ONE_ALLY:
                selectSkillText.gameObject.SetActive(false); //set select skill text to not actives
                UnlockChar(); //let player select apporpriate target.

                nextState = ChooseSkillTargetOneAlliance();
                if (nextState != currentState)
                    UpdateCharacterLayer(nextState);
                break;
            //? Peem Do
            case InputState.SKILL_SELECTED_ALL_ENEMIES:
                selectSkillText.gameObject.SetActive(false); //set select skill text to not actives
                UnlockChar(); //let player select apporpriate target.

                nextState = ChooseSkillTargetAllEnemies();
                if (nextState != currentState)
                    UpdateCharacterLayer(nextState);
                break;
            case InputState.SKILL_SELECTED_ALL_ALLIANCES:
                selectSkillText.gameObject.SetActive(false); //set select skill text to not actives
                UnlockChar(); //let player select apporpriate target.

                nextState = ChooseSkillTargetAllAlliances();
                if (nextState != currentState)
                    UpdateCharacterLayer(nextState);
                break;
            case InputState.SKILL_SELECTED_WHOLE_FIELD:
                selectSkillText.gameObject.SetActive(false); //set select skill text to not actives
                UnlockChar(); //let player select apporpriate target.

                nextState = ChooseSkillTargetWholeField();
                if (nextState != currentState)
                    UpdateCharacterLayer(nextState);
                break;
            //************************************************************************** Select target state

            case InputState.ENEMY_SELECTED:
                // An target for skilled was selected
                nextState = ConfirmAction();
                if (nextState == InputState.DEFAULT)
                    reset();
                break;
            case InputState.COMFIRMED:
                // Player clicked OK button
                selectTargetText.gameObject.SetActive(false); //set select text to not actives
                LockChar();
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
                            LevelManager.instance.winTime+=1;
                            SceneManager.LoadScene("VictoryScene");
                            LevelManager.instance.unlockStatus[LevelManager.instance.thislevel - 1 + 1] = true;
                        }
                        else
                        {
                            //do defeat stuff
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
                    characterManager.ResetState(0);
                    UnlockChar();
                    alreadySelectSkill = new List<GameObject>();
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

                    PakRender ally = characterManager.GetCharacter(hit.collider.tag);

                    if (ally != null)
                    {
                        // Select the ally
                        selectedPak = hit.collider.tag;
                        ally.Selected = true;

                        // Send character to update on Skill menu
                        UpdateSkillUIImage(ally);

                        if (ally.InAction())
                        {
                            // Get old action data
                            ActionCommand action = BattleManager.instance.actionCommandHandler.GetAction(selectedPak);

                            // get index of the called skill in the caller pak
                            selectedSkill = action.selectedSkill;
                            skillMenu.ToggleSkillUI(selectedSkill);

                            // get game tag of the target
                            selectedEnemy = action.targets[0].gameObject.tag;
                            characterManager.GetCharacter(selectedEnemy).Selected = true;

                            // Change UI from DEFAULT scene to ENEMY_SELECTED
                            skillMenu.ToggleMenu(true);
                            endTurnButton.gameObject.SetActive(false);
                            Backdrop.SetActive(true);
                            UpdateCharacterLayer(InputState.ENEMY_SELECTED);
                            action.caller.DisplayInAction(false);
                            return InputState.ENEMY_SELECTED;
                        }


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
                // Set selected of the old selected character to false
                characterManager.GetCharacter(selectedPak).Selected = false;

                if (hit.collider.CompareTag(selectedPak))
                {
                    selectedPak = "";
                    return InputState.DEFAULT;
                }

                // Set selected state of the new character
                selectedPak = hit.collider.tag;
                PakRender newPak = characterManager.GetCharacter(selectedPak);
                newPak.Selected = false;

                // Send character to update on Skill menu
                UpdateSkillUIImage(newPak);
            }
        }
        return InputState.CHARCTER_SELECTED;
    }

    private InputState ChooseSkillTargetOneAlliance()
    {
        selectTargetText.gameObject.SetActive(true);
        if ((selectedSkill > -1)
            && (selectSkillBuffer > -1))
        {

            if (selectedSkill == selectSkillBuffer)
            {
                selectedSkill = -1;
                return InputState.CHARCTER_SELECTED;
            }

            selectedSkill = selectSkillBuffer;
            skillMenu.ToggleSkillUI(selectedSkill);

            Debug.Log("Now chabg skill to" + selectSkillBuffer); //selectSkillBuffer is index of skill in SkillMenuUI

            //check here that the new skill is which type using switch case and return that state
            PakRender selectedPakRender = GameObject.Find(selectedPak).transform.GetChild(0).GetComponent<PakRender>();
            Skill pakSkill = selectedPakRender.skill[selectSkillBuffer];



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
                characterManager.GetCharacter(selectedEnemy).Selected = true;
                return InputState.ENEMY_SELECTED;
            }
        }
        return InputState.SKILL_SELECTED_ONE_ALLY;
    }


    private InputState ChooseSkillTargetAllEnemies()
    {
        selectTargetText.gameObject.SetActive(true);
        if ((selectedSkill > -1)
            && (selectSkillBuffer > -1))
        {

            if (selectedSkill == selectSkillBuffer)
            {
                selectedSkill = -1;
                return InputState.CHARCTER_SELECTED;
            }
            selectedSkill = selectSkillBuffer;
            skillMenu.ToggleSkillUI(selectedSkill);

            Debug.Log("Now chabg skill to" + selectSkillBuffer); //selectSkillBuffer is index of skill in SkillMenuUI

            //check here that the new skill is which type using switch case and return that state
            PakRender selectedPakRender = GameObject.Find(selectedPak).transform.GetChild(0).GetComponent<PakRender>();
            Skill pakSkill = selectedPakRender.skill[selectSkillBuffer];



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

        //need to click on one enemy to proceed
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2 = Physics2D.Raycast(ray2.origin, ray2.direction);

            if (hit2.collider == null) return InputState.SKILL_SELECTED_ALL_ENEMIES;

            if (hit2.collider.CompareTag("Enemy1") || hit2.collider.CompareTag("Enemy2") || hit2.collider.CompareTag("Enemy3")
                || hit2.collider.CompareTag("Enemy4") || hit2.collider.CompareTag("Boss"))
            {
                string selectedEnemySum = "";
                foreach (PakRender e in characterManager.getTeamHolders(1))
                {
                    selectedEnemy = e.tag;
                    selectedEnemySum += e.tag;
                    selectedEnemySum += ",";
                    characterManager.GetCharacter(selectedEnemy).Selected = true;
                }
                selectedEnemy = selectedEnemySum;
                // selectedEnemy = hit2.collider.tag;
                // characterManager.SetSelect(selectedEnemy, true);
                return InputState.ENEMY_SELECTED;
            }
        }

        return InputState.SKILL_SELECTED_ALL_ENEMIES;
    }

    private InputState ChooseSkillTargetAllAlliances()
    {
        selectTargetText.gameObject.SetActive(true);
        if ((selectedSkill > -1)
            && (selectSkillBuffer > -1))
        {

            if (selectedSkill == selectSkillBuffer)
            {
                selectedSkill = -1;
                return InputState.CHARCTER_SELECTED;
            }


            selectedSkill = selectSkillBuffer;
            skillMenu.ToggleSkillUI(selectedSkill);

            Debug.Log("Now chabg skill to" + selectSkillBuffer); //selectSkillBuffer is index of skill in SkillMenuUI

            //check here that the new skill is which type using switch case and return that state
            PakRender selectedPakRender = GameObject.Find(selectedPak).transform.GetChild(0).GetComponent<PakRender>();
            Skill pakSkill = selectedPakRender.skill[selectSkillBuffer];



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

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2 = Physics2D.Raycast(ray2.origin, ray2.direction);

            if (hit2.collider == null) return InputState.SKILL_SELECTED_ALL_ALLIANCES;

            if (hit2.collider.CompareTag("Plant1") || hit2.collider.CompareTag("Plant2") || hit2.collider.CompareTag("Plant3")
                || hit2.collider.CompareTag("Plant4") || hit2.collider.CompareTag("Chaam"))
            {
                string selectedEnemySum = "";
                foreach (PakRender e in characterManager.getTeamHolders(0))
                {
                    selectedEnemy = e.tag;
                    selectedEnemySum += e.tag;
                    selectedEnemySum += ",";
                    characterManager.GetCharacter(selectedEnemy).Selected = true;
                }
                selectedEnemy = selectedEnemySum;
                // selectedEnemy = hit2.collider.tag;
                // characterManager.SetSelect(selectedEnemy, true);

                return InputState.ENEMY_SELECTED;
            }
        }

        return InputState.SKILL_SELECTED_ALL_ALLIANCES;
    }

    private InputState ChooseSkillTargetWholeField()
    {
        selectTargetText.gameObject.SetActive(true);
        if ((selectedSkill > -1)
            && (selectSkillBuffer > -1))
        {

            if (selectedSkill == selectSkillBuffer)
            {
                selectedSkill = -1;
                return InputState.CHARCTER_SELECTED;
            }


            selectedSkill = selectSkillBuffer;
            skillMenu.ToggleSkillUI(selectedSkill);

            Debug.Log("Now chabg skill to" + selectSkillBuffer); //selectSkillBuffer is index of skill in SkillMenuUI

            //check here that the new skill is which type using switch case and return that state
            PakRender selectedPakRender = GameObject.Find(selectedPak).transform.GetChild(0).GetComponent<PakRender>();
            Skill pakSkill = selectedPakRender.skill[selectSkillBuffer];



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

            return InputState.SKILL_SELECTED_WHOLE_FIELD;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // if (hit.collider == null) return InputState.SKILL_SELECTED_ALL_ENEMIES;

        // if (!characterManager.hasCharacter(hit.collider.tag))
        // {
        //     Debug.Log(hit.collider.tag + " selected");
        //     return currentState;
        // }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2 = Physics2D.Raycast(ray2.origin, ray2.direction);

            if (hit2.collider == null) return InputState.SKILL_SELECTED_WHOLE_FIELD;

            if (hit2.collider.CompareTag("Plant1") || hit2.collider.CompareTag("Plant2") || hit2.collider.CompareTag("Plant3")
                || hit2.collider.CompareTag("Plant4") || hit2.collider.CompareTag("Chaam") || hit2.collider.CompareTag("Enemy1") || hit2.collider.CompareTag("Enemy2") || hit2.collider.CompareTag("Enemy3")
                || hit2.collider.CompareTag("Enemy4") || hit2.collider.CompareTag("Boss"))
            {

                string selectedEnemySum = "";
                List<PakRender> chars = new List<PakRender>();
                chars.AddRange(characterManager.getTeamHolders(0));
                chars.AddRange(characterManager.getTeamHolders(1));

                foreach (PakRender e in chars)
                {
                    selectedEnemy = e.tag;
                    selectedEnemySum += e.tag;
                    selectedEnemySum += ",";
                    characterManager.GetCharacter(selectedEnemy).Selected = true;
                }
                selectedEnemy = selectedEnemySum;
                // selectedEnemy = hit.collider.tag;
                // characterManager.SetSelect(selectedEnemy, true);
                return InputState.ENEMY_SELECTED;
            }
        }

        return InputState.SKILL_SELECTED_WHOLE_FIELD;
    }

    //! Original
    private InputState ChooseSkillTarget()
    {
        selectTargetText.gameObject.SetActive(true);

        if ((selectedSkill > -1)
            && (selectSkillBuffer > -1))
        {

            if (selectedSkill == selectSkillBuffer)
            {
                selectedSkill = -1;
                return InputState.CHARCTER_SELECTED;
            }

            selectedSkill = selectSkillBuffer;
            skillMenu.ToggleSkillUI(selectSkillBuffer);
            Debug.Log("Now chabg skill to" + selectSkillBuffer); //selectSkillBuffer is index of skill in SkillMenuUI

            //check here that the new skill is which type using switch case and return that state
            PakRender selectedPakRender = GameObject.Find(selectedPak).transform.GetChild(0).GetComponent<PakRender>();
            Skill pakSkill = selectedPakRender.skill[selectSkillBuffer];



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
                characterManager.GetCharacter(selectedEnemy).Selected = true;
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

                characterManager.GetCharacter(selectedEnemy).Selected = false;

                if (hit.collider.tag != selectedEnemy)
                {
                    selectedEnemy = hit.collider.tag;
                    characterManager.GetCharacter(selectedEnemy).Selected = true;
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

                characterManager.GetCharacter(selectedEnemy).Selected = false;

                if (hit.collider.tag != selectedEnemy)
                {

                    selectedEnemy = hit.collider.tag;
                    characterManager.GetCharacter(selectedEnemy).Selected = true;
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
        if (cancelPressed && holder.InAction())
        {
            // Remove action
            var commandHandler = BattleManager.instance.actionCommandHandler;
            commandHandler.RemoveAction(selectedPak);
            holder.currentState = PakRender.State.Idle;
            holder.DisplayInAction(false);
            return InputState.DEFAULT;
        }

        // Player presses ok button
        if (okPressed)
        {
            // If holder is In action remove old action
            // then waiting for the new one
            if (holder.InAction())
            {
                var commandHandler = BattleManager.instance.actionCommandHandler;
                commandHandler.RemoveAction(selectedPak);
                holder.currentState = PakRender.State.Idle;
                holder.DisplayInAction(false, 0);
            }

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
                Backdrop.SetActive(true);
                break;
            case InputState.SKILL_SELECTED:
                backButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(false);
                Backdrop.SetActive(true);
                break;

            case InputState.SKILL_SELECTED_ONE_ALLY:
                backButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(false);
                Backdrop.SetActive(true);
                break;
            case InputState.SKILL_SELECTED_ALL_ENEMIES:
                endTurnButton.gameObject.SetActive(false);
                Backdrop.SetActive(true);
                break;
            case InputState.SKILL_SELECTED_ALL_ALLIANCES:
                endTurnButton.gameObject.SetActive(false);
                Backdrop.SetActive(true);
                break;
            case InputState.SKILL_SELECTED_WHOLE_FIELD:
                endTurnButton.gameObject.SetActive(false);
                Backdrop.SetActive(true);
                break;
            case InputState.ENEMY_SELECTED:
                TooltipScreenSpaceUI.hideTooltip_Static();
                okButton.gameObject.SetActive(true);
                backButton.gameObject.SetActive(true);
                if (selectedSkill == 0)
                {
                    skillMenu.skills[1].GetComponent<Button>().interactable = false;
                    skillMenu.skills[2].GetComponent<Button>().interactable = false;
                    //SkillMenuUI.skills[3].GetComponent<Button>().interactable = false; //ulti skill
                }
                if (selectedSkill == 1)
                {
                    skillMenu.skills[0].GetComponent<Button>().interactable = false;
                    skillMenu.skills[2].GetComponent<Button>().interactable = false;
                    //SkillMenuUI.skills[3].GetComponent<Button>().interactable = false; //ulti skill
                }
                if (selectedSkill == 2)
                {
                    skillMenu.skills[0].GetComponent<Button>().interactable = false;
                    skillMenu.skills[1].GetComponent<Button>().interactable = false;
                    //SkillMenuUI.skills[3].GetComponent<Button>().interactable = false; //ulti skill
                }
                // if(selectedSkill == 3){
                //     skillMenu.skills[0].GetComponent<Button>().interactable = false;
                //     skillMenu.skills[1].GetComponent<Button>().interactable = false;
                //SkillMenuUI.skills[2].GetComponent<Button>().interactable = false;
                // }

                // if the selected pak is already in action show cancel button
                var pakHolder = characterManager.GetCharacter(selectedPak);
                if (pakHolder.InAction())
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
                foreach (SkillUI skill in skillMenu.skills) {
                    if (skill.isActiveAndEnabled)
                        skill.GetComponent<Button>().interactable = true;
                }
                //skillMenu.skills[3].GetComponent<Button>().interactable = true; //ulti skill
                break;
        }
    }

    private void UpdateCharacterLayer(InputState state)
    {
        if (state == InputState.SKILL_SELECTED || state == InputState.SKILL_SELECTED_ALL_ENEMIES ||
            (currentState == InputState.DEFAULT && state == InputState.ENEMY_SELECTED))
        {
            characterManager.ResetState(3);
            var characterTags = new List<string> { "Enemy1", "Enemy2", "Enemy3", "Enemy4", "Boss" };
            characterTags.Add(selectedPak);
            characterManager.HighLightCharacters(characterTags);
        }
        else if (state == InputState.SKILL_SELECTED_ONE_ALLY || state == InputState.SKILL_SELECTED_ALL_ALLIANCES ||
            (currentState == InputState.DEFAULT && state == InputState.ENEMY_SELECTED))
        {
            characterManager.ResetState(3);
            var characterTags = new List<string> { "Plant1", "Plant2", "Plant3", "Plant4", "Chaam" };
            characterManager.HighLightCharacters(characterTags);
        }
        else if (state == InputState.SKILL_SELECTED_WHOLE_FIELD ||
            (currentState == InputState.DEFAULT && state == InputState.ENEMY_SELECTED))
        {
            characterManager.ResetState(3);
            var characterTags = new List<string> { "Enemy1", "Enemy2", "Enemy3", "Enemy4", "Boss", "Plant1", "Plant2", "Plant3", "Plant4", "Chaam" };
            characterManager.HighLightCharacters(characterTags);
        }
        else if (state == InputState.DEFAULT ||
                 state == InputState.END_TURN)
        {
            // Reset all highlight to default
            characterManager.ResetState(3);
        }
        else if (state == InputState.CHARCTER_SELECTED)
        {
            var characterTags = new List<string>();
            characterTags.Add(selectedPak);
            characterManager.HighLightCharacters(characterTags);
        }
    }

    public void UpdateSkillUIImage(PakRender ally)
    {

        // Check  if the ally gameobject has PakRende or ChaamRender
        // WSprite is null if the ally does not have both
        if (ally != null)
        {
            skillMenu.UpdateCharacterUI(ally.Entity.Image);
            skillMenu.UpdateSkillUI(ally.skill);

        }
    }


    public void SendCommand()
    {
        PakRender pakCaller = characterManager.GetCharacter(selectedPak);
        if (pakCaller != null)
        {
            pakCaller.currentState = PakRender.State.InAction;
            pakCaller.DisplayInAction(true, selectedSkill);
            List<PakRender> targets = new List<PakRender>();

            // Convert selectedEnemy to list of Pakrender
            if (selectedEnemy.Contains(","))
            {
                // selectedEnemy has multiple tags
                List<string> selectedEnemyList = new List<string>(selectedEnemy.Split(','));
                selectedEnemyList.RemoveAt(selectedEnemyList.Count - 1); // remove space in the end
                foreach (var enemy in characterManager.getTeamHolders(1))
                {
                    if (selectedEnemyList.Contains(enemy.tag)) {
                        Debug.Log(enemy.tag);
                        targets.Add(enemy);
                    }
                }
            }
            else
            {
                // selectedEnemy has only one tag
                targets.Add(characterManager.GetCharacter(selectedEnemy));
            }

            float speed = pakCaller.currentSpeed;
            ActionCommand newCommand = new ActionCommand(pakCaller, selectedSkill, targets, speed);
            BattleManager.instance.actionCommandHandler.AddCommand(newCommand);

        }
    }

    public void reset()
    {
        selectSkillText.gameObject.SetActive(false); //set select skill text to not actives
        selectTargetText.gameObject.SetActive(false); //set select target text to not actives

        nextState = InputState.DEFAULT;

        characterManager.ResetState(2);

        selectedPak = "";
        selectedEnemy = "";

        if (selectedSkill > -1)
            skillMenu.ToggleSkillUI(selectedSkill);
        selectedSkill = -1;
        selectSkillBuffer = -1;



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

    private void LockChar()
    {
        if (selectedPak.CompareTo("") != 0)
        {
            GameObject caller = characterManager.GetCharacter(selectedPak).gameObject;
            alreadySelectSkill.Add(caller);
        }
        foreach (GameObject e in alreadySelectSkill)
        {
            e.GetComponent<BoxCollider2D>().enabled = false;
        }
        return;
    }

    private void UnlockChar()
    {
        List<PakRender> temp = characterManager.getHolders();
        foreach (PakRender e in temp)
        {
            e.GetComponent<BoxCollider2D>().enabled = true;
        }
        return;
    }
}
