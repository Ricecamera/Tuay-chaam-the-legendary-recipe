using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuUI : MonoBehaviour {
    
    [Header("Default image")]
    private Sprite defualtCharacter;        // a image to show when the chracter doesn't have its image
    

    [SerializeField]
    private Image characterImage;

    public SkillUI[] skills;

    public void UpdateCharacterUI(Sprite sprite) {
        if (sprite) {
            characterImage.color  = Color.white;
            characterImage.sprite = sprite;

            /**TODO: Set icon for each skill*/
        }
        else {
            characterImage.sprite = null;
            characterImage.color  = Color.clear;
        }
    }

    public void ToggleMenu(bool isShow) {
        if (isShow) {
            gameObject.SetActive(true);
            return;
        }

        characterImage.sprite = null;
        gameObject.SetActive(false);
    }

    public void ToggleSkillUI(int index) {
        if (index < 0 || index > skills.Length) {
            throw new IndexOutOfRangeException();
        }

        if (skills[index].Selected) {
            skills[index].SetSelect(false);
        }
        else {
            for (int i = 0; i < skills.Length; i++) {
                if (i != index) {
                    skills[i].SetSelect(false);
                }
                else {
                    skills[i].SetSelect(true);
                }
            }
        }
    }
}
