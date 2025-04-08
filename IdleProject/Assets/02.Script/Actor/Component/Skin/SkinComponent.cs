using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SkinComponent : BaseComponent
{
    #region Variables
    Animator animator;
    Dictionary<eCharacterAnimState, int> animatorHashDic = new Dictionary<eCharacterAnimState, int>();
    [SerializeField]eCharacterAnimState currentAnim;
    #endregion

    #region Component Method
    public SkinComponent(Actor owner, RuntimeAnimatorController controller) : base(owner, eComponent.SkinComponent, useUpdate: false)
    {
        var skin = owner.transform.Find("Skin");
        animator = skin.GetComponent<Animator>();
        animator.runtimeAnimatorController = controller;       

        var moveHash = Animator.StringToHash("AnimState");
        animatorHashDic.Add(eCharacterAnimState.Move, moveHash);
        animatorHashDic.Add(eCharacterAnimState.Idle, moveHash);
        animatorHashDic.Add(eCharacterAnimState.Attack, Animator.StringToHash("Attack"));
        animatorHashDic.Add(eCharacterAnimState.Hit, Animator.StringToHash("Hit"));
        animatorHashDic.Add(eCharacterAnimState.Death, Animator.StringToHash("Death"));
    }
    #endregion

    #region SkinMethod
    public void SetAnimationTrigger(eCharacterAnimState state)
    {
        currentAnim = state;
        animator.SetTrigger(animatorHashDic[currentAnim]);
    }
    public void SetAnimationFloat(float value)
    {
        var nextState = (value == 0) ? eCharacterAnimState.Idle : eCharacterAnimState.Move;
        if (nextState == currentAnim) return;

        currentAnim = nextState;
        animator.SetFloat(animatorHashDic[currentAnim], value);
    }
    #endregion
}
