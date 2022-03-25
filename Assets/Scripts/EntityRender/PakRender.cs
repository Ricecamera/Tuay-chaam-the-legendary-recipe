using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class PakRender : MonoBehaviour, IComparable
{   
    static Color DARK_COLOR = new Color(160 / 255f, 160 / 255f, 160 / 255f, 1);
    static string LAYER_CHARACTER = "Character";
    static string LAYER_OVERLAY = "Overlay";
    static string LAYER_FRONT = "Front";

    public enum State {
        Idle,
        Sliding,
        Busy,
        InAction
    }

    public HealthSystem healthSystem { get; private set; }


    public List<Skill> skill;
    public int setSkill = -1;

    public ParticleSystem defBuffVfx, atkBuffVfx;

    public int currentAtk { get; set; }
    public int currentDef { get; set; }
    public int currentSpeed { get; set; }


    [SerializeField]
    private SpriteRenderer actionIcon;

    [SerializeField]
    private GameObject selectedIcon;

    [SerializeField]
    private Entity enitityData;

    [SerializeField]
    private float slideSpeed = 6f, attackDistance = 1.75f;

    private Vector3 targetPos;
    private State state;
    private bool selected;  // Is this character selected by player?

    private Coroutine flashcheck;
    private Action onSlideComplete;

    public Entity Entity { get { return enitityData;} }
    public State currentState { 
        get {
            return state;
        }
        set {
            state = value;
        }
    }
    public bool Selected {
        get {
            return selected;
        }

        set {
            selected = value;
            selectedIcon.SetActive(value);
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Initialize(enitityData.MaxHp);
        currentAtk = enitityData.BaseAtk;
        currentDef = enitityData.BaseDef;
        currentSpeed = enitityData.BaseSpeed;
        state = State.Idle;
        Selected = false;
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
    }

    protected virtual void Update()
    {
        switch (state) {
            case State.Sliding:
                transform.position += (targetPos - GetPosition()) * slideSpeed * Time.deltaTime;

                float reachedDistance = 0.2f;
                if (Vector3.Distance(GetPosition(), targetPos) < reachedDistance) {
                    // Arrived at Slide Target Position
                    //transform.position = slideTargetPosition;
                    onSlideComplete();
                }
                break;
            default:
                break;
        }
    }

    public int CompareTo(object obj) {
        if (obj == null) return 1;
        PakRender nextEvent = obj as PakRender;
        if (nextEvent != null) {
            return this.GetInstanceID().CompareTo(nextEvent.GetInstanceID());
        }
        else {
            throw new ArgumentException("Object doesn't have a property speed");
        }
    }

    public void DisplayInAction(bool value, int skillIndex)
    {
        // Prevent index out of bound error
        if(skillIndex < 0 || skillIndex > skill.Count){
            Debug.LogError("Skill index out of range and couldn't load skill icon.");
        }
        
        SpriteRenderer spirteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (value) {
            // Display inAction indicator
            actionIcon.sprite = skill[skillIndex].Icon;
        }
        else {
            // Hide InAction indicator
            if (setSkill == skillIndex)
                actionIcon.sprite = null;
        }

        actionIcon.gameObject.SetActive(value);
        spirteRenderer.color = (value) ? DARK_COLOR : Color.white;
    }

    public void DisplayInAction(bool value) {
        SpriteRenderer spirteRenderer = gameObject.GetComponent<SpriteRenderer>();
        actionIcon.gameObject.SetActive(value);
        spirteRenderer.color = (value) ? DARK_COLOR : Color.white;
    }

    // Set sorting layer of Pak's sprite and its health bar
    public void GoToFrontLayer(bool value)
    {
        try
        {
            SpriteRenderer enitityDataSprite = gameObject.GetComponent<SpriteRenderer>();
            Canvas healthbar = healthSystem.healthBar.GetComponent<Canvas>();

            enitityDataSprite.sortingLayerName = value ? LAYER_FRONT: LAYER_CHARACTER;
            healthbar.sortingLayerName = value ? LAYER_FRONT : LAYER_OVERLAY;

        }
        catch (NullReferenceException error)
        {
            Debug.LogError(error.Message);
        }

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

    public bool InAction() {
        return state == State.InAction;
    }

    private IEnumerator DamageEffect() {
        SpriteRenderer sp = this.GetComponent<SpriteRenderer>();
        Material whiteMat = (Material) Resources.Load<Material>("FlashMaterial");
        Material originalMat = GetComponent<SpriteRenderer>().material;
        sp.material = whiteMat;
        yield return new WaitForSeconds(0.5f);
        sp.material = originalMat;
        flashcheck = null;
    }


    private void SlideToPosition(Vector3 slideTargetPosition, Action onSlideComplete) {
        this.targetPos = slideTargetPosition;
        this.onSlideComplete = onSlideComplete;
        currentState = State.Sliding;
    }


    private IEnumerator SpellAnimation(string skillId, Action onEffect, Action onComplete) {
        /* insert spell animation here */
        yield return new WaitForSeconds(0.2f);
        onEffect();
        /* insert sound here! */
        yield return new WaitForSeconds(1.5f);
        onComplete();
    }
}
