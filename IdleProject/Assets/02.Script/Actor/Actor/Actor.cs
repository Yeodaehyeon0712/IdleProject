using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : PoolingObject<eActorType>
{
    #region Variables
    protected Dictionary<eComponent, BaseComponent> componentDictionary = new Dictionary<eComponent, BaseComponent>();
    public SkinComponent Skin => skinComponent;
    [SerializeField] protected SkinComponent skinComponent;
    public StatusComponent Status => statusComponent;
    [SerializeField] protected StatusComponent statusComponent;
    public FSMComponent FSM => fsmComponent;
    [SerializeField] protected FSMComponent fsmComponent;
    public eFSMState FSMState => fsmComponent.State;
    //Stat Fields
    [SerializeField] public float CurrentHP { get => currentHP; set { currentHP = value; OnHpChange(); } }
    protected virtual void OnHpChange() { }

    [SerializeField] protected float currentHP;

    //Attachment Fields
    public Attachment Attachment => attachment;
    protected Attachment attachment;
    #endregion

    #region Pooling Object Method
    public override void Initialize(eActorType type, int objectID)
    {
        base.Initialize(type, objectID);
        attachment = GetComponentInChildren<Attachment>();
        attachment.Initialize();
        InitializeComponent();
        fsmComponent.GenerateFSMState();
    }
    public override void Spawn(uint worldID, Vector2 position)
    {
        base.Spawn(worldID, position);
        CurrentHP = Status.GetStatus(eStatusType.MaxHP);
        FSM.State = eFSMState.Idle;
    }
    protected override void ReturnToPool()
    {
        ActorManager.Instance.RegisterActorPool(worldID, type, objectID);
    }
    #endregion

    #region Unity API
    protected virtual void Update()
    {
        if (TimeManager.Instance.IsActiveTimeFlow == false) return;
        OnUpdateComponent(TimeManager.DeltaTime);
    }
    protected virtual void FixedUpdate()
    {
        if (TimeManager.Instance.IsActiveTimeFlow==false) return;
        fsmComponent.FixedComponentUpdate(TimeManager.FixedDeltaTime);
    }
    #endregion

    #region Actor Method
    public virtual void Hit(in AttackHandler attackHandler)
    {
        double damage = attackHandler.Damage;
        CurrentHP -= (float)damage;

        UIManager.Instance.FieldUI.SetDamageText(attachment.GetAttachmentElement(eAttachmentTarget.OverHead).Transform.position, attackHandler.Damage, attackHandler.IsCritical,type);
        if (CurrentHP <= 0f)
            Death();
        else
            HitAnimation();   
    }
    protected virtual void HitAnimation() { }
    public void Recovery(in AttackHandler attackHandler)
    {
        CurrentHP = System.Math.Clamp(currentHP - (float)attackHandler.Damage, 0, statusComponent.GetStatus(eStatusType.MaxHP));
    }
    public virtual void Death(float time = 2.5f)
    {
        Skin.SetAnimationTrigger(eCharacterAnimState.Death);
        FSM.State = eFSMState.Death;
        Clean(time);
    }
    #endregion

    #region Component Method
    protected abstract void InitializeComponent();
    public void AddComponent(BaseComponent component)
    {
        if (componentDictionary.ContainsKey(component.ComponentType))
        {
            componentDictionary[component.ComponentType].DestroyComponent();
            componentDictionary.Remove(component.ComponentType);
        }
        componentDictionary.Add(component.ComponentType, component);
    }
    void OnUpdateComponent(float deltaTime)
    {
        foreach (var component in componentDictionary.Values)
            component.ComponentUpdate(deltaTime);
    }
    #endregion

    #region Attack Method
    public abstract void DefaultAttack();    
    #endregion
}
