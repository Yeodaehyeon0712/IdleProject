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
    [SerializeField] public float CurrentHP { get => currentHP; set => currentHP = value; }
    [SerializeField] protected float currentHP;
    #endregion

    #region Pooling Object Method
    public override void Initialize(eActorType type, int objectID)
    {
        base.Initialize(type, objectID);
        InitializeComponent();
        fsmComponent.GenerateFSMState();
    }
    protected override void ReturnToPool()
    {

    }
    #endregion

    #region Unity API
    protected virtual void Update()
    {
        UpdateComponent(TimeManager.DeltaTime);
    }
    protected virtual void FixedUpdate()
    {
        fsmComponent.FixedComponentUpdate(TimeManager.FixedDeltaTime);
    }
    #endregion

    #region Actor Method
    public virtual void Death(float time = 2.5f)
    {
        Clean(time);
    }
    public virtual void Hit(in AttackHandler attackHandler)
    {
        double damage = attackHandler.Damage;
        currentHP -= (float)damage;

        if (currentHP <= 0f)
            Death();
        else
            Skin.SetAnimationTrigger(eCharacterAnimState.Hit);
    }
    public void Recovery(in AttackHandler attackHandler)
    {
        currentHP = System.Math.Clamp(currentHP - (float)attackHandler.Damage, 0, statusComponent.GetStatus(eStatusType.MaxHP));
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
    void UpdateComponent(float deltaTime)
    {
        foreach (var component in componentDictionary.Values)
            component.ComponentUpdate(deltaTime);
    }
    public void ActiveComponent()
    {
        foreach (var component in componentDictionary.Values)
            component.ActiveComponent();
    }
    public void InactiveComponent()
    {
        foreach (var component in componentDictionary.Values)
            component.InactiveComponent();
    }

    #endregion
}
