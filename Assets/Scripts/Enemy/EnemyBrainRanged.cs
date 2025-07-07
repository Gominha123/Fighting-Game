using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrainRanged : MonoBehaviour
{
    [HideInInspector] public EnemyReferences enemyReferences;
    Transform target;

    private float pathUpdaeDeadline;

    private float attackingDistance;

    private void Awake()
    {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    private void Start()
    {
        attackingDistance = enemyReferences.navMeshAgent.stoppingDistance;
        target = enemyReferences.target;
    }

    private void Update()
    {
        if (target != null)
        {
            bool inRange = Vector3.Distance(transform.position, target.position) <= attackingDistance;

            if (inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }
            enemyReferences.animator.SetBool("attacking", inRange);
        }
        // Set speed in animator for moving animations
        enemyReferences.animator.SetFloat("Speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
    }

    private void UpdatePath()
    {
        if (Time.time >= pathUpdaeDeadline)
        {
            pathUpdaeDeadline = Time.time + enemyReferences.pathUpdateDelay;
            enemyReferences.navMeshAgent.SetDestination(target.position);
        }
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }
}
