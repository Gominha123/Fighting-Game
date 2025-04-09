using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Base Stats")]
    public float maxHealth;
    public float curretnHealth;

    public bool isDead;
    public bool revive;

    [Header("Heal Overtime")]
    public bool healOvertime;
    public float healOvertimeAmount = 5f;
    public float healOvertimeCooldown = 1f;
    public float healtOvertimeDuration = 15f;
    private float tHeal;
    private float tHealDuration;

    private void Start()
    {
        curretnHealth = maxHealth;
    }

    private void Update()
    {
        HandleTimers();
        Revive(maxHealth);
        HealOvertime(healOvertimeAmount, healOvertimeCooldown, healtOvertimeDuration);
    }

    private void HealOvertime(float healAmount, float healCooldown, float healDuration)
    {
        if (healOvertime && tHeal > healCooldown)
        {
            Heal(healAmount);
            tHeal = 0;
            if (tHealDuration > healDuration)
            {
                healOvertime = false;
                tHealDuration = 0f;
            }
        }
    }

    private void HandleTimers()
    {
        if (healOvertime)
        {
            tHeal += Time.deltaTime;
            tHealDuration += Time.deltaTime;
        }
    }

    void TakeDamage(float damage)
    {
        if (curretnHealth - damage <= 0)
        {
            curretnHealth = 0;
            isDead = true;
        }
        else curretnHealth -= damage;
    }

    void Heal(float healAmount)
    {
        if (!isDead)
        {
            if (curretnHealth + healAmount > maxHealth)
            {
                curretnHealth = maxHealth;
            }
            else curretnHealth += healAmount;
        }
    }

    void Revive(float healAmount)
    {
        if (isDead && revive)
        {
            isDead = false;
            curretnHealth = healAmount;
            revive = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Constants.WEAPON_TAG))
        {
            if (other.TryGetComponent<Weapon>(out var playerWeapon))
            {
                if (playerWeapon.canHit)
                {
                    TakeDamage(playerWeapon.damage);
                    playerWeapon.canHit = false;
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.WEAPON_TAG))
        {
            if (other.TryGetComponent<Weapon>(out var playerWeapon))
            {

            }
        }
    }
}
