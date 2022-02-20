using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleScene.BattleLogic;
using Random = UnityEngine.Random;
using BattleLogic;

namespace BattleScene
{
    /*
        *** Overview of this class ***
        - เป็น class ที่ทำการเชื่อม logic การทำงานของ PakSelection กับ ActionCommandHandler
        - ค่อยทำหน้าที่เก็บคลาส AI controller
    */
    [RequireComponent(typeof(ActionCommandHandler))]
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager instance;                   // singleton instance of this class

        private ActionCommandHandler actionCommandHandler;      // reference to ActionCommandHandler

        public int currentTurn { get; private set; }            // keep track how many turn have pass

        public Text actionText;                                 // text that show when in battle

        public AIController AI;                                 // reference to AI controller class

        // This ensure that the object of this class will be only one in the game.
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Subscribe OnComplete event
        // the event invokes went actionCommandHandler finish execute all of characters' actions.
        private void OnEnable()
        {
            ActionCommandHandler.OnComplete += NextTurn;

        }

        // Unsubscribe OnComplete event when this object disable from game.
        private void OnDisable()
        {
            ActionCommandHandler.OnComplete -= NextTurn;
        }

        // Start is called before the first frame update
        void Start()
        {
            currentTurn = 0;

            actionCommandHandler = GetComponent<ActionCommandHandler>();
            actionText.gameObject.SetActive(false);

            // Initialize AI
            AI = new AIController();
        }

        // Set game AI target and call RunCommands in actionCommandHandler.
        // This method call when a player clicks endTurn button.
        public void RunCommand()
        {
            Debug.Log("Battle Start!!");

            //---------------------------------New AI ---------------------------------------------//

            // Get list of pakTeam and enemy Team
            CharacterManager playerTeam = GameObject.Find("PlayerTeam").GetComponent<CharacterManager>();
            CharacterManager enemyTeam = GameObject.Find("EnemyTeam").GetComponent<CharacterManager>();
            List<CharacterManager.CharacterHolder> pakHolders = playerTeam.getHoldersList();
            List<CharacterManager.CharacterHolder> enemyHolders = enemyTeam.getHoldersList();

            List<GameObject> pakTeamObject = new List<GameObject>();
            List<GameObject> enemyTeamObject = new List<GameObject>();

            foreach (CharacterManager.CharacterHolder e in pakHolders)
            {
                if (e.character.activeSelf)
                {
                    pakTeamObject.Add(e.character);
                }
            }

            foreach (CharacterManager.CharacterHolder f in enemyHolders)
            {
                if (f.character.activeSelf)
                {
                    enemyTeamObject.Add(f.character);
                }
            }

            // Let AI controller selection its actions
            List<ActionCommand> temp = AI.selectAction(pakTeamObject, enemyTeamObject);
            foreach (ActionCommand e in temp)
            {
                actionCommandHandler.AddCommand(e);
            }

            actionText.gameObject.SetActive(true);
            actionCommandHandler.RunCommands();

            //****
        }

        // Go to next turn this method call when OnComplete event is triggered
        public void NextTurn()
        {
            Debug.Log("Battle End!!");

            currentTurn++;
            actionText.gameObject.SetActive(false);
        }

        public void AddNewCommand(GameObject caller, string skillName, GameObject[] targets)
        {
            PakRender pakCaller = caller.GetComponent<PakRender>();

            // Get PakRender component of each game oject in `targets`
            // and push it into pakTargets list.
            List<PakRender> pakTargets = new List<PakRender>();
            foreach (var target in targets)
            {
                PakRender tmp = target.GetComponent<PakRender>();
                if (tmp != null) pakTargets.Add(tmp);
            }

            // Log error when the caller does not exist or there are not available target.
            if (pakCaller == null || pakTargets.Count == 0)
            {
                Debug.LogError("The caller or targets do not contain PakRender");
                return;
            }

            // Set random speed for each action and initialize it.
            float speed = Random.Range(10.0f, 30.0f); //Need to fix this.
            ActionCommand newCommand = new ActionCommand(pakCaller, skillName, pakTargets, speed);
            actionCommandHandler.AddCommand(newCommand);
        }

        public bool IsPlayerLose()
        {
            Spawner spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
            List<CharacterManager.CharacterHolder> pakInBattle = spawner.playerTeam.getHoldersList();
            foreach (CharacterManager.CharacterHolder e in pakInBattle)
            {
                if (e.character.activeSelf)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsPlayerWin()
        {
            Spawner spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
            List<CharacterManager.CharacterHolder> enemyInBattle = spawner.enemyTeam.getHoldersList();
            foreach (CharacterManager.CharacterHolder e in enemyInBattle)
            {
                if (e.character.activeSelf)
                {
                    return false;
                }
            }
            return true;
        }
    }
}