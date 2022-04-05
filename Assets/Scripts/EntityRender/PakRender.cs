using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem;
using BuffSystem.Behaviour;
using System.Linq;

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

    public SkillObj[] skills;

    public int currentAtk { get; set; }
    public int currentDef { get; set; }
    public int currentSpeed { get; set; }

    private int setSkill = -1;

    [SerializeField]
    private SpriteRenderer actionIcon;

    [SerializeField]
    private GameObject selectedIcon;

    [SerializeField]
    private BuffDisplayer buffDisplayer;

    [SerializeField]
    private Entity enitityData;

    private float slideSpeed = 6f, attackDistance = 1.75f;

    private Vector3 targetPos;
    private State state;
    private bool selected;  // Is this character selected by player?

    private Coroutine flashcheck;
    private Action onSlideComplete;

    private List<SkillExecutor> skillExecutors;
    private List<BaseBuff> attachedBuffs;
    private List<int> buffRemainingTurns;
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

        skillExecutors = new List<SkillExecutor>();
        for (int i = 0; i < skills.Length; i++) {
            skillExecutors.Add(new SkillExecutor(skills[i]));
        }

        attachedBuffs = new List<BaseBuff>();
        buffRemainingTurns = new List<int>();
        buffDisplayer.UpdateBuffImage(new List<Sprite>(from b in attachedBuffs select b.buffIcon));
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
        if(skillIndex < 0 || skillIndex > skillExecutors.Count){
            Debug.LogError("Skill index out of range and couldn't load skill icon.");
        }
        
        SpriteRenderer spirteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (value) {
            // Display inAction indicator
            actionIcon.sprite = skillExecutors[skillIndex].Icon;
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

    // Add buff to this object
    public void AddBuff(BaseBuff buff) {
        // Check for duplicate buff
        for (int i = 0; i < attachedBuffs.Count; i++) {
            if (buff.name == attachedBuffs[i].name) {
                if (buff is ILastingBehaviour) {
                    ILastingBehaviour lastingEffect = (ILastingBehaviour) buff;

                    // Execute OnDeactivate effect and add new OnInitialize effect
                    lastingEffect.Deactivate(this);
                    lastingEffect.Initialize(this);
                }
                // Reset duration
                buffRemainingTurns[i] = buff.duration;
                return;
            }
        }
        
        // Check if buff added to the character alreadly reach Maximum number
        if (attachedBuffs.Count == 10) {
            // Deactivate and remove the first buff
            if (attachedBuffs[0] is ILastingBehaviour)
                ((ILastingBehaviour) attachedBuffs[0]).Deactivate(this);
            attachedBuffs.RemoveAt(0);
        }
        // Add new buff
        attachedBuffs.Add(buff);
        buffRemainingTurns.Add(buff.duration);
        buffDisplayer.UpdateBuffImage(new List<Sprite>(from b in attachedBuffs select b.buffIcon));

        // Do on-buff-added effect
        if (buff is ILastingBehaviour) {
            ILastingBehaviour toExecute = (ILastingBehaviour) buff;
            toExecute.Initialize(this);
        }
    }

    public void UpdateTurn() {
        // Update skill cooldown
        for (int i = 0; i < skillExecutors.Count; ++i) {
            skillExecutors[i].Cooldown--;
        }

        List<BaseBuff> buffBuffer = new List<BaseBuff>();
        List<int> turnBuffer = new List<int>();

        // Do something about this character when end turn ex. buff effect, burn damage, etc
        for (int i = 0; i < buffRemainingTurns.Count; i++) {
            if (buffRemainingTurns[i] > 0) {
                // check if the buff have overtime effect
                if (attachedBuffs[i] is IOvertimeBehaviour) {
                    var toRunBuff = attachedBuffs[i] as IOvertimeBehaviour;
                    toRunBuff.OnChangeTurn(this);
                }
                
                // Reduce effect turn
                buffRemainingTurns[i]--;

                buffBuffer.Add(attachedBuffs[i]);
                turnBuffer.Add(buffRemainingTurns[i]);
            }
            else {
                // Do on-buff-removed effect
                if (attachedBuffs[i] is ILastingBehaviour) {
                    ILastingBehaviour toExecute = (ILastingBehaviour) attachedBuffs[i];
                    toExecute.Deactivate(this);
                }
            }

        }

        // Replace buff with buffer
        attachedBuffs = buffBuffer;
        buffRemainingTurns = turnBuffer;
        buffDisplayer.UpdateBuffImage(new List<Sprite>(from buff in attachedBuffs select buff.buffIcon));
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

    public List<SkillExecutor> GetSkills() {
        return skillExecutors;
    }

    public SkillExecutor GetSkill(int index) {
        if (index < 0 || index >= skillExecutors.Count)
            return null;
        return skillExecutors[index];
    }
}
