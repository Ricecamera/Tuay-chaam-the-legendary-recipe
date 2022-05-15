using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BattleScene.BattleLogic;
using BattleScene;

public class HistoryRecord : MonoBehaviour
{
    private List<ActionCommand> commands;
    public BattleManager battleManager;
    public static HistoryRecord instance;

    private void Awake()
    {
        instance = this;
    }

    private GameObject action;
    private GameObject turn;

    private GameObject skill;
    [SerializeField] private GameObject scrollPane;

    public void SetCommand(List<ActionCommand> commands)
    {
        this.commands = commands;
    }

    public void Show()
    {
        turn = new GameObject();
        turn.AddComponent<TextMeshProUGUI>();
        turn.GetComponent<TextMeshProUGUI>().text = "Turn " + battleManager.currentTurn;
        turn.transform.SetParent(scrollPane.transform);
        for (int i = commands.Count - 1; i >= 0; i--)
        {
            action = new GameObject();
            action.AddComponent<Image>();
            action.GetComponent<Image>().sprite = commands[i].caller.Entity.image;
            action.transform.SetParent(turn.transform);
            skill = new GameObject();
            skill.AddComponent<Image>();
            skill.GetComponent<Image>().sprite = commands[i].selectedSkill.GetSkill().icon;
            skill.GetComponent<RectTransform>().localPosition = new Vector3(24.2000008f, -33.9860001f, 0f);
            skill.GetComponent<RectTransform>().sizeDelta = new Vector2(32.0287f, 32.0287f);
            skill.transform.SetParent(action.transform);
        }
    }
}
