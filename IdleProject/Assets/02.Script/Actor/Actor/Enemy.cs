using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    #region Variables
    public Data.EnemyData enemyData;
    #endregion

    protected override void InitializeComponent()
    {
        enemyData = DataManager.EnemyTable[objectID];
        skinComponent = new SkinComponent(this, AddressableSystem.GetAnimator(enemyData.AnimatorPath));
        statusComponent = new EnemyStatusComponent(this);
        fsmComponent = new EnemyFSMComponent(this);
    }
    public override void Death(float time = 2.5F)
    {
        base.Death(time);
        DropGold();
    }
    void DropGold()
    {
        var snapShot = SnapShotDataProperty.Instance;
        var gold = DataManager.StageTable[snapShot.GetCurrentStage()].GetGold();
        snapShot.GetData.GoldAmount += gold;
    }
    public override void DefaultAttack()
    {
        if (FSM.State == eFSMState.Death) return;

        //Skin.SetAnimationTrigger(eCharacterAnimState.Attack);

        double damage = Status.GetStatus(eStatusType.AttackDamage) ;

        AttackHandler attackHandler = new AttackHandler(worldID, FSM.Target.WorldID, damage, false);
        ActorManager.Instance.PushAttackHandler = attackHandler;
    }
    protected override void HitAnimation() 
    {
        Skin.SetAnimationTrigger(eCharacterAnimState.Hit);
    }
}
