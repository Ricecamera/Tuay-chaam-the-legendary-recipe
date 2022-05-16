using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BattleScene.BattleLogic;
using BattleScene;
using UnityEngine.EventSystems;

public class HistoryRecord : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private List<ActionCommand> commands;
    public BattleManager battleManager;
    public static HistoryRecord instance;

    public TMPro.TMP_FontAsset font;

    private void Awake()
    {
        instance = this;
    }

    private GameObject actor;

    private GameObject target;

    private GameObject turn;

    private GameObject skill;
    [SerializeField] private GameObject scrollPane;

    private List<string> nameList = new List<string>(new string[] { "Actor", "Skill", "Target", "Turn", "All allies" , "All enemies" ,  "All units" });

    public void SetCommand(List<ActionCommand> commands)
    {
        this.commands = commands;
    }

    public void Show()
    {
        turn = new GameObject("Turn");
        turn.AddComponent<TextMeshProUGUI>();
        turn.GetComponent<TextMeshProUGUI>().text = "Turn " + (battleManager.currentTurn + 1);
        turn.GetComponent<TextMeshProUGUI>().fontStyle = TMPro.FontStyles.Bold;
        turn.GetComponent<TextMeshProUGUI>().fontSize = 30;
        turn.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
        turn.GetComponent<TextMeshProUGUI>().font = font;
        turn.GetComponent<RectTransform>().sizeDelta = new Vector2(111.0746f, 33.6339f);
        turn.transform.SetParent(scrollPane.transform);
        for (int i = 0; i < commands.Count; i++)
        {
            actor = new GameObject("Actor");
            actor.AddComponent<Image>();
            actor.GetComponent<Image>().sprite = commands[i].caller.Entity.image;
            actor.GetComponent<RectTransform>().sizeDelta = new Vector2(80f, 80f);
            actor.transform.SetParent(scrollPane.transform);
            if (commands[i].targets.Count == 1 ){
                target = new GameObject("Target");
                target.AddComponent<Image>();
                target.GetComponent<Image>().sprite = commands[i].targets[0].Entity.image;
                target.GetComponent<RectTransform>().sizeDelta = new Vector2(80f, 80f);
                target.GetComponent<RectTransform>().localPosition = new Vector3(113.099998f, 0f, 0f);
                target.transform.SetParent(actor.transform);
            }
            else if (commands[i].targets.Count > 1 && commands[i].targets.Count <= 4){
                if ( commands[i].selectedSkill.ActionType.Contains("Alliances"))
                {
                    Debug.Log("Do all allies");
                    target = new GameObject("All allies");
                    target.AddComponent<TextMeshProUGUI>();
                    target.GetComponent<TextMeshProUGUI>().text = "All allies";
                }
                else
                {
                    Debug.Log("Do all enemies");
                    target = new GameObject("All enemies");
                    target.AddComponent<TextMeshProUGUI>();
                    target.GetComponent<TextMeshProUGUI>().text = "All enemies";
                }
                
                target.GetComponent<RectTransform>().localPosition = new Vector3(113.099998f, 0f, 0f);
                target.GetComponent<TextMeshProUGUI>().fontSize = 22;
                target.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                target.GetComponent<TextMeshProUGUI>().font = font;
                target.GetComponent<RectTransform>().sizeDelta = new Vector2(111.0746f, 33.6339f);
                target.transform.SetParent(actor.transform);
            }
            else if (commands[i].targets.Count >= 5){
                Debug.Log("Do all units");
                target = new GameObject("All units"); 
                target.AddComponent<TextMeshProUGUI>(); 
                target.GetComponent<TextMeshProUGUI>().text = "All units";
                target.GetComponent<RectTransform>().localPosition = new Vector3(113.099998f, 0f, 0f);
                target.GetComponent<TextMeshProUGUI>().fontSize = 22;
                target.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                target.GetComponent<TextMeshProUGUI>().font = font;
                target.GetComponent<RectTransform>().sizeDelta = new Vector2(111.0746f, 33.6339f);
                target.transform.SetParent(actor.transform);
            }
            
            skill = new GameObject("Skill");
            skill.AddComponent<Image>();
            skill.GetComponent<Image>().sprite = commands[i].selectedSkill.GetSkill().icon;
            skill.GetComponent<RectTransform>().localPosition = new Vector3(53f, -1.71659995e-05f, 0f);
            skill.GetComponent<RectTransform>().sizeDelta = new Vector2(32.0287f, 32.0287f);
            skill.transform.SetParent(actor.transform);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //* eventData == "Actor", "Skill", "Target", "Turn"
        Debug.Log("Pointer at : " + eventData.pointerCurrentRaycast.gameObject.name);
        if (!nameList.Contains(eventData.pointerCurrentRaycast.gameObject.name))
        {
            gameObject.SetActive(false);
            HistoryButton.chooseFlag = true;
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
