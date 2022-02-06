// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TasteSystem;
// using BattleLogic;

// [RequireComponent(typeof(ActionCommandHandler))]
// public class BattleManagerTemp : MonoBehaviour
// {
//     private ActionCommandHandler actionCommandHandler;

//     public enum GameState { START, PLANNING_PHASE, BATTLE_PHASE, END_PHASE, GAME_OVER}
//     public int currentTurn {get; private set; }
//     public GameState state { get; private set;}

//     public PakSelection pakSelection;
//     public Text actionText;

//     public List<GameObject> pakTeam, enemyTeam;

//     private AIController AI;

//     // OnEnable call before start
//     private void OnEnable() {
//         actionCommandHandler = GetComponent<ActionCommandHandler>();
//         actionText.gameObject.SetActive(false);
//         AI = new AIController();
//     }

//     // Start is called before the first frame update
//     void Start()
//     {
//         currentTurn = 0;
//         state = GameState.START;
//         /*TODO:
//          * Get characters data from character selection scene
//          * Spawn characters for both teams
//         */

//         StartCoroutine(SetDelayState(GameState.PLANNING_PHASE, 1f));
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         switch(state) {
//             case GameState.PLANNING_PHASE:
//                 // check if the player has confirmed action
//                 if (pakSelection.IsPlayerConfirmed()) {
//                     /*TODO:
//                      * AI controller choose their action 
//                      1.get ว่าฝั่งผัก กับ AI เหลืออยู่ฝั่งละกี่ตัว(เอามาในรูปPakrender)
//                      2.for loop แต่ละตัวของฝั่ง AI เพื่อสร้าง ActionCommand ทีละตัวยัดใส่ list (Random ตัวตีเลย) {not this...แต่ห้ามตีตัวที่เลือดหมดแล้ว} !!!!
//                      3.return list ออกมา
//                      4.เอา list ที่ออกมายัดใส่ actionCommandHandler
//                     */
//                     pakTeam = pakSelection.getPakTeam();
//                     enemyTeam = pakSelection.getEnemyTeam();
//                     List<ActionCommand> temp = AI.selectAction(pakTeam,enemyTeam);
//                     foreach (ActionCommand e in temp){
//                         actionCommandHandler.getCommandList().Add(e);
//                     }
                             
                    
//                     state = GameState.BATTLE_PHASE;
                    
//                     state = GameState.BATTLE_PHASE;
//                     actionCommandHandler.RunCommands();
//                     //StartCoroutine(ExecuteActions());
//                 }
//                 break;
//             case GameState.BATTLE_PHASE:
//                 break;
//             case GameState.END_PHASE:
//                 /*TODO:
//                 * Check if one side are all dead
//                 * if yes decide who is the winner
//                 * else app;y buffs' effect and go to the next turn
//                 */
//                 if (state != GameState.GAME_OVER)
//                     NextTurn();
//                 break;
//             default:
//                 break;
//         }
//     }

//     public void NextTurn() {
//             currentTurn++;
//     }

//     // Set game state with delay in second unity
//     public IEnumerator SetDelayState(GameState newState, float delay) {
//         yield return new WaitForSeconds(delay);
//         state = newState;
//     }
// }