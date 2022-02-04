using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene {
    class CharacterHolder {
        public GameObject character;
        public bool isSelected;
        public bool InAction;

        public CharacterHolder(GameObject character) {
            this.character = character;
            isSelected = false;
            InAction = false;
        }

    }

    public class CharacterManager : MonoBehaviour {
        public static Vector3 normalCharacterScale = new Vector3(0.3f, 0.3f, 0.3f);
        public static Vector3 selectCharacterScale = new Vector3(0.35f, 0.35f, 0.35f);

        private Dictionary<string, CharacterHolder> playerTeam;
        private Dictionary<string, CharacterHolder> enemyTeam;

        public void Intialize() {
            playerTeam = new Dictionary<string, CharacterHolder>();
            enemyTeam = new Dictionary<string, CharacterHolder>();
        }

        public void SetCharacter(string tag, GameObject character, bool isPlayerFlag = false){
            try {
                if (isPlayerFlag)
                    playerTeam.Add(tag, new CharacterHolder(character));
                else
                    enemyTeam.Add(tag, new CharacterHolder(character));
            }
            catch (ArgumentException) {
                Debug.LogError(string.Format("An element with Key = {0} already exists.", tag));
            }
        }

        public bool RemoveCharacter(string tag, bool isPlayerFlag = false) {
            if (isPlayerFlag)
                return playerTeam.Remove(tag);
            return enemyTeam.Remove(tag);
        }

        public GameObject GetCharacter(string tag, bool isPlayerFlag = false){
            CharacterHolder output = null;
            try {
                if (isPlayerFlag)
                    output = playerTeam[tag];
                else
                    output = enemyTeam[tag];
                return output.character;
            } catch {
                Debug.LogError(string.Format("An element with Key = {0} doesn't exist.", tag));
            }
            return output.character;
        }

        public bool hasCharacter(string tag, bool isPlayerFlag = false) {
            if (isPlayerFlag)
                return playerTeam.ContainsKey(tag);
            return enemyTeam.ContainsKey(tag);
        }

        public void SelectEnemy(string tag, bool value) {
            try {
                CharacterHolder found = enemyTeam[tag];
                found.isSelected = value;
                if (value) {
                    found.character.GetComponent<RectTransform>().localScale = selectCharacterScale;
                }
                else {
                    found.character.GetComponent<RectTransform>().localScale = normalCharacterScale;
                }
            }
            catch {
                Debug.LogError("the enemy character isn't exist!!");
            }
        }

        public void SelectAlly(string tag, bool value) {
            try {
                CharacterHolder found = playerTeam[tag];
                found.isSelected = value;
                if (value) {
                    found.character.GetComponent<RectTransform>().localScale = selectCharacterScale;
                }
                else {
                    found.character.GetComponent<RectTransform>().localScale = normalCharacterScale;
                }
            }
            catch {
                Debug.LogError("the ally character isn't exist!!");
            }
        }

        public void Reset() {
            playerTeam?.Clear();
            enemyTeam?.Clear();
        }
    }
}

