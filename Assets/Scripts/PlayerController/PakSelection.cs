using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleScene;
using BattleScene.BattleLogic;
/*
    *** Overview of this class ***
    
    Initial
    -ให้ Object ทุกตัวใน Class มี BoxCollider2D Component (เช่น Eggplant , Carrot , Garlik , all skills , Yuak , NormalPrik , ... )
    -ใช้ Raycast เพื่อ detect object ที่ mouse คลิก ถ้าคลิกโดนจะทำการ resize ให้ใหญ่ขึ้นเพื่อบอกว่าถูกเลือก
    -เปลี่ยน state ในการเลือกตามการกด ( default -> pakSelected -> skillSelected -> enemySelected -> pressOkButton )
*/

public class PakSelection : MonoBehaviour {

    enum InputState { DEFAULT, CHARCTER_SELECTED, SKILL_SELECTED, ENEMY_SELECTED, COMFIRMED, END_TURN };

    private InputState currentState;
    private bool actionFinished;

    private string selectedPak = "";
    private string selectedEnemy = "";
    private int selectedSkill = -1;  // current selected skill

    // buffer field using to check state after the player clicked some of the field-related buttons
    private bool okPressed = false;
    private bool endTurnPressed = false;
    private bool backPressed = false;
    private int selectSkillBuffer = -1;  // buffer for storing user's click input

    private CharacterManager characters;

    [SerializeField]
    private SkillMenuUI skillMenu;

    [SerializeField]
    private Button okButton;

    [SerializeField]
    private Button backButton;

    private List<string> result;

    private void OnEnable() {
        ActionCommandHandler.OnComplete += SetActionFinished;
    }

    private void OnDisable() {
        ActionCommandHandler.OnComplete -= SetActionFinished;
    }

    void Start() {
        characters = GameObject.FindGameObjectWithTag("spawner").GetComponent<CharacterManager>();

        for (int i = 0; i < skillMenu.skills.Length; ++i) {
            int k = i;
            skillMenu.skills[i].onClick.AddListener(() => SelectSkill(k));
        }

        okButton.onClick.AddListener(() => okPressed = true);
        backButton.onClick.AddListener(() => backPressed = true);

        reset();
    }

    // Update is called once per frame
    void Update() {
        InputState nextState = InputState.DEFAULT;
        switch (currentState) {
            case InputState.CHARCTER_SELECTED:
                 nextState = ChooseSkill();
                currentState = nextState;
                break;
            case InputState.SKILL_SELECTED:
                nextState = ChooseSkillTarget();
                currentState = nextState;
                break;
            case InputState.ENEMY_SELECTED:
                nextState = ConfirmAction();
                currentState = nextState;
                break;
            case InputState.COMFIRMED:
                SendCommand();
                reset();
                break;
            case InputState.END_TURN:
                if (actionFinished)
                    reset();
                break;
            default:
                nextState = PlayerEndTurn();
                if (nextState != InputState.END_TURN) {
                    nextState = chooseCharacter();
                }

                currentState = nextState;
                break;
        }
        
    }

    private void LateUpdate() {
        if (okPressed) {
            okPressed = false;
        }

        if (endTurnPressed) {
            endTurnPressed = false;
        }

        selectSkillBuffer = -1;
    }

    private InputState chooseCharacter() {
        if (Input.GetMouseButtonDown(0)) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null) {

                if (hit.collider.CompareTag("plant1") || hit.collider.CompareTag("plant2") 
                    || hit.collider.CompareTag("plant3") || hit.collider.CompareTag("chaam")) {
                    // find game object data with tag
                    if (!characters.hasCharacter(hit.collider.tag, true)) return InputState.DEFAULT;

                    selectedPak = hit.collider.tag;
                    GameObject ally = hit.collider.gameObject;

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

    private InputState ChooseSkill() {

        if (selectSkillBuffer > -1) {
                InputState nextState = InputState.SKILL_SELECTED;
                try {
                    selectedSkill = selectSkillBuffer;
                    skillMenu.ToggleSkillUI(selectSkillBuffer);
                    result.Add(string.Format("Skill {0}", selectedSkill + 1));
                } 
                catch (IndexOutOfRangeException e) {
                    Debug.LogError(e.Message);
                    nextState = InputState.DEFAULT;
                }
                
                return nextState;
        }

        if (Input.GetMouseButtonDown(0)) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null) return InputState.CHARCTER_SELECTED; 


            if ((selectedPak.CompareTo("") != 0) && 
                    (hit.collider.CompareTag("plant1") || hit.collider.CompareTag("plant2") 
                    || hit.collider.CompareTag("plant3") || hit.collider.CompareTag("chaam"))) {

                characters.SelectAlly(selectedPak, false);
                result.Clear();

                if (hit.collider.CompareTag(selectedPak)) {
                    skillMenu.ToggleMenu(false);
                    selectedPak = "";
                    result.Add(hit.collider.name);
                    return InputState.DEFAULT;
                }

                selectedPak = hit.collider.tag;
                GameObject ally = hit.collider.gameObject;

                // Send character to update on Skill menu
                SendCharacterImage(ally);

                // Add value to result
                result.Add(hit.collider.name);
            }
        }
        return InputState.CHARCTER_SELECTED;
    }
    
    //* Button btn = GameObject.Find("Skill1").GetComponent<Button>(); 
    //* btn.onClick.AddListener(...); 
    
    private InputState ChooseSkillTarget() {
        if ((selectedSkill > -1)
            && (selectSkillBuffer > -1)) {


            if (selectedSkill == selectSkillBuffer) {
                skillMenu.ToggleSkillUI(selectedSkill);
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

        if (Input.GetMouseButtonDown(0)) {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null) return InputState.SKILL_SELECTED;

            if (hit.collider.CompareTag("enemy1") || hit.collider.CompareTag("enemy2") || hit.collider.CompareTag("enemy3")
                || hit.collider.CompareTag("enemy4") || hit.collider.CompareTag("boss")) {
                if (!characters.hasCharacter(hit.collider.tag)) return currentState;

                selectedEnemy = hit.collider.tag;
                characters.SelectEnemy(selectedEnemy, true);
                result.Add(hit.collider.name);

                okButton.gameObject.SetActive(true);
                return InputState.ENEMY_SELECTED;
            }
        }
        return InputState.SKILL_SELECTED;
    }


    private InputState ConfirmAction() {

        if (Input.GetMouseButtonDown(0)) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null) return currentState;

            if ( (selectedEnemy != "")
                && (hit.collider.CompareTag("enemy1") || hit.collider.CompareTag("enemy2") || hit.collider.CompareTag("enemy3") 
                || hit.collider.CompareTag("enemy4") || hit.collider.CompareTag("boss"))) {
                if (!characters.hasCharacter(hit.collider.tag)) return InputState.SKILL_SELECTED;
                
                
                characters.SelectEnemy(selectedEnemy, false);
                GameObject oldEnemy = characters.GetCharacter(selectedEnemy);
                result.Remove(oldEnemy.name);

                if (hit.collider.tag != selectedEnemy) {

                    selectedEnemy = hit.collider.tag;
                    characters.SelectEnemy(selectedEnemy, true);
                    result.Add(hit.collider.name);
                }
                else {
                    okButton.gameObject.SetActive(false);
                    return InputState.SKILL_SELECTED;
                }

                return InputState.ENEMY_SELECTED;
            }
        }

        if (okPressed) {
            string output = "Add command ";
            foreach (string name in result) {
                output += name;
                output += " ";
            }
            Debug.Log(output);
            return InputState.COMFIRMED;
        }
            
        return currentState;
    }

    private InputState PlayerEndTurn() {
        if (Input.GetKeyDown(KeyCode.R)) {
            BattleManager.instance.RunCommand();
            return InputState.END_TURN;
        }
        return InputState.DEFAULT;
    }


    public void SendCharacterImage(GameObject ally) {

        PakRender pakRender = ally.GetComponent<PakRender>();
        ChaamRender chaamRender = ally.GetComponent<ChaamRender>();

        // Check  if the ally gameobject has PakRende or ChaamRender
        // WSprite is null if the ally does not have both
        if (pakRender != null) {

            skillMenu.UpdateCharacterUI(pakRender.pak.Image);
        }
        else if (chaamRender != null) {
            skillMenu.UpdateCharacterUI(chaamRender.chaam.image);
        }

        // Update UI is skillMenu
        
        characters.SelectAlly(selectedPak, true);
    }

    public void SendCommand() {
        GameObject caller = characters.GetCharacter(selectedPak, true);
        string toCallSkill = string.Format("skill {0}", selectedSkill + 1);
        GameObject[] targets =  { characters.GetCharacter(selectedEnemy) };
        BattleManager.instance.AddNewCommand(caller, toCallSkill, targets );
    }

    public void reset() {
        Debug.Log("Back to default state");
        currentState = InputState.DEFAULT;

        if (selectedPak.CompareTo("") != 0)
            characters.SelectAlly(selectedPak, false);
        selectedPak = "";

        if (selectedSkill > -1)
            skillMenu.ToggleSkillUI(selectedSkill);
        selectedSkill = -1;
        selectSkillBuffer = -1;

        if (selectedEnemy.CompareTo("") != 0)
            characters.SelectEnemy(selectedEnemy, false);
        selectedEnemy = "";

        if (result == null)
            result = new List<string>();

        actionFinished = false;
        skillMenu.ToggleMenu(false);
        okButton.gameObject.SetActive(false);
    }

    public void SelectSkill(int index) {
        selectSkillBuffer = index;
    }

    private void SetActionFinished() {
        actionFinished = true;
    }
}
