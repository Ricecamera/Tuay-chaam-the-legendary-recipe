using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene {
    public class CharacterManager : MonoBehaviour {
    
    enum Team {PLAYER_TEAM, ENEMY_TEAM };
    private SortedList<string, Team> lookupTable;
    private Dictionary<string, CharacterHolder> holders;         // dictionary contains character holder

    // Initialize character dictonarys
    public void Intialize() {
        lookupTable = new SortedList<string, Team>();
        holders = new Dictionary<string, CharacterHolder>();
    }

    // Add a new character
    public void AddCharacter(string tag, GameObject character, int teamKey){
        try {
            // find the team of added character
            Team team;
            if (teamKey == 0)
                team= Team.PLAYER_TEAM;
            else if (teamKey == 1)
                team= Team.ENEMY_TEAM;
            else
                throw new Exception("team key is not valid");


            holders.Add(tag, new CharacterHolder(character));
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

    // Get a specific character by tag and team flag
    public CharacterHolder GetCharacter(string tag){
        CharacterHolder output = null;
        try {
            output = holders[tag];
        } catch (Exception e) {
            Debug.LogError(e.Message);
            output = null;
        }
        return output;
    }

    // Check if the dictionary has this tag
    public bool hasCharacter(string tag) {
        return holders.ContainsKey(tag);
    }

    // Set selected state of enemy character
    public void SetSelect(string tag, bool value) {
        try {
            CharacterHolder found = holders[tag];
            found.Select(value);
        }
        catch {
            Debug.LogError("the enemy character isn't exist!!");
        }
    }

    public void SetAction(string tag, bool value)
    {
        try
        {
            CharacterHolder found = holders[tag];
            found.Action(value);
        }
        catch
        {
            Debug.LogError("the ally character isn't exist!!");
        }
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

    // Get a list of CharacterHolder of pakTeam
    public List<CharacterHolder> getHolders(){
        List<CharacterHolder> temp = new List<CharacterHolder>();
        foreach (CharacterHolder e in holders.Values){
            temp.Add(e);
        }
        return temp;
    }

    // 0 is PLAYER_TEAM
    // 1 is ENEMY_TEAM
    public List<CharacterHolder> getTeamHolders(int teamKey) {
        List<CharacterHolder> temp = new List<CharacterHolder>();
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
                k_v.Value.HighLightLayer(true);
            }
            else {
                k_v.Value.HighLightLayer(false);
            }
        }
    }

    public void ResetHighLight() {
        foreach (var k_v in holders) {
             k_v.Value.HighLightLayer(false);
        }
    }

    // Set select state of ally character
    public void Reset() {
        foreach (var p in holders) {
            CharacterHolder holder = p.Value;
            holder.ResetState();
        }
    }

    public void ResetAction() {
        foreach (var p in holders) {
            CharacterHolder holder = p.Value;
            holder.Action(false);
        }
    }

    // Clear dictionarys
    public void Clear() {
        holders?.Clear();
    }
}
}

