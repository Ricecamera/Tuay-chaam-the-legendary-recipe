using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace BattleLogic {
    public class ActionCommandHandler : MonoBehaviour {

        public static event Action OnComplete;

        [SerializeField]
        private List<ActionCommand> commandList = new List<ActionCommand>();
        

        private IEnumerator DoActionOverTime() {
            foreach (var action in commandList) {
                action.Execute();
                yield return new WaitForSeconds(1f);
            }

            commandList.Clear();
            OnComplete?.Invoke();
        }

        public void AddCommand(ICommand command) {
            commandList.Add(command as ActionCommand);
        }

        // Execute all commands order by command's speed;
        public void RunCommands() {
            commandList.Sort();
            StartCoroutine(DoActionOverTime());
        }


        // Get the first selected selected action by specific character's tag in scene
        public ActionCommand GetAction(string gameTag) {
            // Remove the first skill name skillName called by the character
            foreach (ActionCommand action in commandList) {
                if (action.caller.CompareTag(gameTag)) {
                    return action;
                }
            }
            return null;
        }

        // Get the first selected action by specific character's tag and skill's name
        public ActionCommand GetAction(string gameTag, string skillName) {
            foreach (ActionCommand action in commandList) {
                if (action.caller.CompareTag(gameTag)
                    && action.selectedSkill.Equals(skillName)) {
                    return action;
                }
            }
            return null;
        }

        // Remove the first skill called by the character and having name as 'skillName`
        public ActionCommand RemoveAction(string gameTag) {
            for (int i = 0; i < commandList.Count; i++) {
                ActionCommand action = commandList[i];
                if (action.caller.CompareTag(gameTag)) {
                    commandList.RemoveAt(i);
                    return action;
                }
            }
            return null;
        }

        public ActionCommand RemoveAction(string gameTag, string skillName) {
            for (int i = 0; i < commandList.Count; i++) {
                ActionCommand action = commandList[i];
                if (action.caller.CompareTag(gameTag)) {
                    commandList.RemoveAt(i);
                    return action;
                }
            }
            return null;
        }
    }
}
