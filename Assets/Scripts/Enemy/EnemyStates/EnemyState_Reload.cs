using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Reload : IState
{
    private EnemyReferences enemyReferences;

    public EnemyState_Reload(EnemyReferences enemyReferences)
    {
        this.enemyReferences = enemyReferences;
    }

    public Color GizmoColor()
    {
        return Color.gray;
    }

    public void OnEnter()
    {
        Debug.Log("EnemyState_Reload");
        enemyReferences.animator.SetFloat("cover", 1f);
        enemyReferences.animator.SetLayerWeight(1, 1);
        enemyReferences.animator.SetTrigger("reload");

    }

    public void OnExit()
    {
        Debug.Log("EXIT EnemyState_Reload");
        enemyReferences.animator.SetFloat("cover", 0f);
        enemyReferences.animator.SetLayerWeight(1, 0);
    }

    public void Tick()
    {

    }
}
