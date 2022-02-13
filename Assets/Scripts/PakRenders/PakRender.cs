using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthSystem))]
public class PakRender : MonoBehaviour
{
    private static Color DARK_COLOR = new Color(.53f, .53f, .53f, 1);

    [SerializeField]
    private SpriteRenderer actionIcon;
    public HealthSystem healthSystem {get; private set;}

    

    public Pak pak;

    public Skill skill;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Initialize(pak.MaxHp);

        this.skill = new VanillaAttackOne("atk1","AttackOneEnemy","This do damage to one enemy", 0);
        if(this.skill == null){
            Debug.Log("Skill in PakRender is null");
        }else{
            Debug.Log("Skill in PakRender is not null");
        }
        // healthSystem = GetComponent<HealthSystem>();
        // healthSystem.initHealth(pak.Hp);
        // sr.sprite = pak.image;
        //hp.SetMaxHealth(pak.Hp);
        //currentHp = pak.Hp;
    }

    protected virtual void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            healthSystem.TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            healthSystem.Heal(10);
        }
    }

    public void DisplayInAction(bool value) {
        actionIcon.gameObject.SetActive(value);
        SpriteRenderer spirteRenderer = gameObject.GetComponent<SpriteRenderer>();

        spirteRenderer.color = (value) ? DARK_COLOR : Color.white;
    }

    public void DisplayInAction(bool value, int index)
    {
        //actionIcon.sprite = skillImages[skillIndex];
        DisplayInAction(value);
    }
}
