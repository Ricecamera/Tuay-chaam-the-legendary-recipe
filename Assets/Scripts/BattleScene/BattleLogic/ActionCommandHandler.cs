using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace BattleScene.BattleLogic
{
    public class ActionCommandHandler : MonoBehaviour
    {

        public static event Action OnComplete;          // this event invoke when `DoActionOverTIme` is called and completed

        [SerializeField]
        private List<ActionCommand> commandList = new List<ActionCommand>();    // temporary list that store all of actions to be executed

        public List<PakRender> diedThisTurn; //store PakRender who died this turn.


        // thread for executing each action in `commandList`
        private IEnumerator DoActionOverTime()
        {
            // list storing characters which have died this turn
            List<PakRender> diedThisTurn = new List<PakRender>();

            foreach (var action in commandList)
            {
                Debug.Log(action.caller);
                Debug.Log(action.caller.healthSystem);
                Debug.Log(action.caller.healthSystem.CurrentHp);
                //ตัว clone Pak ไม่มี health system. แต่ ตัว clone Chaam มีเฉย
                // PakRender temp = action.caller.GetComponent<PakRender>();
                // Debug.Log(temp.pak);
                // Debug.Log(temp.healthSystem.GetHealth());
                Debug.Log("Yes");

                //If the caller is alive, the skill is casted.
                if (action.caller.healthSystem.IsAlive)
                {   

                    // wait for action to finishes
                    bool finish = false;
                    action.Execute(() => {finish = true;});
                    do {
                        // if the action isn't finish wait for the next frame
                        yield return null;
                    } while (!finish);
                    action.CheckDead(diedThisTurn);
                }
                else
                {
                    // the target is dead
                    string callerName = action.caller.Pak.EntityName;
                    Debug.Log("The " + callerName + " is already dead.");
                }
                yield return null; // wait for the next frame
            }

            // Clear commandList
            commandList.Clear();
            OnComplete?.Invoke();
        }

        // Add a new command to `commandList`
        public void AddCommand(ICommand command)
        {
            commandList.Add(command as ActionCommand);
        }

        // Execute all commands order by command's speed;
        public void RunCommands()
        {
            commandList.Sort();
            StartCoroutine(DoActionOverTime());
        }


        // Get the first selected selected action by specific character's tag in scene
        public ActionCommand GetAction(string gameTag)
        {
            // Remove the first skill name skillName called by the character
            foreach (ActionCommand action in commandList)
            {
                if (action.caller.CompareTag(gameTag))
                {
                    return action;
                }
            }
            return null;
        }

        // Get the first selected action by specific character's tag and skill's name
        public ActionCommand GetAction(string gameTag, string skillName)
        {
            foreach (ActionCommand action in commandList)
            {
                if (action.caller.CompareTag(gameTag)
                    && action.selectedSkill.Equals(skillName))
                {
                    return action;
                }
            }
            return null;
        }

        // Remove the first skill that is called by the character with provided gameTag.
        public ActionCommand RemoveAction(string gameTag)
        {
            for (int i = 0; i < commandList.Count; i++)
            {
                ActionCommand action = commandList[i];
                if (action.caller.CompareTag(gameTag))
                {
                    commandList.RemoveAt(i);
                    return action;
                }
            }
            return null;
        }

        // Remove the specific skill that is called by the character with provided gameTag.
        public ActionCommand RemoveAction(string gameTag, string skillName)
        {
            for (int i = 0; i < commandList.Count; i++)
            {
                ActionCommand action = commandList[i];
                if (action.caller.CompareTag(gameTag))
                {
                    commandList.RemoveAt(i);
                    return action;
                }
            }
            return null;
        }
    }
}
