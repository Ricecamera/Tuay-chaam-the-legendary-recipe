using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleScene.BattleLogic;
using BattleScene;
using UnityEngine.SceneManagement;

public class BattleUIController : MonoBehaviour
{

    public SkillMenuUI skillMenu;

    public Button okButton, backButton, endTurnButton, cancelButton, cookButton, startCookButton;

    public Button[] supportButton;

    public GameObject[] tickCook;

    public GameObject supportMenu, comboPanel, speedPanel;

    public GameObject[] speed;

    //Text
    public Text selectTargetText;
    public Text selectSkillText;

    [SerializeField]
    private GameObject Backdrop;

    public Image cookImageDark;

    private PakSelection _pakSelection;




    [SerializeField]
    private BattleManager battleManager;

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
        cookImageDark.color = UnityEngine.Color.gray;
        cookImageDark.fillAmount = 0f;
        _pakSelection = GetComponent<PakSelection>();

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
                cookButton.gameObject.SetActive(false);
                break;
            case PakSelection.GameState.CHOOSE_CHAAM_SKILL:
                selectSkillText.gameObject.SetActive(true);
                skillMenu.ToggleMenu(true);
                backButton.gameObject.SetActive(true);
                endTurnButton.gameObject.SetActive(false);
                List<int> noCookLevel = new List<int>() { 1, 2, 3 };
                if (SceneManager.GetActiveScene().name == "Battle1-2V2" || noCookLevel.Contains(LevelManager.instance.thislevel))
                {
                    cookButton.gameObject.SetActive(false);
                }
                else
                {
                    cookButton.gameObject.SetActive(true);
                }
                List<PakRender> pakTeam = CharacterManager.instance.GetAliveCharacters(0);
                foreach (PakRender x in pakTeam)
                {
                    if (x.CompareTag("Chaam"))
                    {
                        ChaamRender nongChaam = (ChaamRender)x;
                        if (nongChaam.getGuage() >= 100)
                        {
                            cookImageDark.gameObject.SetActive(false);
                            cookButton.enabled = true;
                        }
                        else
                        {
                            cookImageDark.fillAmount = 1f - (float)nongChaam.getGuage() / 100f;
                            cookImageDark.gameObject.SetActive(true);
                            cookButton.enabled = false;
                        }
                        break;
                    }
                }
                break;
            case PakSelection.GameState.CHOOSE_COOK_SKILL:
                skillMenu.skills[0].getMyButton().GetComponent<Image>().color = UnityEngine.Color.gray;
                skillMenu.skills[1].getMyButton().GetComponent<Image>().color = UnityEngine.Color.gray;
                skillMenu.skills[2].getMyButton().GetComponent<Image>().color = UnityEngine.Color.gray;
                selectSkillText.gameObject.SetActive(true);
                skillMenu.ToggleMenu(true);
                backButton.gameObject.SetActive(true);
                endTurnButton.gameObject.SetActive(false);
                // for (int i = 0; i < supportButton.Length; i++)
                // {
                //     supportButton[i].gameObject.GetComponent<Image>().sprite = CharacterSelecter.instance.GetSupports()[i].uiDisplay;
                // }
                for (int i = 0; i < supportButton.Length; i++)
                {
                    if (i < CharacterSelecter.instance.GetSupports().Count)
                    {
                        supportButton[i].gameObject.GetComponent<Image>().sprite = CharacterSelecter.instance.GetSupports()[i].uiDisplay;
                    }
                    else
                    {
                        supportButton[i].gameObject.SetActive(false);
                    }

                }

                supportMenu.gameObject.SetActive(true);
                speedPanel.gameObject.SetActive(false);
                break;

            case PakSelection.GameState.CHOOSE_TARGET:
                selectSkillText.gameObject.SetActive(false);
                selectTargetText.gameObject.SetActive(true);
                backButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(false);
                Backdrop.SetActive(true);
                cookButton.gameObject.SetActive(false);
                break;
            case PakSelection.GameState.CHAAM_CHOOSE_TARGET:
                selectSkillText.gameObject.SetActive(false);
                selectTargetText.gameObject.SetActive(true);
                backButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(false);
                Backdrop.SetActive(true);
                cookButton.gameObject.SetActive(false);
                break;
            case PakSelection.GameState.WAIT_FOR_CONFIRM:
                okButton.gameObject.SetActive(true);
                break;
            case PakSelection.GameState.CHAAM_WAIT_FOR_CONFIRM:
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
                if (_pakSelection.SelectedPak.CompareTag("Chaam"))
                {
                    if (battleManager.actionCommandHandler == null)
                    {
                    }
                    else if (battleManager.actionCommandHandler.isChaamThisTurnUseCookSkill())
                    {
                        cookButton.gameObject.GetComponent<Image>().color = UnityEngine.Color.gray;
                        cookButton.enabled = false;
                    }
                    else
                    {
                        cookButton.gameObject.GetComponent<Image>().color = UnityEngine.Color.white;
                    }

                    if (SceneManager.GetActiveScene().name == "Battle1-2V2")
                    {
                        cookButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        cookButton.gameObject.SetActive(true);
                    }
                    // cookButton.gameObject.SetActive(true);
                }
                else
                {
                    cookButton.gameObject.SetActive(false);
                }

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
                foreach (GameObject tick in tickCook)
                {
                    tick.SetActive(false);
                }

                comboPanel.SetActive(false);
                TooltipScreenSpaceUI.hideTooltip_Static();
                skillMenu.skills[0].getMyButton().GetComponent<Image>().color = UnityEngine.Color.white;
                skillMenu.skills[1].getMyButton().GetComponent<Image>().color = UnityEngine.Color.white;
                skillMenu.skills[2].getMyButton().GetComponent<Image>().color = UnityEngine.Color.white;
                cookButton.gameObject.GetComponent<Image>().color = UnityEngine.Color.white;

                if (SceneManager.GetActiveScene().name != "Battle1-2V2")
                {
                    UpdateSpeedPanel();
                    speedPanel.gameObject.SetActive(true);
                }
                break;
        }
    }

    public void UpdateSkillMenuImage(PakRender ally)
    {
        try
        {
            skillMenu.UpdateImage(ally.Entity.Image, ally.GetSkillExecutors());
        }
        catch
        {
        }
    }

    private void UpdateEndturnButton(bool isEmpty)
    {
        endTurnButton.interactable = !isEmpty;
    }

    private void UpdateSpeedPanel()
    {
        List<PakRender> aliveCharacter = CharacterManager.instance.GetSpeedOfCharacters();
        for (int i = 0; i < speed.Length; i++)
        {
            if (i < aliveCharacter.Count)
            {
                speed[i].GetComponent<Image>().sprite = aliveCharacter[i].Entity.image;
                if (CharacterManager.IsEnemyTeam(aliveCharacter[i].tag))
                {
                    speed[i].transform.GetChild(0).gameObject.SetActive(true);
                    speed[i].transform.GetChild(1).gameObject.SetActive(false);
                }
                else if (CharacterManager.IsPlayerTeam(aliveCharacter[i].tag))
                {
                    speed[i].transform.GetChild(0).gameObject.SetActive(false);
                    speed[i].transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogError("Error");
                }
            }
            else
            {
                speed[i].SetActive(false);
            }
        }
    }
}
