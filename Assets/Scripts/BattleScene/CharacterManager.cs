using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene {
public class CharacterManager : MonoBehaviour {
    public static CharacterManager instance;     
    enum Team {PLAYER_TEAM, ENEMY_TEAM };
    private Dictionary<string, PakRender> holders = new Dictionary<string, PakRender>();         // dictionary contains character holder

    // Check team of tag object
    public static bool IsPlayerTeam(string tag) {
        return (tag.CompareTo("Plant1") == 0 || tag.CompareTo("Plant2") == 0
                    || tag.CompareTo("Plant3") == 0|| tag.CompareTo("Chaam") == 0);
    }

    public static bool IsEnemyTeam(string tag) {
        return (tag.CompareTo("Enemy1") == 0 || tag.CompareTo("Enemy2") == 0 || tag.CompareTo("Enemy3") == 0
            || tag.CompareTo("Enemy4") == 0 || tag.CompareTo("Boss") == 0);
    }


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

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

    // Add a new character
    public void AddCharacter(string tag, GameObject character) {
        try {
            // find the team of added character
            holders.Add(tag, character.GetComponent<PakRender>());
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
        if (teamKey < 0 || teamKey > 1) {
            throw new Exception("Invalid team key");
        }

        List<PakRender> temp = new List<PakRender>();

        foreach (var kv in holders) {
            PakRender pak = kv.Value;

            if (CheckTeamKey(teamKey, pak)) {
                temp.Add(pak);
            }    
        }
        return temp;
    }

    public List<PakRender> GetAliveCharacters(int teamKey) {
        if (teamKey < 0 || teamKey > 2) {
            throw new Exception("Invalid team key");
        }

        List<PakRender> temp = new List<PakRender>();

        foreach (var kv in holders) {
            var pak = kv.Value;
            if (pak.healthSystem.IsAlive) {
                if (CheckTeamKey(teamKey, pak)) {
                    temp.Add(pak);
                }
            }
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

    public void LockAllCharacters(bool value, int teamKey) {
        if (teamKey < 0 || teamKey > 2) {
            throw new Exception("Invalid team key");
        }

        foreach (var kv in holders) {
            var pak = kv.Value;
            if (CheckTeamKey(teamKey, pak)) {
                pak.GetComponent<BoxCollider2D>().enabled = !value;
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

    private bool CheckTeamKey(int key, PakRender pak) {
        if (key < 0 || key > 2) {
            throw new Exception("Invalid team key");
        }
        return (key == 2) || (key == 0 && IsPlayerTeam(pak.tag)) || (key == 1 && IsEnemyTeam(pak.tag));
    }
}
}


