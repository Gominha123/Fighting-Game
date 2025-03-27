using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] InputHandler inputActions;
    [SerializeField] Animator anim;

    void Start()
    {

    }

    void Update()
    {
        if (inputActions.AttackInput)
        {
            //inputActions.attackInput = false;
        }
    }

    public void Attack()
    {
        Debug.Log("yo");
    }
}
