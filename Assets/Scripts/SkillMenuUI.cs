using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuUI : MonoBehaviour {
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

            characterImage.sprite = sprite;

            /**TODO: Set icon for each skill*/
        }
        else {
            characterImage.sprite = null;
        }
        ToggleMenu(true);
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
            skills[index].GetComponent<RectTransform>().localScale = new Vector3(9f, 9f, 9f);
            skillToggle[index] = false;
        }
        else {
            for (int i = 0; i < skills.Length; i++) {
                skills[i].GetComponent<RectTransform>().localScale = new Vector3(9f, 9f, 9f);
                skillToggle[i] = false;
            }
            skills[index].GetComponent<RectTransform>().localScale = new Vector3(10f, 10f, 10f);
            skillToggle[index] = true;
        }
    }
}
