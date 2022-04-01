using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingController
{
    //Food recipies DB.
    private Dictionary<string, Skill> recepies;

    private Text describe;

    // Start is called before the first frame update
    public CookingController()
    {
        recepies = new Dictionary<string, Skill>();
        //init all special skill and map it in the dict
        recepies.Add("honeylimesalt", new SetOneEnemyHPTo10("HTo10", "SetOneEnemyHPTo10", "Deal very great amount of damage to one enemy.", 1, Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite));
        recepies.Add("carrotgarlichoneylimesalt", new HalfEnemiesHealth("HAEH", "HalfEnemiesHealth", "Half all enemies HP in half.", 3, Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite));
        Debug.Log("*********************************************************************************************************************************************************************************************************************************************");
        foreach (KeyValuePair<string, Skill> kvp in recepies)
        {
            Debug.Log(kvp.Key);
            Debug.Log(kvp.Value);
        }
        describe = GameObject.Find("describeText").GetComponent<Text>();



    }

    public Skill OnIngredientClick(List<string> selectedIngredient, GameObject comboPanel)
    {
        string key = string.Join("", selectedIngredient);
        Debug.Log(key);
        Debug.Log(recepies.ContainsKey(key));
        Debug.Log(recepies["honeylimesalt"]);
        Debug.Log(recepies.Count);
        // GameObject comboPanel = GameObject.Find("comboPanel2");
        if (recepies.ContainsKey(key))
        {
            // set description of food

            describe.text = "Honey Lemon: It will deal great damage to one enemy.";

            //enable the cook window
            if (!comboPanel.activeSelf)
            {
                comboPanel.SetActive(true);
            }
            Debug.Log("Yay");

        }
        else
        {
            Debug.Log(comboPanel);
            //disable the cook window
            if (comboPanel.activeSelf)
            {
                comboPanel.SetActive(false);
            }

            Debug.LogError("Noo:(");
        }

        return recepies[key];
    }

    public void OnStartCooking(List<string> selectedIngredient, PakRender caller, List<PakRender> target)
    {
        string key = string.Join("", selectedIngredient);
        recepies[key].performSkill(caller, target);
    }
}
