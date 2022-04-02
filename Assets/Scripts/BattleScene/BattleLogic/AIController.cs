using System.Collections;
using System.Collections.Generic;
// using System;
using UnityEngine;
using BattleScene.BattleLogic;

public class AIController
{
    public List<ActionCommand> selectAction(List<PakRender> pakTeam, List<PakRender> enemyTeam)
    { //pakTeam count the one that dead?
        List<ActionCommand> actionList = new List<ActionCommand>();


        foreach (PakRender e in enemyTeam)
        {
            List<PakRender> targets = new List<PakRender>();
            int target_num = Random.Range(0, pakTeam.Count);
            Debug.Log("The target num is " + target_num);
            var target = pakTeam[target_num];
            targets.Add(target);
            //ตีธรรมดา Only
            float speed = e.GetComponent<PakRender>().currentSpeed;
            Sprite skillIcon1 = Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite;
            ActionCommand action = new ActionCommand(e, new VanillaAttackOne("VA1", "AttackOneEnemy", "This do damage to one enemy.", 1, skillIcon1), targets, speed);
            actionList.Add(action);
        }

        return actionList;
    }
}