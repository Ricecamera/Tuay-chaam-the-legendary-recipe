using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class PakRender : MonoBehaviour
{
    private static Color DARK_COLOR = new Color(99 / 255f, 238 / 255f, 108 / 255f, 1);

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
    }

    protected virtual void Update()
    {

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

    // Set sorting layer of Pak's sprite and its health bar
    public void GoToLayer(string sortingLayer) {
        try {
            SpriteRenderer pakSprite = gameObject.GetComponent<SpriteRenderer>();
            Canvas healthbar = healthSystem.healthBar.GetComponent<Canvas>();

            Debug.Log(pakSprite.sortingLayerName);

            pakSprite.sortingLayerName = sortingLayer;
            if (sortingLayer.CompareTo("Front") == 0)
                healthbar.sortingLayerName = "Front";
            else
                healthbar.sortingLayerName = "Overlay";
            
        }
        catch (NullReferenceException error){
            Debug.LogError(error.Message);
        }

    }
}
