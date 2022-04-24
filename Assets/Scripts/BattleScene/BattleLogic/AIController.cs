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
            
            //Debug.Log("The target num is " + target_num);
            
            List<SkillExecutor> skills = e.getActiveSkill();
            int skillUseInt = Random.Range(0, skills.Count);
            SkillExecutor skillUse = skills[skillUseInt];
            Debug.Log("Skill enemy use is:"+skillUse.SkillName);
            if (skillUse.ActionType.Equals("TargetAllEnemies") ){
                foreach (PakRender pak in pakTeam){
                    targets.Add(pak);
                }
            }else if (skillUse.ActionType.Equals("TargetAllAlliances") ){
                foreach (PakRender pak in enemyTeam){
                    targets.Add(pak);
                }
            }else if (skillUse.ActionType.Equals("TargetWholeField")){
                foreach (PakRender pak in pakTeam){
                    targets.Add(pak);
                }
                foreach (PakRender pak in enemyTeam){
                    targets.Add(pak);
                }
            }
            else if(skillUse.ActionType.Equals("TargetOneEnemy")){
                int target_num = Random.Range(0, pakTeam.Count);
                Debug.Log("The target num is " + target_num);
                var target = pakTeam[target_num];
                targets.Add(target);
            }else if(skillUse.ActionType.Equals("TargetOneAlliance") ){
                int target_num = Random.Range(0, enemyTeam.Count);
                Debug.Log("The target num is " + target_num);
                var target = enemyTeam[target_num];
                targets.Add(target);
            }else{
                Debug.Log("------------------------Type fail----------------");
            }
            //ตีธรรมดา Only
            float speed = e.GetComponent<PakRender>().currentSpeed;
            ActionCommand action = new ActionCommand(e, skillUse, targets, speed);
            actionList.Add(action);
        }

        return actionList;
    }
}