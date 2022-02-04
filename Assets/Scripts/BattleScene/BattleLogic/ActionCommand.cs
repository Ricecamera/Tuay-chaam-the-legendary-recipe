using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleLogic {

    public class ActionCommand : IComparable, ICommand {
        private float speed;                // field that determine the execution order of skills

        public PakRender caller;            // a index of the character calling this action
        public string selectedSkill;        // a index of the skill to be execute
        public List<PakRender> targets;     // indice of the allied targets

        // Constructor
        public ActionCommand(PakRender caller, string selectedSkill, List<PakRender> targets, float speed) {
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
            // Mock execution of the skill
            String callerName = caller.pak.EntityName;
            
            List<string> targetNames = new List<string>();

            foreach (var target in targets) {
                string enemyName = "";
                enemyName += target.pak?.EntityName;
                targetNames.Add(enemyName);
            }
            
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