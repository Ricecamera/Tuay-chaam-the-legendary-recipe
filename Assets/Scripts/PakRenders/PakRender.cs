using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class PakRender : MonoBehaviour
{   
    private enum State {
        Idle,
        Sliding,
        Busy
    }

    private static Color DARK_COLOR = new Color(160 / 255f, 160 / 255f, 160 / 255f, 1);


    [SerializeField]
    private SpriteRenderer actionIcon;

    [SerializeField]
    private GameObject selectedIcon;

    [SerializeField]
    private float slideSpeed = 6f, attackDistance = 1.75f;

    private Vector3 targetPos;
    private State state;

    private Coroutine flashcheck;
    private Action onSlideComplete;

    public HealthSystem healthSystem { get; private set; }

    [SerializeField]
    private Pak pak;

    public List<Skill> skill;

    public ParticleSystem defBuffVfx, atkBuffVfx;

    public int currentAtk, currentDef, currentSpeed, currentSp, maxSp;

    public Pak Pak { get { return pak;} }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Initialize(pak.MaxHp);
        currentAtk = pak.BaseAtk;
        currentDef = pak.BaseDef;
        currentSpeed = pak.BaseSpeed;
        maxSp = pak.MaxSp;
        currentSp = 0;
        ShowSelected(false);
//!
        actionIcon.gameObject.SetActive(false);
        SpriteRenderer spirteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spirteRenderer.color = Color.white;

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

    }

    protected virtual void Update()
    {
        switch (state) {
            case State.Idle:
                break;
            case State.Busy:
                break;
            case State.Sliding:
                transform.position += (targetPos - GetPosition()) * slideSpeed * Time.deltaTime;

                float reachedDistance = 0.2f;
                if (Vector3.Distance(GetPosition(), targetPos) < reachedDistance) {
                    // Arrived at Slide Target Position
                    //transform.position = slideTargetPosition;
                    onSlideComplete();
                }
                break;
        }
    }

    public void DisplayInAction(bool value, int index=0)
    {
        if(index <0 || index>skill.Count){
            Debug.LogError("Skill index out of range and couldn't load skill icon.");
        }
        actionIcon.sprite = skill[index].Icon;
        actionIcon.gameObject.SetActive(value);
        SpriteRenderer spirteRenderer = gameObject.GetComponent<SpriteRenderer>();

        spirteRenderer.color = (value) ? DARK_COLOR : Color.white;
    }

    // Set sorting layer of Pak's sprite and its health bar
    public void GoToLayer(string sortingLayer)
    {
        try
        {
            SpriteRenderer pakSprite = gameObject.GetComponent<SpriteRenderer>();
            Canvas healthbar = healthSystem.healthBar.GetComponent<Canvas>();

            pakSprite.sortingLayerName = sortingLayer;
            if (sortingLayer.CompareTo("Front") == 0)
                healthbar.sortingLayerName = "Front";
            else
                healthbar.sortingLayerName = "Overlay";

        }
        catch (NullReferenceException error)
        {
            Debug.LogError(error.Message);
        }

    }

    public void ShowSelected(bool value)
    {
        selectedIcon.SetActive(value);
    }


    private IEnumerator DamageEffect()
    {
        SpriteRenderer sp = this.GetComponent<SpriteRenderer>();
        Material whiteMat = (Material)Resources.Load<Material>("FlashMaterial");
        Material originalMat = GetComponent<SpriteRenderer>().material;
        sp.material = whiteMat;
        yield return new WaitForSeconds(0.5f);
        sp.material = originalMat;
        flashcheck = null;
    }

    public void switchMat()
    {
        if (flashcheck != null)
        {
            Debug.LogError("Coroutine call multiple time");
            StopCoroutine(flashcheck);
        }

        StartCoroutine(DamageEffect());
    }

    private void SlideToPosition(Vector3 slideTargetPosition, Action onSlideComplete) {
        this.targetPos = slideTargetPosition;
        this.onSlideComplete = onSlideComplete;
        state = State.Sliding;
    }

    public void Attack(Vector3 targetPosition, Action onReachTarget, Action onComplete) {
        Vector3 slideTargetPostion = targetPosition + (GetPosition() - targetPosition).normalized * attackDistance;
        Vector3 startingPostion = GetPosition();

        // Set sorting layer
        SpriteRenderer sp = this.GetComponent<SpriteRenderer>();

        sp.sortingLayerName = "Front";
        // Slide to the target
        SlideToPosition(slideTargetPostion, () => {
            state = State.Busy;

            // TO DO: add attack animation here
            /* insert sound here! */
            onReachTarget();
            SlideToPosition(startingPostion, () => {
                state = State.Idle;
                sp.sortingLayerName = "Character";
                onComplete();
            });
        });
    }

    private IEnumerator SpellAnimation(string skillId, Action onEffect, Action onComplete) {
        /* insert spell animation here */
        yield return new WaitForSeconds(0.2f);
        onEffect();
        /* insert sound here! */
        yield return new WaitForSeconds(1.5f);
        onComplete();
    }

    public void RangedBuff(string skillId, Action buffEffect, Action onComplete) {
        state = State.Busy;
        SpriteRenderer sp = this.GetComponent<SpriteRenderer>();
        sp.sortingLayerName = "Front";
        StartCoroutine(SpellAnimation(
            skillId,
            buffEffect,
            () => {
                onComplete();
                state = State.Idle;
                sp.sortingLayerName = "Character";
            })
        );
    }

    /*public void moveToEnemy(PakRender caller, List<PakRender> targets)
    {
        foreach (PakRender target in targets)
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
    }*/

    public void UpdateTurn() {
        // Update skill cooldown
        for (int i = 0; i < skill.Count; ++i) {
            skill[i].Cooldown--;
        }

        // Do something about this character when end turn ex. buff effect, burn damage, etc
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

}
