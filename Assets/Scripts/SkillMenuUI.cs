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

    private void Start() {
        for (int i = 0; i < skills.Length; i++) {
            skills[i].SetSelect(false);
        }
    }
    public void ToggleMenu(bool isShow)
    {
        if (isShow)
        {
            gameObject.SetActive(true);

            return;
        }

        characterImage.sprite = null;
        gameObject.SetActive(false);
    }

    public void ToggleSkill(int index)
    {
        if (index < 0 || index > skills.Length)
        {
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

    public void UpdateImage(Sprite sprite, List<SkillExecutor> charaterSkills)
    {
        if (sprite) {
            characterImage.color = Color.white;
            characterImage.sprite = sprite;
        }
        else {
            characterImage.sprite = null;
            characterImage.color = Color.clear;
        }

        int i = 0;

        // Set skills for each skill button
        for (; i < charaterSkills.Count && i < skills.Length; ++i) {
            skills[i].SetSkill(charaterSkills[i]);
        }

        // hide all skill button that doesn't set skill
        for (; i < skills.Length; ++i) {
            skills[i].SetSkill(null);
        }
    }
}
