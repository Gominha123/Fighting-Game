using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_RunToCover : IState
{
    private EnemyReferences enemyReferences;
    private CoverArea coverArea;

    public EnemyState_RunToCover(EnemyReferences enemyReferences, CoverArea coverArea)
    {
        this.enemyReferences = enemyReferences;
        this.coverArea = coverArea;
    }

    public void OnEnter()
    {
        Debug.Log("EnemyState_RunToCover");
        Cover nextCover = this.coverArea.GetRandomCover(enemyReferences.transform.position);
        enemyReferences.navMeshAgent.SetDestination(nextCover.transform.position);
    }

    public void OnExit()
    {
        enemyReferences.animator.SetFloat("speed", 0f);
        Debug.Log("exit EnemyState_RunToCover");
    }

    public void Tick()
    {
        enemyReferences.animator.SetFloat("speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
    }

    public Color GizmoColor()
    {
        return Color.blue;
    }

    public bool HasArrivedDestination()
    {
        return enemyReferences.navMeshAgent.remainingDistance < 0.1f;
    }
}
