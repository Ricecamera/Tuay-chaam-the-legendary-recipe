﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene {
public class CharacterManager : MonoBehaviour {
    public class CharacterHolder {
        private bool isSelected;         // Is this character selected by player?
        private bool inAction;           // Is this character setted its action?

        public GameObject character;    // game object of this character

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

            // Hide Action Icon
            PakRender render = character.GetComponent<PakRender>();
            render.DisplayInAction(false);
        }

        public void ResetState() {
            isSelected = false;
            // Reduce scale to normal scale
            character.GetComponent<RectTransform>().localScale = Vector3.one;

        }

        public void Select(bool value) {
            isSelected = value;
            if (value)
            {
                character.GetComponent<RectTransform>().localScale = Vector3.one * SIZE_MULTIPLER;
            }
            else
            {
                character.GetComponent<RectTransform>().localScale = Vector3.one;
            }
        }

        public void Action(bool value) {
            inAction = value;
            PakRender render = character.GetComponent<PakRender>();
            if (value)
                render.DisplayInAction(true, 0);
            else
                render.DisplayInAction(false);
        }
    }

    private const float SIZE_MULTIPLER = 1.2f;                   // size mulitplier to be apply to the selected character

    private Dictionary<string, CharacterHolder> holders;         // dictionary contains character holder

    // Initialize character dictonarys
    public void Intialize() {
        holders = new Dictionary<string, CharacterHolder>();
    }

    // Add a new character
    public void AddCharacter(string tag, GameObject character){
        try {
            holders.Add(tag, new CharacterHolder(character));
        }
        catch (Exception e) {
            Debug.LogError(e.Message);
        }
    }

    // Remove a specific chacracter by tag and team flag
    public bool RemoveCharacter(string tag) {
        return holders.Remove(tag);
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


    // Set select state of ally character
    public void Reset() {
        foreach (var p in holders) {
            CharacterHolder holder = p.Value;
            holder.ResetState();
        }
    }

    public void ResetAction() {
            foreach (var p in holders)
            {
                CharacterHolder holder = p.Value;
                holder.Action(false);
            }
    }

    // Clear dictionarys
    public void Clear() {
        holders?.Clear();
    }

    // Get a list of CharacterHolder of pakTeam
    public List<CharacterHolder> getHoldersList(){
        List<CharacterHolder> temp = new List<CharacterHolder>();
        foreach (CharacterHolder e in holders.Values){
            temp.Add(e);
        }
        return temp;
    }

}}

