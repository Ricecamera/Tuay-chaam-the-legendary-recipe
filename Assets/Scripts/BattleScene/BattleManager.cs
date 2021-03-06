using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleScene.BattleLogic;

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

        public int currentTurn { get; private set; }            // keep track how many turn have pass

        public Text actionText;                                 // text that show when in battle

        public AIController AI;                                 // reference to AI controller class

        public ActionCommandHandler actionCommandHandler { get; private set; }       // reference to ActionCommandHandler

        private Action onChangeTurn;

        // Subscribe OnComplete event
        // the event invokes went actionCommandHandler finish execute all of characters' actions.
        private void OnEnable()
        {
            ActionCommandHandler.OnComplete += NextTurn;
        }

        // Unsubscribe OnComplete event
        // the event when this object disable from game.
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
            List<PakRender> alivePak = CharacterManager.instance.GetAliveCharacters(0);
            List<PakRender> aliveEnemies = CharacterManager.instance.GetAliveCharacters(1);





            // Let AI controller selection its actions
            List<ActionCommand> temp = AI.selectAction(alivePak, aliveEnemies);
            foreach (ActionCommand e in temp)
            {
                actionCommandHandler.AddCommand(e);
            }

            actionText.gameObject.SetActive(true);
            actionCommandHandler.RunCommands();
            //* borrow command to make history record
            HistoryRecord.instance.SetCommand(actionCommandHandler.GetCommands());

            //****
        }

        public void AddCommand(ActionCommand command)
        {
            actionCommandHandler.AddCommand(command);
        }

        // Go to next turn this method call when OnComplete event is triggered
        public void NextTurn()
        {
            Debug.Log("Battle End!!");

            currentTurn++;
            actionText.gameObject.SetActive(false);
            List<PakRender> holders = CharacterManager.instance.GetAliveCharacters(2);
            foreach (var pak in holders)
                pak.UpdateTurn();
            onChangeTurn();

        }

        public bool IsPlayerLose()
        {
            List<PakRender> pakInBattle = CharacterManager.instance.GetAliveCharacters(0);
            return pakInBattle.Count == 0;
        }

        public bool IsPlayerWin()
        {
            List<PakRender> enemyInBattle = CharacterManager.instance.GetAliveCharacters(1);
            List<PakRender> pakInBattle = CharacterManager.instance.getTeamHolders(0);
            SaveManager.instance.SetZeroDieCount();  //* reset dieCount

            foreach (PakRender e in pakInBattle)
            {
                if (!e.gameObject.activeSelf)
                {
                    SaveManager.instance.AddDieCount();
                }
            }
            return enemyInBattle.Count == 0;
        }

        public void SetChangeTurn(Action ToExecute)
        {
            onChangeTurn = ToExecute;
        }
    }
}