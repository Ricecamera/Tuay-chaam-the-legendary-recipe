using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingController : MonoBehaviour
{
    //Food recipies DB.
    private Dictionary<List<string>, SkillExecutor> recepies = new Dictionary<List<string>, SkillExecutor>();

    // Start is called before the first frame update
    void Start()
    {
        //init all special skill and map it in the dict
        //recepies.Add(new List<string>(){"(Ingredients) honey", "(Ingredients) Lime", "(Ingredients) salt"}, new SetOneEnemyHPTo10("VGSP1", "GainSPOneAlliance", "Target one alliance and gain it 1 SP.", 3, Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite));

    }

    void OnIngredientClick(List<string> selectedIngredient){
        if(recepies.ContainsKey(selectedIngredient)){
            // set description of food
            GameObject describeText = GameObject.Find("describeText");
            Text description = describeText.GetComponent<Text>();
            description.text="Honey Lemon: It will deal great damage to one enemy.";
            
            //enable the cook window
            describeText.SetActive(true);

        }else{
            //disable the cook window
            GameObject describeText = GameObject.Find("describeText");
            describeText.SetActive(false);
        }
    }

    void OnStartCooking(List<string> selectedIngredient,PakRender caller, List<PakRender> target){
        //recepies[selectedIngredient].performSkill(caller, target);
    }
}
