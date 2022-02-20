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

    [SerializeField]
    private GameObject selectedIcon;
    public HealthSystem healthSystem {get; private set;}

    public Pak pak;

    public List<Skill> skill;

    private Vector3 initPos;

    private Coroutine flashcheck;



    // Start is called before the first frame update
    protected virtual void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Initialize(pak.MaxHp);
        ShowSelected(false);
        DisplayInAction(false);

        skill = new List<Skill>();

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

        initPos = this.transform.position;
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

    public void ShowSelected(bool value) {
        selectedIcon.SetActive(value);
    }


    private IEnumerator pause(PakRender target, Vector3 desirePos)
    {
        this.transform.position = desirePos;
        SpriteRenderer sp = this.GetComponent<SpriteRenderer>();
        sp.sortingLayerName = "Tooltip";
        Material whiteMat = (Material)Resources.Load<Material>("FlashMaterial");
        Material originalMat = target.GetComponent<SpriteRenderer>().material;
        target.GetComponent<SpriteRenderer>().material = whiteMat;
        yield return new WaitForSeconds(0.5f);
        target.GetComponent<SpriteRenderer>().material = originalMat;
        flashcheck = null;
        this.transform.position = this.initPos;
        sp.sortingLayerName = "Character";

    }

    public void switchMat(PakRender target, Vector3 desirePos)
    {
        if (flashcheck != null)
        {
            Debug.LogError("Coroutine call multiple time");
            StopCoroutine(flashcheck);
        }

        StartCoroutine(pause(target, desirePos));
    }


    public void moveToEnemy(PakRender caller, PakRender target)
    {
        string my_tag = caller.tag;
        string enemy_tag = target.tag;
        // Transform myObjectTransform = GameObject.Find(my_tag).GetComponent<Transform>();
        Transform myObjectTransform = this.transform;
        Transform opposeObjectTransform = target.transform;
        //Vector3 myOldPos = new Vector3(myObjectTransform.position.x, myObjectTransform.position.y, myObjectTransform.position.z);

        Debug.Log("Oppose pos " + opposeObjectTransform.position);
        Vector3 desirePos;
        if (caller.tag == "Plant1" || caller.tag == "Plant2" || caller.tag == "Plant3" || caller.tag == "Chaam")
        {
            desirePos = new Vector3(opposeObjectTransform.position.x - 2, opposeObjectTransform.position.y, opposeObjectTransform.position.z);
        }
        else
        {
            desirePos = new Vector3(opposeObjectTransform.position.x + 2, opposeObjectTransform.position.y, opposeObjectTransform.position.z);
        }

        // myObjectTransform.position = new Vector3(opposeObjectTransform.position.x + 2, opposeObjectTransform.position.y, opposeObjectTransform.position.z);

        this.switchMat(target, desirePos);
        //this.transform.position = initPos;

    }

}
