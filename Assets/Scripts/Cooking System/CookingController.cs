using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleScene;

public class CookingController : MonoBehaviour
{
    [Serializable]
    private class RecipePair {
        public string key;
        public SkillObj recipe;
    }

    //Refrence to recipes DB
    [SerializeField]
    private RecipePair[] recipesPair;

    // recipes map
    private Dictionary<string, SkillObj> recipes;

    private Text describe;

    // Start is called before the first frame update
    public void Start() {
        describe = GameObject.Find("describeText").GetComponent<Text>();
        foreach (RecipePair x in recipesPair) {
            recipes.Add(x.key, x.recipe);
        }
    }

    public SkillObj OnIngredientClick(List<string> selectedIngredient, GameObject comboPanel)
    {
        string key = string.Join("", selectedIngredient);
        Debug.Log(key);
        Debug.Log(recipes.ContainsKey(key));
        Debug.Log(recipes["honeylimesalt"]);
        Debug.Log(recipes.Count);
        // GameObject comboPanel = GameObject.Find("comboPanel2");
        if (recipes.ContainsKey(key))
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

        return recipes[key];
    }
}
