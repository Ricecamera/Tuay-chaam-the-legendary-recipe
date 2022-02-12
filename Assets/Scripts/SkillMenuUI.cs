using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuUI : MonoBehaviour {
    private const float SIZE_MULTIPLER = 1.2f;

    [Header("Default image")]
    private Sprite defualtCharacter;
    private Sprite defaultButtonSprite;

    [SerializeField]
    private Image characterImage;

    public Button[] skills;

    public List<bool> skillToggle;

    public void Awake() {
        skillToggle = new List<bool>(skills.Length);
        for (int i = 0; i < skills.Length; i++) {
            skillToggle.Add(false);
        }
    }
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

        if (skillToggle[index]) {
            skills[index].GetComponent<RectTransform>().localScale = Vector3.one;
            skillToggle[index] = false;
        }
        else {
            for (int i = 0; i < skills.Length; i++) {
                skills[i].GetComponent<RectTransform>().localScale = Vector3.one;
                skillToggle[i] = false;
            }
            skills[index].GetComponent<RectTransform>().localScale = Vector3.one * SIZE_MULTIPLER;
            skillToggle[index] = true;
        }
    }
}
