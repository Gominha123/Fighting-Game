using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DodgeRoll : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCombat playerCombat;
    public bool invulnerable;
    public float duration = 0.5f;
    public float cooldown = 2f;
    float tDuration;
    float tCooldown;
    int isRollingHash;

    private void Start()
    {
        tDuration = duration;
        tCooldown = cooldown;
        isRollingHash = Animator.StringToHash("isRolling");

    }

    private void Update()
    {
        HandleTimers();
        DodgeRollDuration();
    }

    void HandleTimers()
    {
        tCooldown += Time.deltaTime;
        tDuration += Time.deltaTime;
    }

    private void DodgeRollDuration()
    {
        if (tDuration > duration && invulnerable)
        {
            invulnerable = false;
        }
    }

    public void OnDodgeRoll()
    {
        if (tCooldown > cooldown)
        {
            anim.SetTrigger(isRollingHash);
            tDuration = 0;
            tCooldown = 0;
            invulnerable = true;
            playerCombat.CantHit();
        }
    }


}
