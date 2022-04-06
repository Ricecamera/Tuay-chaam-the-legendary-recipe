using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class CharacterDetail : MonoBehaviour
{
    public static TextMeshProUGUI characterName;
    public static TextMeshProUGUI characterSkill;
    public static Image characterImg;

    //"Skill 1 : asdjwidjwda Skill 2 : asdhwuidhaw Skill 3 : cjoiajdowdwd Skill 4 : cjoiajdowdwd"
    public static void ChangeDetail(string txtName, string txtSkill, Sprite img)
    {
        characterName = GameObject.Find("CharacterName").GetComponent<TextMeshProUGUI>();
        characterSkill = GameObject.Find("Skill").GetComponent<TextMeshProUGUI>();
        characterImg = GameObject.Find("CharacterImg").GetComponent<Image>();
        characterName.text = txtName;
        characterSkill.text = txtSkill;
        characterImg.sprite = img;
    }


}
