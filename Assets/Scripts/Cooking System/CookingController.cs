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
        recepies.Add(new List<string>(){"honey", "lime", "salt"}, new SetOneEnemyHPTo10("HTo10", "SetOneEnemyHPTo10", "Deal very great amount of damage to one enemy.", 1, Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite));
        recepies.Add(new List<string>(){"carrot", "garlic", "honey", "lime", "salt"}, new HalfEnemiesHealth("HAEH", "HalfEnemiesHealth", "Half all enemies HP in half.", 3, Resources.Load("SkillIcons/sk3", typeof(Sprite)) as Sprite));

    }

    public void OnIngredientClick(List<string> selectedIngredient){
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

    public void OnStartCooking(List<string> selectedIngredient,PakRender caller, List<PakRender> target){
        recepies[selectedIngredient].performSkill(caller, target);
    }
}
