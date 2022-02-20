using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using BattleScene;

using BattleScene.BattleLogic;

namespace BattleLogic
{

    public class ActionCommand : IComparable, ICommand
    {
        private float speed;                // field that determine the execution order of skills

        public PakRender caller;            // a index of the character calling this action
        public string selectedSkill;        // a index of the skill to be execute
        public List<PakRender> targets;     // indice of the allied targets

        // Constructor
        public ActionCommand(PakRender caller, string selectedSkill, List<PakRender> targets, float speed)
        {
            this.caller = caller;
            this.selectedSkill = selectedSkill;
            this.targets = targets;
            this.speed = speed;
        }

        // Getter Setter
        public float Speed
        {
            get { return speed; }
            set
            {
                if (value > 0)
                    speed = value;
            }
        }

        public void Execute(List<PakRender> diedThisTurn)
        {
            Debug.Log("Do Execute");
            // Mock execution of the skill
            String callerName = caller.pak.EntityName;

            List<string> targetNames = new List<string>();

            foreach (var target in targets)
            {
                string enemyName = "";
                enemyName += target.pak?.EntityName;
                targetNames.Add(enemyName);
            }
            // work with skill
            // caller.skill.AttackOneEnemy(targets[0], caller);

            // 
            // Debug.Log(caller.skill.GetType());
            // Type t = caller.skill.GetType();
            // T vskill = (T) caller.skill;
            Debug.Log(targets[0].name);
            Debug.Log(caller.name);

            if (caller.GetComponent<PakRender>().skill == null)
            {
                Debug.Log("caller.skill is null");
            }
            else
            {
                Debug.Log("caller.skill is not null");
            }

            PakRender caller2 = caller.GetComponent<PakRender>();
            PakRender target2 = targets[0].GetComponent<PakRender>();
            Debug.Log("Caller2 def:" + caller2.pak.Def);

            Skill callerskill = caller2.skill[0];


            VanillaAttackOne vskill = (VanillaAttackOne)callerskill;
            if (vskill == null)
            {
                Debug.Log("vskill is null");
            }
            else
            {
                Debug.Log(vskill.SkillId);
            }



            //? Yod
            if (target2.healthSystem.IsAlive)
            {
                vskill.AttackOneEnemy(target2, caller);
                caller2.moveToEnemy(caller2, target2);

            }

            string displaytext = string.Format("{0} calls {1} to {2}", callerName, selectedSkill, string.Join(", ", targetNames.ToArray()));
            Debug.Log(displaytext);

            //get CharManager to get array of char.
            Spawner spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();

            //Check death. If dead, the dead animation should be play
            if (!caller.healthSystem.IsAlive && !diedThisTurn.Contains(caller))
            {
                diedThisTurn.Add(caller);
                Debug.Log(caller.pak.EntityName + " is killed.");
                //Insert some death animation here.

                //delete char from list
                // character.RemoveCharacter(caller.plantpos.tag,true);

                //set disable is better
                if (!caller.healthSystem.IsAlive)
                {
                    GameObject callerGameObject = caller.gameObject;
                    callerGameObject.SetActive(false);
                }

                //*************************************
                // List<CharacterManager.CharacterHolder> pakInBattle = spawner.playerTeam.getHoldersList();
                // List<CharacterManager.CharacterHolder> enemyInBattle = spawner.enemyTeam.getHoldersList();

                // foreach (CharacterManager.CharacterHolder e in pakInBattle){
                //     PakRender temp = e.character.gameObject.GetComponent(typeof(PakRender)) as PakRender;
                //     Debug.Log("the moment of truth");
                //     Debug.Log(temp);
                //     if(!temp.healthSystem.IsAlive){
                //         //disable it
                //         e.character.SetActive(false);
                //     }
                // }

                // foreach (CharacterManager.CharacterHolder e in enemyInBattle){
                //     PakRender temp = e.character.gameObject.GetComponent(typeof(PakRender)) as PakRender;
                //     Debug.Log(temp);
                //     if(!temp.healthSystem.IsAlive){
                //         //disable it
                //         e.character.SetActive(false);
                //     }
                // }
                //*************************************
            }
            Debug.Log("wowza");
            foreach (PakRender e in targets)
            {
                Debug.Log(e.pak.EntityName);
                if (!e.healthSystem.IsAlive && !diedThisTurn.Contains(e))
                { //ตัว clone Pak ไม่มี health system. แต่ ตัว clone Chaam มีเฉย
                    diedThisTurn.Add(e);
                    Debug.Log(e.pak.EntityName + " is killed.");
                    //Insert some death animation here.

                    //delete char from list
                    // character.RemoveCharacter(e.plantpos.tag,false);

                    //set disable is better
                    GameObject targetGameObject = e.gameObject;
                    targetGameObject.SetActive(false);

                }
            }

        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            ActionCommand nextEvent = obj as ActionCommand;
            if (nextEvent != null)
            {
                return -(this.Speed.CompareTo(nextEvent.Speed));
            }
            else
            {
                throw new ArgumentException("Object doesn't have a property speed");
            }
        }


    }

}

