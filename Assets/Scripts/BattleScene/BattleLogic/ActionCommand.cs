using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene.BattleLogic {

    public class ActionCommand : IComparable , ICommand{
        private float speed;                // field that determine the execution order of skills

        public PakRender caller;            // a index of the character calling this action
        public int selectedSkill;        // a index of the skill to be execute
        public List<PakRender> targets;     // indice of the allied targets

        // Constructor
        public ActionCommand(PakRender caller, int selectedSkill, List<PakRender> targets, float speed) {
            this.caller = caller;
            this.selectedSkill = selectedSkill;
            this.targets = targets;
            this.speed = speed;
        }

        // Getter Setter
        public float Speed {
            get { return speed; }
            set {
                if (value > 0)
                    speed = value;
            }
        }

        public void Execute() {
            Debug.Log("Do Execute");
            // Mock execution of the skill
            String callerName = caller.pak.EntityName;
            
            List<string> targetNames = new List<string>();

            foreach (var target in targets) {
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

            if(caller.GetComponent<PakRender>().skill == null){
                Debug.Log("caller.skill is null");
            }else{
                Debug.Log("caller.skill is not null");
            }

            PakRender caller2 = caller.GetComponent<PakRender>();
            PakRender target2 = targets[0].GetComponent<PakRender>();
            Debug.Log("Caller2 def:"+caller2.pak.Def);

            Skill callerskill = caller2.skill;
            

            VanillaAttackOne vskill = (VanillaAttackOne) callerskill;
            if(vskill == null){
                Debug.Log("vskill is null");
            }else{
                Debug.Log(vskill.SkillId);
            }
            
            vskill.AttackOneEnemy(target2, caller);

            string displaytext = string.Format("{0} calls {1} to {2}", callerName, selectedSkill, string.Join(", ", targetNames.ToArray()));
            Debug.Log(displaytext);
            
        }

        public int CompareTo(object obj) {
            if (obj == null) return 1;
            ActionCommand nextEvent = obj as ActionCommand;
            if (nextEvent != null) {
                return -(this.Speed.CompareTo(nextEvent.Speed));
            }
            else {
                throw new ArgumentException("Object doesn't have a property speed");
            }
        }
    }
}