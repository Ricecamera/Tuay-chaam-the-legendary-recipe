using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene {
    public class CharacterManager : MonoBehaviour {
    
    enum Team {PLAYER_TEAM, ENEMY_TEAM };
    private SortedList<string, Team> lookupTable;
    private Dictionary<string, PakRender> holders;         // dictionary contains character holder

    // Initialize character dictonarys
    public void Intialize() {
        lookupTable = new SortedList<string, Team>();
        holders = new Dictionary<string, PakRender>();
    }

    // Add a new character

    // Get a specific character by tag and team flag
    public PakRender GetCharacter(string tag) {
        PakRender output = null;
        try {
            output = holders[tag];
        } 
        catch (Exception e) {
            Debug.LogError(e.Message);
            output = null;
        }
        
        return output;
    }

    // Check if the dictionary has this tag
    public bool hasCharacter(string tag) {
        return holders.ContainsKey(tag);
    }

    public string GetCharacterTeam(string tag) {
        try {
            Team team = lookupTable[tag];

            if (team == Team.PLAYER_TEAM)
                return "Player team";
            return "Enemy team";
        }
        catch {
            return "No team";
        }
    }

    public void AddCharacter(string tag, GameObject character, int teamKey) {
        try {
            // find the team of added character
            Debug.Log(character);
            Team team;
            if (teamKey == 0)
                team = Team.PLAYER_TEAM;
            else if (teamKey == 1)
                team = Team.ENEMY_TEAM;
            else
                throw new Exception("team key is not valid");


            holders.Add(tag, character.GetComponent<PakRender>());
            lookupTable.Add(tag, team);
        }
        catch (Exception e) {
            if (e is ArgumentException) {
                Destroy(character);
            }
            Debug.LogError(e.Message);
        }
    }

    // Remove a specific chacracter by tag and team flag
    public void RemoveCharacter(string tag) {
        lookupTable.Remove(tag);
        holders.Remove(tag);
    }

    // Get a list of CharacterHolder of pakTeam
    public List<PakRender> getHolders(){
        List<PakRender> temp = new List<PakRender>();
        foreach (PakRender p in holders.Values){
            temp.Add(p);
        }
        return temp;
    }

    // 0 is PLAYER_TEAM
    // 1 is ENEMY_TEAM
    public List<PakRender> getTeamHolders(int teamKey) {
        List<PakRender> temp = new List<PakRender>();
        if (teamKey < 0 || teamKey > 1) {
            return temp;
        }

        Team team;
        if (teamKey == 0)
            team = Team.PLAYER_TEAM;
        else
            team = Team.ENEMY_TEAM;

        foreach (var kv in lookupTable) {
            if (kv.Value == team)
                temp.Add(holders[kv.Key]);
        }
        return temp;
    }

    
    public void HighLightCharacters(List<string> tags) {
        foreach (var k_v in holders) {
            // if the llst contain a key then highlight the character
            if (tags.Contains(k_v.Key)) {
                k_v.Value.GoToFrontLayer(true);
            }
            else {
                k_v.Value.GoToFrontLayer(false);
            }
        }
    }

    /** Reset state of all characters
     * 0.Reset all state
     * 1.Reset Action
     * 2.Reset Select
     * 3.Reset HighLight
     */
    public void ResetState(int options) {
        foreach (var k_v in holders) {
            switch(options) {
                case 0:
                    k_v.Value.GoToFrontLayer(false);
                    k_v.Value.currentState = PakRender.State.Idle;
                    k_v.Value.DisplayInAction(false);
                    k_v.Value.Selected = false;
                    break;
                case 1:
                    k_v.Value.currentState = PakRender.State.Idle;
                    k_v.Value.DisplayInAction(false);
                    break;
                case 2:
                    k_v.Value.Selected = false;
                    break;
                case 3:
                    k_v.Value.GoToFrontLayer(false);
                    break;
                default:
                    break;
            }
        }
    }


    // Clear dictionarys
    public void Clear() {
        holders?.Clear();
    }

}
}


