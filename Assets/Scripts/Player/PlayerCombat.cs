using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] InputHandler inputActions;
    [SerializeField] Animator anim;
    Weapon weapon;

    public List<AttackSO> combo; // maybe delete
    float lastClickedTime;
    float lastComboEnd; // maybe delete
    public int comboCounter; // maybe delete
    public float timeBetweenCombos = 2f; // maybe delete
    public float timeBetweenAttacks = 0.2f; // maybe delete
    int currentAnimationLayer;
    public bool isAttacking;

    void Update()
    {
        if (inputActions.AttackInput)
        {
            Attack();
        }
        else
        {
            if (anim.GetCurrentAnimatorStateInfo(currentAnimationLayer).normalizedTime < 0.3f)
            {
                EndCombo();
            }
        }
        ExitAttack();
    }

    //void Attack()
    //{
    //    clipLength = anim.GetCurrentAnimatorClipInfo((int)weapon.animLayer)[0].clip.length;

    //}


    public void SetWeapon(Weapon newWeapon)
    {
        if (weapon != null)
        {
            ResetAnimationLayer(weapon.animLayer);
        }
        this.weapon = newWeapon;
        SetAnimationLayer(weapon.animLayer);
        currentAnimationLayer = (int)weapon.animLayer;
    }

    private void SetAnimationLayer(Weapon.AnimLayer animLayer)
    {
        anim.SetLayerWeight((int)animLayer, 1f);
    }

    private void ResetAnimationLayer(Weapon.AnimLayer animLayer)
    {
        anim.SetLayerWeight((int)animLayer, 0f);
    }

    public bool IsAttacking()
    {
        if (anim.GetCurrentAnimatorStateInfo(currentAnimationLayer).IsTag(Constants.ATTACK_ANIMATION_TAG))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

        }
    }

    void Attack()
    {
        if (anim.GetCurrentAnimatorClipInfo(currentAnimationLayer).Length != 0 &&// There is an animation playing
                (anim.GetCurrentAnimatorStateInfo(currentAnimationLayer).IsTag(Constants.ATTACK_ANIMATION_TAG) // The animation is an Attack
                || (Time.time - lastClickedTime > anim.GetCurrentAnimatorClipInfo(currentAnimationLayer)[0].clip.length * 0.3f)))
        {
            CancelInvoke("EndCombo");
            isAttacking = true;
            anim.SetBool("isAttacking", isAttacking);
            lastClickedTime = Time.time;
        }
    }

    #region old attack
    void Attack1()
    {
        // Prevents player from starting another combo right after combo ends
        if (Time.time - lastComboEnd > timeBetweenCombos && comboCounter < combo.Count)
        {
            CancelInvoke("EndCombo");
            // Prevents player from attacking too quicly 
            //if (Time.time - lastClickedTime >= timeBetweenAttacks)

            // Can only start a new attack after previous attack animation ends
            if (anim.GetCurrentAnimatorClipInfo(0).Length != 0 && (!anim.GetCurrentAnimatorStateInfo(0).IsTag(Constants.ATTACK_ANIMATION_TAG) ||
                Time.time - lastClickedTime >= anim.GetCurrentAnimatorClipInfo(0)[anim.GetCurrentAnimatorClipInfoCount(0) - 1].clip.length - timeBetweenAttacks))
            {
                anim.runtimeAnimatorController = combo[comboCounter].animOV;
                weapon.damage = combo[comboCounter].damage;
                //weapon.knockback = ...

                comboCounter++;
                lastClickedTime = Time.time;
            }
        }
    }

    void ExitAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(currentAnimationLayer).normalizedTime < 0.5f
            && anim.GetCurrentAnimatorStateInfo(currentAnimationLayer).IsTag("Attack"))
        {
            isAttacking = false;
            anim.SetBool("isAttacking", isAttacking);
            Invoke("EndCombo", 1f);
        }
    }

    void EndCombo()
    {
         
        //anything that happens when combo ends happens here
    }

    #endregion
}
