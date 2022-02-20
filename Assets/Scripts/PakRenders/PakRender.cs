using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthSystem))]
public class PakRender : MonoBehaviour
{
    public HealthSystem healthSystem { get; private set; }

    public Pak pak;

    public List<Skill> skill;

    private Vector3 initPos;

    private Coroutine flashcheck;




    // Start is called before the first frame update
    protected virtual void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Initialize(pak.MaxHp);

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            healthSystem.TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            healthSystem.Heal(10);
        }
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
