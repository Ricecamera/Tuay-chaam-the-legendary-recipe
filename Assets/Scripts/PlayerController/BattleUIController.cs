using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour {

    public SkillMenuUI skillMenu;

    public Button okButton, backButton, endTurnButton, cancelButton;

    public GameObject supportMenu;

    //Text
    public Text selectTargetText;
    public Text selectSkillText;

    [SerializeField]
    private GameObject Backdrop;

    void Start() {
        //Set text during pak selection to not active
        selectTargetText.gameObject.SetActive(false);
        selectSkillText.gameObject.SetActive(false);
    }

    public void UpdateUI(PakSelectionV3.GameState nextState) {
        switch (nextState) {
            case PakSelectionV3.GameState.CHOOSE_SKILL:
                selectSkillText.gameObject.SetActive(true);
                skillMenu.ToggleMenu(true);
                backButton.gameObject.SetActive(true);
                endTurnButton.gameObject.SetActive(false);
                Backdrop.SetActive(true);
                break;
            case PakSelectionV3.GameState.CHOOSE_TARGET:
                selectSkillText.gameObject.SetActive(false);
                backButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(false);
                Backdrop.SetActive(true);
                break;
            case PakSelectionV3.GameState.WAIT_FOR_CONFIRM:
                selectTargetText.gameObject.SetActive(false);
                break;
            case PakSelectionV3.GameState.END_TURN:
                endTurnButton.gameObject.SetActive(false);
                okButton.gameObject.SetActive(false);
                backButton.gameObject.SetActive(false);
                break;
            case PakSelectionV3.GameState.DISPLAY_SKILL:
                break;
            default:
                skillMenu.ToggleMenu(false);
                backButton.gameObject.SetActive(false);
                okButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(false);
                endTurnButton.gameObject.SetActive(true);
                supportMenu.SetActive(false);
                Backdrop.SetActive(false);
                selectSkillText.gameObject.SetActive(false); //set select skill text to not actives
                selectTargetText.gameObject.SetActive(false); //set select target text to not actives
                break;
        }
    }

    public void UpdateSkillMenuImage(PakRender ally) {
        try {
            skillMenu.UpdateImage(ally.Entity.Image, ally.skill);
        }
        catch {
            Debug.LogError("the target character does not have PakRender component");
        }
    }

}
