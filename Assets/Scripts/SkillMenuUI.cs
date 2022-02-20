using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuUI : MonoBehaviour
{
    private const float SIZE_MULTIPLER = 1.2f;

    [Header("Default image")]
    private Sprite defualtCharacter;
    private Sprite defaultButtonSprite;

    [SerializeField]
    private Image characterImage;

    public Button[] skills;

    public List<bool> skillToggle;

    public void Awake()
    {
        skillToggle = new List<bool>(skills.Length);
        for (int i = 0; i < skills.Length; i++)
        {
            skillToggle.Add(false);
        }
    }
    public void UpdateCharacterUI(Sprite sprite)
    {
        if (sprite)
        {
            characterImage.color = Color.white;
            characterImage.sprite = sprite;

            /**TODO: Set icon for each skill*/

        }
        else
        {
            characterImage.sprite = null;
            characterImage.color = Color.clear;
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

    public void ToggleSkillUI(int index)
    {
        if (index < 0 || index > skills.Length)
        {
            throw new IndexOutOfRangeException();
        }

        if (skillToggle[index])
        {
            skills[index].GetComponent<RectTransform>().localScale = Vector3.one;
            skillToggle[index] = false;
        }
        else
        {
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].GetComponent<RectTransform>().localScale = Vector3.one;
                skillToggle[i] = false;
            }
            skills[index].GetComponent<RectTransform>().localScale = Vector3.one * SIZE_MULTIPLER;
            skillToggle[index] = true;
        }
    }

    public void UpdateSkillUI(PakRender pak)
    {
        // System.Type pakSpecificType = pak.GetType(); //get the actual type (lowest level) of the object.
        // var pakSpecific = Convert.ChangeType(pak, pakSpecificType);
        // if () {
        //     characterImage.color  = Color.white;
        //     characterImage.sprite = sprite;

        //     /**TODO: Set icon for each skill*/
        // }
        // else {
        //     characterImage.sprite = null;
        //     characterImage.color  = Color.clear;
        // }
        Debug.Log("Show Type");
        Debug.Log(pak);
        Debug.Log(pak.skill[0]);
        //var a = pak.skill[0].Icon;

        Image temp1 = skills[0].GetComponent<Image>();
        temp1.sprite = pak.skill[0].Icon;
        Image temp2 = skills[1].GetComponent<Image>();
        temp2.sprite = pak.skill[1].Icon;
        Image temp3 = skills[2].GetComponent<Image>();
        temp3.sprite = pak.skill[2].Icon;
        // Image temp4 = skills[3].GetComponent<Image>();
        // temp4.sprite = pak.skill[3].Icon;

    }
}
