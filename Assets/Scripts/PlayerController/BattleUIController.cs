using System;
using UnityEngine;
using UnityEngine.UI;
using BattleScene.BattleLogic;

public class BattleUIController : MonoBehaviour
{

    public SkillMenuUI skillMenu;

    public Button okButton, backButton, endTurnButton, cancelButton, cookButton;

    public GameObject supportMenu, tickCook1, tickCook2, tickCook3, comboPanel, support1, support2, support3;

    //Text
    public Text selectTargetText;
    public Text selectSkillText;

    [SerializeField]
    private GameObject Backdrop;

    private void OnEnable()
    {
        ActionCommandHandler.OnUpdateCommands += UpdateEndturnButton;
    }

    private void OnDisable()
    {
        ActionCommandHandler.OnUpdateCommands -= UpdateEndturnButton;
    }
    void Start()
    {
        //Set text during pak selection to not active
        selectTargetText.gameObject.SetActive(false);
        selectSkillText.gameObject.SetActive(false);
        endTurnButton.interactable = false;
    }

    public void UpdateUI(PakSelection.GameState nextState)
    {
        switch (nextState)
        {
            case PakSelection.GameState.CHOOSE_SKILL:
                selectSkillText.gameObject.SetActive(true);
                skillMenu.ToggleMenu(true);
                backButton.gameObject.SetActive(true);
                endTurnButton.gameObject.SetActive(false);
                break;
            case PakSelection.GameState.CHOOSE_CHAAM_SKILL:
                selectSkillText.gameObject.SetActive(true);
                skillMenu.ToggleMenu(true);
                backButton.gameObject.SetActive(true);
                endTurnButton.gameObject.SetActive(false);
                cookButton.gameObject.SetActive(true);
                break;
            case PakSelection.GameState.CHOOSE_COOK_SKILL:
                selectSkillText.gameObject.SetActive(true);
                skillMenu.ToggleMenu(true);
                backButton.gameObject.SetActive(true);
                endTurnButton.gameObject.SetActive(false);
                support1.gameObject.GetComponent<Image>().sprite = CharacterSelecter.instance.GetSupports()[0].uiDisplay;
                support2.gameObject.GetComponent<Image>().sprite = CharacterSelecter.instance.GetSupports()[1].uiDisplay;
                support3.gameObject.GetComponent<Image>().sprite = CharacterSelecter.instance.GetSupports()[2].uiDisplay;
                supportMenu.gameObject.SetActive(true);
                break;

            case PakSelection.GameState.CHOOSE_TARGET:
                selectSkillText.gameObject.SetActive(false);
                selectTargetText.gameObject.SetActive(true);
                backButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(false);
                Backdrop.SetActive(true);
                break;
            case PakSelection.GameState.WAIT_FOR_CONFIRM:
                okButton.gameObject.SetActive(true);
                break;
            case PakSelection.GameState.END_TURN:
                selectTargetText.gameObject.SetActive(false);
                endTurnButton.gameObject.SetActive(false);
                okButton.gameObject.SetActive(false);
                backButton.gameObject.SetActive(false);
                break;
            case PakSelection.GameState.DISPLAY_SKILL:
                skillMenu.ToggleMenu(true);
                backButton.gameObject.SetActive(true);
                endTurnButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(true);
                Backdrop.SetActive(true);
                break;
            default: // GameState.CHOOSE_CHARACTER
                skillMenu.ToggleMenu(false);
                backButton.gameObject.SetActive(false);
                okButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(false);
                endTurnButton.gameObject.SetActive(true);
                supportMenu.SetActive(false);
                Backdrop.SetActive(false);
                selectSkillText.gameObject.SetActive(false); //set select skill text to not actives
                selectTargetText.gameObject.SetActive(false); //set select target text to not actives
                cookButton.gameObject.SetActive(false);
                tickCook1.SetActive(false);
                tickCook2.SetActive(false);
                tickCook3.SetActive(false);
                comboPanel.SetActive(false);
                break;
        }
    }

    public void UpdateSkillMenuImage(PakRender ally)
    {
        try
        {
            skillMenu.UpdateImage(ally.Entity.Image, ally.GetSkills());
        }
        catch
        {
            Debug.LogError("the target character does not have PakRender component");
        }
    }

    private void UpdateEndturnButton(bool isEmpty)
    {
        endTurnButton.interactable = !isEmpty;
    }
}
