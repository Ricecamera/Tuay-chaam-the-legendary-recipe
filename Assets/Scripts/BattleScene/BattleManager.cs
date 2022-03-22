using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleScene.BattleLogic;
using Random = UnityEngine.Random;

namespace BattleScene {
/*
    *** Overview of this class ***
    - เป็น class ที่ทำการเชื่อม logic การทำงานของ PakSelection กับ ActionCommandHandler
    - ค่อยทำหน้าที่เก็บคลาส AI controller
*/
[RequireComponent(typeof(ActionCommandHandler))]
public class BattleManager : MonoBehaviour {
    public static BattleManager instance;                   // singleton instance of this class
    
    public int currentTurn { get; private set; }            // keep track how many turn have pass

    public Text actionText;                                 // text that show when in battle

    public AIController AI;                                 // reference to AI controller class

    private CharacterManager characters;
    public ActionCommandHandler actionCommandHandler {get; private set; }       // reference to ActionCommandHandler


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    // Subscribe OnComplete event
    // the event invokes went actionCommandHandler finish execute all of characters' actions.
    private void OnEnable() {
        ActionCommandHandler.OnComplete += NextTurn;
    }

    // Unsubscribe OnComplete event when this object disable from game.
    private void OnDisable() {
        ActionCommandHandler.OnComplete -= NextTurn;
    }

    // Start is called before the first frame update
    void Start() {
        currentTurn = 0;

        characters = GameObject.Find("Chracter Manager").GetComponent<CharacterManager>();
        actionCommandHandler = GetComponent<ActionCommandHandler>();
        actionText.gameObject.SetActive(false);

        // Initialize AI
        AI = new AIController();
    }

    // Set game AI target and call RunCommands in actionCommandHandler.
    // This method call when a player clicks endTurn button.
    public void RunCommand() {
        Debug.Log("Battle Start!!");

        CharacterSelecter.instance?.ResetCharacter();

        //---------------------------------New AI ---------------------------------------------//

        // Get list of pakTeam and enemy Team
        List<PakRender> pakHolders = new List<PakRender>();
        List<PakRender> enemyHolders = new List<PakRender>();


        
        // Remove dead characters from player team and enemy team
        foreach (var e in characters.getTeamHolders(0))
        {
            if (e.gameObject.activeSelf)
            {
                pakHolders.Add(e);
            }
        }

        foreach (var f in characters.getTeamHolders(1))
        {
            if (f.gameObject.activeSelf)
            {
                enemyHolders.Add(f);
            }
        }

        // Let AI controller selection its actions
        List<ActionCommand> temp = AI.selectAction(pakHolders, enemyHolders);
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
        List<PakRender> holders = characters.getHolders();
        foreach (var pak in holders)
            pak.UpdateTurn();
    }

    public bool IsPlayerLose()
    {
        Spawner spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        List<PakRender> pakInBattle = characters.getTeamHolders(0);
        foreach (var e in pakInBattle)
        {
            if (e.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsPlayerWin()
    {
        Spawner spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        List<PakRender> enemyInBattle = characters.getTeamHolders(1);
        foreach (PakRender e in enemyInBattle)
        {
            if (e.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
}