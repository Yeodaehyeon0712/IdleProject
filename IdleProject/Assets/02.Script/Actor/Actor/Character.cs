using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    #region Variables
    public Data.CharacterData characterData;
    #endregion
    protected override void InitializeComponent()
    {
        characterData = DataManager.CharacterTable[objectID];
        skinComponent = new SkinComponent(this, AddressableSystem.GetAnimator(characterData.AnimatorPath));
        statusComponent = new CharacterStatusComponent(this);
        fsmComponent = new CharacterFSMComponent(this);
    }
    public override void Death(float time = 2.5F)
    {
        base.Death(time);
        Timer.SetTimer(time, true, () => StageManager.Instance.StopStage(skipResult: false));
    }
    public override void DefaultAttack()
    {
        if (FSM.State == eFSMState.Death) return;

        Skin.SetAnimationTrigger(eCharacterAnimState.Attack);

        double criticalCoefficient = Player.ComputeCritical(out bool isCritical);
        double damage = Status.GetStatus(eStatusType.AttackDamage) * criticalCoefficient;

        AttackHandler attackHandler = new AttackHandler(worldID, FSM.Target.WorldID, damage, isCritical);
        ActorManager.Instance.PushAttackHandler = attackHandler;
    }

}
