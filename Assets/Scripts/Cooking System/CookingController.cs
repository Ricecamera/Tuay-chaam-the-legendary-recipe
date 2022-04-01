using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingController : MonoBehaviour
{
    //Food recipies DB.
    private Dictionary<List<string>, Skill> recepies = new Dictionary<List<string>, Skill>();

    // Start is called before the first frame update
    void Start()
    {
        //init all special skill and map it in the dict
        recepies.Add(new List<string>(){"(Ingredients) honey", "(Ingredients) Lime", "(Ingredients) salt"}, new SetOneEnemyHPTo10("VGSP1", "GainSPOneAlliance", "Target one alliance and gain it 1 SP.", 3, Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite));

    }

    void OnIngredientClick(List<string> selectedIngredient){
        if(recepies.ContainsKey(selectedIngredient)){
            GameObject describeText = GameObject.Find("describeText");
            Text description = describeText.GetComponent<Text>();
            description.text="Honey Lemon: It will deal great damage to one enemy.";
            // set name of food
            // recepies[selectedIngredient].SkillNamed;

            // set description of food
            // recepies[selectedIngredient].Description;

            //enable the cook window
        }else{
            //disable the cook window

        }
    }

    void OnStartCooking(List<string> selectedIngredient,PakRender caller, List<PakRender> target){
        recepies[selectedIngredient].performSkill(caller, target);
    }
}
