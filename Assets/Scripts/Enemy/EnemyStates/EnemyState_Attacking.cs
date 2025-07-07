using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Attacking : IState
{
    private EnemyReferences enemyReferences;
    private Transform target;

    public EnemyState_Attacking(EnemyReferences enemyReferences)
    {
        this.enemyReferences = enemyReferences;
    }

    public void OnEnter()
    {
        Debug.Log("EnemyState_Attacking");
        target = enemyReferences.target;
    }

    public void OnExit()
    {
        Debug.Log("EXIT EnemyState_Attacking");
        enemyReferences.animator.SetBool("attacking", false);
        target = null;
    }

    public void Tick()
    {
        if (target != null)
        {
            bool inRange = Vector3.Distance(enemyReferences.transform.position, target.position) <= enemyReferences.range;
            if (inRange)
            {
                Debug.Log(inRange);
                LookAtTarget();
                // Decide to shoot or to hide. For now, shoot first
                enemyReferences.animator.SetBool("attacking", true);
            }

        }
    }

    private void LookAtTarget()
    {
        // Aim at the target
        Vector3 lookPos = target.position - enemyReferences.transform.position;
        lookPos.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        enemyReferences.transform.rotation = Quaternion.Slerp(enemyReferences.transform.rotation, rotation, 0.2f);
    }

    public Color GizmoColor()
    {
        return Color.red;
    }
}
