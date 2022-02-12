using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PakRender : MonoBehaviour
{

    public Pak pak;
    public HealthBar hp;
    //public int currentHp;
    public Canvas canvas;
    public HealthSystem healthSystem;

    public Skill skill;

    public GameObject plantpos;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Level = " + LevelSelection.Level);
        pak.Hp = pak.MaxHp;
        Debug.Log("start pakRender:" + pak.Hp);

        healthSystem = new HealthSystem(pak.Hp, hp);
        this.skill = new VanillaAttackOne("atk1", "AttackOneEnemy", "This do damage to one enemy", 0);
        if (this.skill == null)
        {
            Debug.Log("Skill in PakRender is null");
        }
        else
        {
            Debug.Log("Skill in PakRender is not null");
        }
        // healthSystem = GetComponent<HealthSystem>();
        // healthSystem.initHealth(pak.Hp);
        // sr.sprite = pak.image;
        //hp.SetMaxHealth(pak.Hp);
        //currentHp = pak.Hp;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            healthSystem.TakeDamage(5, pak.EntityName);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            healthSystem.Heal(5, pak.EntityName);
        }
    }

    /*    void TakeDamage(int damage)
        {
            if (currentHp - damage >= 0)
            {
                currentHp -= damage;
                hp.SetHealth(currentHp);
            }

            Debug.Log("wowza");

        }*/

    public void HideHpBar()
    {
        canvas.enabled = false;
    }

    public void ShowHpBar()
    {
        canvas.enabled = true;
    }

}
