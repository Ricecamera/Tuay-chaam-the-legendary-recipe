using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene {
public class CharacterManager : MonoBehaviour {
    public class CharacterHolder {
        public GameObject character;    // game object of this character
        public bool isSelected;         // Is this character selected by player?
        public bool inAction;           // Is this character setted its action?

        // Constructors
        public CharacterHolder() {
            character = null;
            isSelected = false;
            inAction = false;
        }

        public CharacterHolder(GameObject character) {
            this.character = character;
            isSelected = false;
            inAction = false;
        }
    }

    private const float SIZE_MULTIPLER = 1.2f;                      // size mulitplier to be apply to the selected character

    private Dictionary<string, CharacterHolder> playerTeam;         // dictionary contains CharacterHolder of playerTeam
    private Dictionary<string, CharacterHolder> enemyTeam;          // dictionary contains CharacterHolder of enemyTeam

    // Initialize character dictonarys
    public void Intialize() {
        playerTeam = new Dictionary<string, CharacterHolder>();
        enemyTeam = new Dictionary<string, CharacterHolder>();
    }

    // Add a new character
    public void SetCharacter(string tag, GameObject character, bool isPlayerFlag = false){
        try {
            if (isPlayerFlag)
                playerTeam.Add(tag, new CharacterHolder(character));
            else
                enemyTeam.Add(tag, new CharacterHolder(character));
        }
        catch (Exception e) {
            Debug.LogError(e.Message);
        }
    }

    // Remove a specific chacracter by tag and team flag
    public bool RemoveCharacter(string tag, bool isPlayerFlag = false) {
        if (isPlayerFlag)
            return playerTeam.Remove(tag);
        return enemyTeam.Remove(tag);
    }

    // Get a specific character by tag and team flag
    public GameObject GetCharacter(string tag, bool isPlayerFlag = false){
        CharacterHolder output = null;
        try {
            if (isPlayerFlag)
                output = playerTeam[tag];
            else
                output = enemyTeam[tag];
            return output.character;
        } catch (Exception e) {
            Debug.LogError(e.Message);
        }
        return output.character;
    }

    // Check if the dictionary has this tag
    public bool hasCharacter(string tag, bool isPlayerFlag = false) {
        if (isPlayerFlag)
            return playerTeam.ContainsKey(tag);
        return enemyTeam.ContainsKey(tag);
    }

    // Set selected state of enemy character
    public void SelectEnemy(string tag, bool value) {
        try {
            CharacterHolder found = enemyTeam[tag];
            found.isSelected = value;
            if (value) {
                found.character.GetComponent<RectTransform>().localScale = Vector3.one * SIZE_MULTIPLER;
            }
            else {
                found.character.GetComponent<RectTransform>().localScale = Vector3.one;
            }
        }
        catch {
            Debug.LogError("the enemy character isn't exist!!");
        }
    }

    // Set select state of ally character
    public void SelectAlly(string tag, bool value) {
        try {
            CharacterHolder found = playerTeam[tag];
            found.isSelected = value;
            if (value) {
                found.character.GetComponent<RectTransform>().localScale = Vector3.one * SIZE_MULTIPLER;
            }
            else {
                found.character.GetComponent<RectTransform>().localScale = Vector3.one;
            }
        }
        catch {
            Debug.LogError("the ally character isn't exist!!");
        }
    }

    // Clear dictionarys
    public void Reset() {
        playerTeam?.Clear();
        enemyTeam?.Clear();
    }

    // Get a list of CharacterHolder of pakTeam
    public List<CharacterHolder> getPakTeam(){
        List<CharacterHolder> temp = new List<CharacterHolder>();
        foreach (CharacterHolder e in playerTeam.Values){
            temp.Add(e);
        }
        return temp;
    }


    // Get a list of CharacterHolder of enemyTeam
    public List<CharacterHolder> getEnemyTeam(){
        List<CharacterHolder> temp = new List<CharacterHolder>();
        foreach (CharacterHolder e in enemyTeam.Values){
            temp.Add(e);
        }
        return temp;
    }

}
}

