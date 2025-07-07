using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyReferences : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public EnemyShooter shooter;

    [Header("Stats")]
    public float range;
    public float pathUpdateDelay = 0.2f;
    public Transform target;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        shooter = GetComponent<EnemyShooter>();
    }
}
