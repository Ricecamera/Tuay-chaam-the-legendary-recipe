using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleScene.BattleLogic;
using Random = UnityEngine.Random;
using BattleLogic;

namespace BattleScene {

    [RequireComponent(typeof(ActionCommandHandler))]
    public class BattleManager : MonoBehaviour {
        public static BattleManager instance;
        
        private ActionCommandHandler actionCommandHandler;

        public int currentTurn { get; private set; }

        public Text actionText;

        public AIController AI ;

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
            AI = new AIController();
        }


        public void RunCommand() {
            Debug.Log("Battle Start!!");
            /*TODO:
            * AI controller choose their action
            */
            //  if (pakSelection.IsPlayerConfirmed()) {
            //     /*TODO:
            //         * AI controller choose their action 
            //         1.get ว่าฝั่งผัก กับ AI เหลืออยู่ฝั่งละกี่ตัว(เอามาในรูปPakrender)
            //         2.for loop แต่ละตัวของฝั่ง AI เพื่อสร้าง ActionCommand ทีละตัวยัดใส่ list (Random ตัวตีเลย) {not this...แต่ห้ามตีตัวที่เลือดหมดแล้ว} !!!!
            //         3.return list ออกมา
            //         4.เอา list ที่ออกมายัดใส่ actionCommandHandler
            //     */
            //     pakTeam = pakSelection.getPakTeam();
            //     enemyTeam = pakSelection.getEnemyTeam();
            //     List<ActionCommand> temp = AI.selectAction(pakTeam,enemyTeam);
            //     foreach (ActionCommand e in temp){
            //         actionCommandHandler.getCommandList().Add(e);
            //     }
                            
                
            //     state = GameState.BATTLE_PHASE;
                
            //     state = GameState.BATTLE_PHASE;
            //     actionCommandHandler.RunCommands();

            //---------------------------------New AI
            CharacterManager character = GameObject.Find("Characters").GetComponent<CharacterManager>();
            List<CharacterHolder> pakTeam = character.getPakTeam();
            List<CharacterHolder> enemyTeam = character.getEnemyTeam();
            
            List<GameObject> pakTeamObject = new List<GameObject>();
            List<GameObject> enemyTeamObject = new List<GameObject>();

            foreach(CharacterHolder e in pakTeam){
                pakTeamObject.Add(e.character);
            }

            foreach(CharacterHolder f in enemyTeam){
                enemyTeamObject.Add(f.character);
            }

            List<ActionCommand> temp = AI.selectAction(pakTeamObject,enemyTeamObject);
            foreach (ActionCommand e in temp){
                actionCommandHandler.getCommandList().Add(e);
            }
            
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

            float speed = Random.Range(10.0f, 30.0f); //Need to fix this.
            ActionCommand newCommand = new ActionCommand(pakCaller, skillName, pakTargets, speed);
            actionCommandHandler.AddCommand(newCommand);
        }
    }
}