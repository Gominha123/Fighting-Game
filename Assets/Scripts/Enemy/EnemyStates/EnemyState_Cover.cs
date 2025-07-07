using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Cover : IState
{

    private EnemyReferences enemyReferences;
    private StateMachine stateMachine;

    public EnemyState_Cover(EnemyReferences enemyReferences)
    {
        this.enemyReferences = enemyReferences;

        stateMachine = new StateMachine();

        // STATES
        var enemyAttack = new EnemyState_Attacking(enemyReferences);
        var enemyDelay = new EnemyState_Delay(2f);
        var enemyReload = new EnemyState_Reload(enemyReferences);

        // TRANSITIONS
        At(enemyAttack, enemyReload, () => enemyReferences.shooter.ShouldReload());
        At(enemyReload, enemyDelay, () => !enemyReferences.shooter.ShouldReload());
        At(enemyDelay, enemyAttack, () => enemyDelay.IsDone());

        // START STATE
        stateMachine.SetState(enemyAttack);

        // FUNCTION & CONDITIONS
        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);
    }

    public void OnEnter()
    {
        Debug.Log("EnemyState_Cover");
        enemyReferences.animator.SetBool("combat", true);
    }

    public void OnExit()
    {
        Debug.Log("EnemyState_Cover");
        enemyReferences.animator.SetBool("combat", false);
    }

    public void Tick()
    {
        stateMachine.Tick();
    }

    public Color GizmoColor()
    {
       return stateMachine.GetGizmoColor();
    }
}
