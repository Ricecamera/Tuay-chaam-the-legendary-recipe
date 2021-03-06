using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene.BattleLogic

{
    [Serializable]
    public class ActionCommand : IComparable, ICommand
    {
        private float speed;                // field that determine the execution order of skills

        public PakRender caller;            // a index of the character calling this action
        public SkillExecutor selectedSkill;        // a index of the skill to be execute
        public List<PakRender> targets;     // indice of the allied targets

        // Constructor

        public ActionCommand(PakRender caller, SkillExecutor selectedSkill, List<PakRender> targets, float speed)
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

        // Check if targets and the caller dies in the action
        public void CheckDead(List<PakRender> diedThisTurn)
        {
            //get CharManager to get array of char.
            //Spawner spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();

            //Check death. If dead, the dead animation should be play
            if (!caller.healthSystem.IsAlive && !diedThisTurn.Contains(caller))
            {
                diedThisTurn.Add(caller);
                Debug.Log(caller.Entity.EntityName + " is killed.");
                /* TODO: Insert some death animation here. */


                //set disable is better
                if (!caller.healthSystem.IsAlive)
                {
                    GameObject callerGameObject = caller.gameObject;
                    callerGameObject.SetActive(false);
                }
            }

            foreach (PakRender e in targets)
            {
                Debug.Log(e.Entity.EntityName);
                if (!e.healthSystem.IsAlive && !diedThisTurn.Contains(e))
                { //????????? clone Pak ??????????????? health system. ????????? ????????? clone Chaam ???????????????
                    diedThisTurn.Add(e);
                    Debug.Log(e.Entity.EntityName + " is killed.");
                    /* TODO: Insert some death animation here. */

                    //set disable is better
                    GameObject targetGameObject = e.gameObject;
                    targetGameObject.SetActive(false);
                }
            }

        }

        public void Execute(Action onComplete)
        {

            PakRender caller2 = caller.GetComponent<PakRender>();

            bool pass = false;
            // if the selected skill is attackWholefield, do it !!
            if (selectedSkill.SkillId.CompareTo("B:)") == 0)
                pass = true;
            else
                // if not check if some targets are alive
                foreach (var target in targets)
                {
                    if (target.healthSystem.IsAlive)
                    {
                        pass = true;
                        break;
                    }
                }

            if (pass)
            {
                // Invoke Oncomplete and unsubscribe FinishExecute event
                Action finishCallback = () =>
                {
                    onComplete();
                    selectedSkill.OnFinishExecute -= onComplete;
                };

                // Subscribe to skill event
                selectedSkill.OnFinishExecute += finishCallback;

                //dynamic skill call
                selectedSkill?.performSkill(targets, caller2);

            }
            else
            {
                Debug.Log("The target is already died.");
                onComplete();
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

        public int convertSelectedSkillToIndex()
        {
            for (int i = 0; i < caller.skills.Length; i++)
            {
                if (caller.skills[i].skillId == selectedSkill.SkillId)
                {
                    return i;
                }
            }
            Debug.Log("False index in skill");
            return -1;
        }
    }
}

