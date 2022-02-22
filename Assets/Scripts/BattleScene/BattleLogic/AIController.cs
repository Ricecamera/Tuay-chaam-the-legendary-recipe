using System.Collections;
using System.Collections.Generic;
// using System;
using UnityEngine;
using BattleScene.BattleLogic;

public class AIController
{
    public List<ActionCommand> selectAction(List<GameObject> pakTeam, List<GameObject> enemyTeam)
    { //pakTeam count the one that dead?
        List<ActionCommand> actionList = new List<ActionCommand>();


        foreach (GameObject e in enemyTeam)
        {
            List<PakRender> targets = new List<PakRender>();
            int target_num = Random.Range(0, pakTeam.Count);
            Debug.Log("The target num is " + target_num);
            GameObject target = pakTeam[target_num];
            targets.Add(target.GetComponent<PakRender>());
            //ตีธรรมดา Only
            float speed = e.GetComponent<PakRender>().currentSpeed;
            ActionCommand action = new ActionCommand(e.GetComponent<PakRender>(), 0, targets, speed);
            actionList.Add(action);
        }

        return actionList;
    }
}