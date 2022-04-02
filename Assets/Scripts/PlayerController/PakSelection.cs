using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BattleScene;
using BattleScene.BattleLogic;
using UnityEngine.UI;

public class PakSelection : MonoBehaviour
{
    public enum GameState
    {
        CHOOSE_CHARACTER, CHOOSE_SKILL, CHOOSE_CHAAM_SKILL, CHOOSE_COOK_SKILL, CHOOSE_TARGET,
        WAIT_FOR_CONFIRM, DISPLAY_SKILL, END_TURN,

        CHAAM_CHOOSE_TARGET, CHAAM_WAIT_FOR_CONFIRM
    };

    private GameState currentState;
    private PakRender selectedPak;
    public List<PakRender> selectedTargets = new List<PakRender>();
    private int selectedSkill;         // current selected skill

    // other scripts reference
    private BattleUIController _UIcontroller;
    public BattleManager battleManger;

    public List<String> ingredient;

    private CookingController _cookingController;

    public GameObject comboPanel;

    private Skill cookSkill;

    private void OnEnable()
    {
        _UIcontroller = GetComponent<BattleUIController>();
        // Set callback function for skill buttons
        for (int i = 0; i < _UIcontroller.skillMenu.skills.Length; ++i)
        {
            int k = i;
            _UIcontroller.skillMenu.skills[i].AddListener(() =>
            {
                HandleSelectSkill(k);
            });
        }

        // Set callback function for game button
        _UIcontroller.okButton.onClick.AddListener(HandleConfirm);
        _UIcontroller.backButton.onClick.AddListener(HandleBack);
        _UIcontroller.endTurnButton.onClick.AddListener(HandleEndturn);
        _UIcontroller.cancelButton.onClick.AddListener(HandleCancelAction);
        _UIcontroller.cookButton.onClick.AddListener(HandleCookButton);
        _UIcontroller.startCookButton.onClick.AddListener(HandleStartCookButton);
        for (int i = 0; i < _UIcontroller.supportButton.Length; i++)
        {
            int k = i;
            _UIcontroller.supportButton[i].onClick.AddListener(() => HandleSupportButton(k));
        }

        battleManger.SetChangeTurn(() => UpdateGameState(GameState.CHOOSE_CHARACTER));
        _cookingController = new CookingController();
    }

    private void OnDisable()
    {
        battleManger.SetChangeTurn(null);
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedSkill = -1;
        Reset();
        _UIcontroller.UpdateUI(GameState.CHOOSE_CHARACTER);
        ingredient = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == GameState.CHOOSE_CHARACTER)
        {
            if (ingredient.Capacity != 0)
            {
                ingredient.Clear();
            }
            //Is adding check win condition here is fine?
            bool isPlayerWin = battleManger.IsPlayerWin();
            bool isPlayerLose = battleManger.IsPlayerLose();
            bool isGameOver = isPlayerWin || isPlayerLose;
            if (isGameOver)
            {
                if (isPlayerWin)
                {
                    //do victory stuff
                    LevelManager.instance.winTime += 1;
                    SceneManager.LoadScene("VictoryScene");
                    LevelManager.instance.unlockStatus[LevelManager.instance.thislevel - 1 + 1] = true;
                }
                else
                {
                    //do defeat stuff
                    SceneManager.LoadScene("LoseScene");
                }
            }
        }

        if (currentState == GameState.CHOOSE_TARGET)
        {
            // Check skill target condition
            if (selectedTargets.Count > 0)
                UpdateGameState(GameState.WAIT_FOR_CONFIRM); // Show ok button
        }

        if (currentState == GameState.CHAAM_CHOOSE_TARGET)
        {
            // Check skill target condition
            if (selectedTargets.Count > 0)
                UpdateGameState(GameState.CHAAM_WAIT_FOR_CONFIRM); // Show ok button
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null &&
                currentState != GameState.DISPLAY_SKILL &&
                currentState != GameState.END_TURN)
            {

                PakRender character = hit.collider.GetComponent<PakRender>();
                if (character == null)
                {
                    Debug.LogError(hit.collider.tag + " doesn't have PakRender component");
                    return;
                }

                if (currentState == GameState.CHAAM_CHOOSE_TARGET || currentState == GameState.CHAAM_WAIT_FOR_CONFIRM)
                {
                    controlSkill(cookSkill, character);
                }
                else if (currentState == GameState.CHOOSE_TARGET || currentState == GameState.WAIT_FOR_CONFIRM)
                {
                    controlSkill(selectedPak.skill[selectedSkill], character);
                }
                else if (CharacterManager.IsPlayerTeam(hit.collider.tag))
                {
                    HandleChooseAlly(character);
                }


            }
        }
    }

    public void controlSkill(Skill pakSkill, PakRender character)
    {


        if (pakSkill.ActionType == "TargetAllAlliances" ||
            pakSkill.ActionType == "TargetAllEnemies" ||
            pakSkill.ActionType == "TargetWholeField")
            return;

        // Check if the character is already selected
        for (int i = 0; i < selectedTargets.Count; i++)
        {
            if (selectedTargets[i].CompareTag(character.tag))
            {
                return;
            }
        }

        // Dummy if the number of targets is max
        if (selectedTargets.Count >= 1)
        {
            selectedTargets[0].Selected = false;
            selectedTargets.RemoveAt(0);
        }

        character.Selected = true;
        selectedTargets.Add(character);
        return;
    }

    public void HandleChooseAlly(PakRender ally)
    {
        switch (currentState)
        {
            case GameState.CHOOSE_CHARACTER:
                // Select the ally
                ally.Selected = true;
                selectedPak = ally;

                // Send character to update on Skill menu
                _UIcontroller.UpdateSkillMenuImage(ally);

                // Update tooltips
                for (int i = 0; i < _UIcontroller.skillMenu.skills.Length; i++)
                {
                    Tooltiptrigger tooltip = _UIcontroller.skillMenu.skills[i].GetComponent<Tooltiptrigger>();
                    tooltip.setContent(selectedPak.skill[i].Description);
                }

                if (ally.InAction())
                {
                    // Get old action data
                    ActionCommand action = battleManger.actionCommandHandler.GetAction(selectedPak.tag);

                    // get index of the called skill in the caller pak
                    selectedSkill = action.convertSelectedSkillToIndex();
                    _UIcontroller.skillMenu.ToggleSkill(selectedSkill);

                    // get game tag of the target
                    selectedTargets.AddRange(action.targets);
                    foreach (var charc in selectedTargets)
                        charc.Selected = true;


                    var highlightChar = new List<PakRender>(action.targets);
                    highlightChar.Add(selectedPak);

                    CharacterManager.instance.LockAllCharacters(true, 2);
                    UpdateCharacterLayer(highlightChar, true);
                    UpdateGameState(GameState.DISPLAY_SKILL);
                }
                else
                {
                    CharacterManager.instance.LockAllCharacters(true, 1);
                    if (selectedPak.gameObject.CompareTag("Chaam"))
                    {
                        UpdateGameState(GameState.CHOOSE_CHAAM_SKILL);
                    }
                    else
                    {
                        UpdateGameState(GameState.CHOOSE_SKILL);
                    }

                }
                break;
            case GameState.CHOOSE_SKILL: // select new character
                HandleChooseCharacter(ally);
                break;
            case GameState.CHOOSE_CHAAM_SKILL:
                HandleChooseCharacter(ally);
                break;
            default:
                break;
        }
    }

    public void HandleChooseCharacter(PakRender ally)
    {
        if (!ally.CompareTag(selectedPak.tag)) // choose new character
        {
            // Unselect old character
            selectedPak.Selected = false;
            selectedPak = null;

            // Set new character
            selectedPak = ally;
            selectedPak.Selected = true;

            // Update images on skill menu
            _UIcontroller.UpdateSkillMenuImage(ally);

            // Update tooltips
            for (int i = 0; i < _UIcontroller.skillMenu.skills.Length; i++)
            {
                Tooltiptrigger tooltip = _UIcontroller.skillMenu.skills[i].GetComponent<Tooltiptrigger>();
                tooltip.setContent(selectedPak.skill[i].Description);
            }

            if (currentState == GameState.CHOOSE_CHAAM_SKILL)
            {
                UpdateGameState(GameState.CHOOSE_SKILL);
            }

            if (currentState == GameState.CHOOSE_SKILL && selectedPak.CompareTag("Chaam"))
            {
                UpdateGameState(GameState.CHOOSE_CHAAM_SKILL);
            }
        }
        else // choose same character
        {
            // Unselect old character
            selectedPak.Selected = false;
            selectedPak = null;

            // the selected character is not setted
            UpdateGameState(GameState.CHOOSE_CHARACTER);
        }
    }

    public void HandleSelectSkill(int skillIndex)
    {
        Debug.Log("Button " + skillIndex + " clicked!");

        if (currentState == GameState.CHOOSE_CHAAM_SKILL)
        {
            UpdateGameState(GameState.CHOOSE_SKILL);
        }

        if (!(currentState == GameState.CHOOSE_SKILL || currentState == GameState.CHOOSE_TARGET || currentState == GameState.WAIT_FOR_CONFIRM))
            return;

        if (currentState == GameState.CHOOSE_TARGET || currentState == GameState.WAIT_FOR_CONFIRM)
        {
            // unselect all targets
            CharacterManager.instance.ResetState(2);
            // re-select current selected pak
            selectedPak.Selected = true;
            selectedTargets.Clear();
        }

        Skill pakSkill = null;
        try
        {
            _UIcontroller.skillMenu.ToggleSkill(skillIndex);
            pakSkill = selectedPak.skill[skillIndex];
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError(e.Message);
        }

        if (pakSkill != null)
        {
            // Do something

            if (selectedSkill == skillIndex)
            {
                selectedSkill = -1;
                UpdateGameState(GameState.CHOOSE_CHARACTER);
                return;
            }

            characterSelectMode(pakSkill.ActionType);

            selectedSkill = skillIndex;
            UpdateGameState(GameState.CHOOSE_TARGET);
        }
    }

    public void characterSelectMode(String Type)
    {
        switch (Type)
        {
            case "TargetAllAlliances":
                var allys = CharacterManager.instance.getTeamHolders(0);
                // Lock enemy team
                CharacterManager.instance.LockAllCharacters(true, 1);
                UpdateCharacterLayer(allys, true);

                // Automatically select ally targets
                SelectMultipleTargets(allys);
                break;
            case "TargetOneAlliance":
                var allys2 = CharacterManager.instance.getTeamHolders(0);
                // Lock enemy team
                CharacterManager.instance.LockAllCharacters(true, 1);
                UpdateCharacterLayer(allys2, true);
                break;
            case "TargetAllEnemies":
                var enemies2 = CharacterManager.instance.getTeamHolders(1);

                // Lock ally team
                CharacterManager.instance.LockAllCharacters(true, 0);
                UpdateCharacterLayer(enemies2, true);

                // Automatically select enemies targets
                SelectMultipleTargets(enemies2);
                break;
            case "TargetOneEnemy":
                var enemies = CharacterManager.instance.getTeamHolders(1);
                CharacterManager.instance.LockAllCharacters(true, 0);
                UpdateCharacterLayer(enemies, true);
                break;
            case "TargetWholeField":
                // get character of both team
                var characters = CharacterManager.instance.getHolders();
                UpdateCharacterLayer(characters, true);

                // Remove caller from ally list
                characters.Remove(selectedPak);

                // Automatically select ally targets
                SelectMultipleTargets(characters);
                break;
            default:
                Debug.LogError("Wrong Skill Type");
                break;
        }
    }

    public void HandleConfirm()
    {
        if (selectedPak != null && currentState == GameState.WAIT_FOR_CONFIRM)
        {

            float speed = selectedPak.currentSpeed;
            Skill skill = selectedPak.skill[selectedSkill];
            // Deep copy
            List<PakRender> skillTargets = new List<PakRender>(selectedTargets);
            ActionCommand newCommand = new ActionCommand(selectedPak, skill, skillTargets, speed);
            battleManger.AddCommand(newCommand);

            selectedPak.currentState = PakRender.State.InAction;
            selectedPak.DisplayInAction(true, selectedSkill);

            UpdateGameState(GameState.CHOOSE_CHARACTER);
        }

        if (selectedPak != null && currentState == GameState.CHAAM_WAIT_FOR_CONFIRM)
        {
            float speed = selectedPak.currentSpeed;
            Skill skill = cookSkill;
            // Deep copy
            List<PakRender> skillTargets = new List<PakRender>(selectedTargets);
            ActionCommand newCommand = new ActionCommand(selectedPak, skill, skillTargets, speed);
            battleManger.AddCommand(newCommand);

            selectedPak.currentState = PakRender.State.InAction;

            UpdateGameState(GameState.CHOOSE_CHARACTER);
        }


    }

    public void HandleBack()
    {
        if (currentState == GameState.DISPLAY_SKILL &&
              selectedPak != null)
        {

            UpdateGameState(GameState.CHOOSE_CHARACTER);
        }
        else if (currentState > GameState.CHOOSE_CHARACTER && currentState < GameState.END_TURN)
            UpdateGameState(GameState.CHOOSE_CHARACTER);
        return;
    }
    public void HandleEndturn()
    {
        CharacterManager.instance.ResetState(0);
        CharacterManager.instance.LockAllCharacters(true, 2);
        UpdateGameState(GameState.END_TURN);
        battleManger.RunCommand();
        Debug.Log("-----------------------------------------------------TURN END-----------------------------------------------");
        // //chaam must not dead
        // try
        // {
        //     ChaamRender nongChaam = GameObject.FindGameObjectWithTag("Chaam").transform.GetChild(0).GetComponent<ChaamRender>();
        //     nongChaam.addGuage(20);
        // }
        // catch (System.Exception)
        // {
        //     Debug.Log("Chaam is already tai ha.");
        // }
        List<PakRender> pakTeam = CharacterManager.instance.getTeamHolders(0);
        foreach (PakRender x in pakTeam)
        {
            if (x.CompareTag("Chaam") && x.healthSystem.CurrentHp>0){
                ChaamRender nongChaam = (ChaamRender)x;
                nongChaam.addGuage(20);
                break;
            }
        }
    }

    public void HandleCancelAction()
    {
        // Player presses cancel button
        if (currentState == GameState.DISPLAY_SKILL &&
            selectedPak.InAction())
        {
            // Remove action
            var commandHandler = battleManger.actionCommandHandler;
            commandHandler.RemoveAction(selectedPak.tag, selectedSkill);
            selectedPak.currentState = PakRender.State.Idle;
            selectedPak.DisplayInAction(false, selectedPak.setSkill);
            UpdateGameState(GameState.CHOOSE_CHARACTER);
        }
    }

    private void UpdateGameState(GameState state)
    {
        if (state == GameState.CHOOSE_CHARACTER)
        {
            var allys = CharacterManager.instance.getTeamHolders(0);
            foreach (var ally in allys)
            {
                if (ally.InAction())
                    ally.DisplayInAction(true);
            }
            Reset();
        }
        else if (state == GameState.CHOOSE_SKILL || state == GameState.DISPLAY_SKILL)
        {
            var allys = CharacterManager.instance.getTeamHolders(0);
            foreach (var ally in allys)
            {
                ally.DisplayInAction(false);
            }
        }

        _UIcontroller.UpdateUI(state);
        currentState = state;
        Debug.Log("Current State is " + currentState.ToString());
    }

    private void UpdateCharacterLayer(List<PakRender> characterList, bool value)
    {
        CharacterManager.instance.ResetState(3);

        if (characterList == null) return;

        foreach (var character in characterList)
        {
            character.GoToFrontLayer(value);
            character.GetComponent<BoxCollider2D>().enabled = value;
        }
    }

    private void SelectMultipleTargets(List<PakRender> paks, bool value = true)
    {
        foreach (var pak in paks)
        {
            pak.Selected = value;
            selectedTargets.Add(pak);
        }
    }

    private void Reset()
    {
        CharacterManager.instance.ResetState(2);
        CharacterManager.instance.ResetState(3);
        CharacterManager.instance.LockAllCharacters(false, 2);
        selectedPak = null;
        selectedTargets.Clear();

        if (selectedSkill > -1)
            _UIcontroller.skillMenu.ToggleSkill(selectedSkill);
        selectedSkill = -1;
    }

    private void HandleCookButton()
    {
        ChaamRender chaam = (ChaamRender)selectedPak;

        Debug.Log(chaam.getGuage());

        if (chaam.getGuage() == 100)
        {
            UpdateGameState(GameState.CHOOSE_COOK_SKILL);
        }
        else
        {
            Debug.Log("Guage not full");
        }

    }

    private void HandleSupportButton(int index)
    {
        if (_UIcontroller.tickCook[index].activeSelf)
        {
            ingredient.Remove(CharacterSelecter.instance.GetSupports()[index]._name);
        }
        else
        {
            ingredient.Add(CharacterSelecter.instance.GetSupports()[index]._name);
        }
        _UIcontroller.tickCook[index].SetActive(!_UIcontroller.tickCook[index].activeSelf);
        ingredient.Sort();
        cookSkill = _cookingController.OnIngredientClick(ingredient, comboPanel);

    }

    private void HandleStartCookButton()
    {
        characterSelectMode(cookSkill.ActionType);
        UpdateGameState(GameState.CHAAM_CHOOSE_TARGET);

        // _cookingController.OnStartCooking(ingredient, selectedPak, selectedTargets);
    }

}
