using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleLogic;
using Random = UnityEngine.Random;

namespace BattleScene {

    [RequireComponent(typeof(ActionCommandHandler))]
    public class BattleManager : MonoBehaviour {
        public static BattleManager instance;
        
        private ActionCommandHandler actionCommandHandler;

        public int currentTurn { get; private set; }

        public Text actionText;

        private void Awake() {
            if (instance == null) {
                instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        private void OnEnable() {
            ActionCommandHandler.OnComplete += NextTurn;
        }

        private void OnDisable() {
            ActionCommandHandler.OnComplete -= NextTurn;
        }
        // Start is called before the first frame update
        void Start() {
            currentTurn = 0;

            actionCommandHandler = GetComponent<ActionCommandHandler>();
            actionText.gameObject.SetActive(false);
        }


        public void RunCommand() {
            Debug.Log("Battle Start!!");
            /*TODO:
            * AI controller choose their action
            */
            actionText.gameObject.SetActive(true);
            actionCommandHandler.RunCommands();
        }

        public void NextTurn() {
            Debug.Log("Battle End!!");

            currentTurn++;
            actionText.gameObject.SetActive(false);
        }


        public void AddNewCommand(GameObject caller, string skillName, GameObject[] targets) {
            PakRender pakCaller = caller.GetComponent<PakRender>();

            List<PakRender> pakTargets = new List<PakRender>();
            foreach (var target in targets) {
                PakRender tmp = target.GetComponent<PakRender>();
                pakTargets.Add(tmp);
            }

            if (pakCaller == null || pakTargets.Count == 0) {
                Debug.Log("The caller or targets do not contain PakRender");
                return;
            }

            float speed = Random.Range(10.0f, 30.0f);
            ActionCommand newCommand = new ActionCommand(pakCaller, skillName, pakTargets, speed);
            actionCommandHandler.AddCommand(newCommand);
        }
    }
}