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
    [SerializeField] PlayerMovement playerMovement;
    Weapon weapon;

    [HideInInspector] public List<AttackSO> combo; // maybe delete
    float lastClickedTime;
    float lastComboEnd; // maybe delete
    private int comboCounter; // maybe delete
    private float timeBetweenCombos = 2f; // maybe delete
    private float timeBetweenAttacks = 0.2f; // maybe delete
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

        HandleTimers();
        ExitAttack();
        CanMove();
    }

    private void HandleTimers()
    {
        timeBetweenAttacks = Time.time;
    }

    // Prevents player from moving if is currently attacking or rolling
    private void CanMove()
    {
        //can move 
        if (anim.GetCurrentAnimatorStateInfo(currentAnimationLayer).IsTag(Constants.ATTACK_ANIMATION_TAG) 
            && anim.GetCurrentAnimatorStateInfo(currentAnimationLayer).normalizedTime < 0.9f)
        {
            playerMovement.SetMoveCondition(false);
            ThirdPersonCam.canRotate = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(currentAnimationLayer).IsTag(Constants.DODGEROLL_ANIMATION_TAG)
            && anim.GetCurrentAnimatorStateInfo(currentAnimationLayer).normalizedTime < 0.77f)
        {
            playerMovement.SetMoveCondition(false);
            ThirdPersonCam.canRotate = true;
        }
        else
        {
            playerMovement.SetMoveCondition(true);
            ThirdPersonCam.canRotate = true;
        }
    }

    // Sets new weapon whenever it changes
    public void SetWeapon(Weapon newWeapon)
    {
        if (weapon != null)
        {
            ResetAnimationLayer(weapon.animLayer);
        }
        this.weapon = newWeapon;
        SetAnimationLayer(weapon.animLayer);
        currentAnimationLayer = WeaponHolder.GetCurrentAnimator();
    }

    // Set animator layer weight of current weapon to 1
    private void SetAnimationLayer(Weapon.AnimLayer animLayer)
    {
        anim.SetLayerWeight((int)animLayer, 1f);
    }

    // Set animator layer weight of previous weapon to 0
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

    //public void OnAttack()
    //{
    //    anim.SetBool("isAttacking", true);
    //}

    void Attack()
    {
        if (anim.GetCurrentAnimatorClipInfo(currentAnimationLayer).Length != 0 &&// There is an animation playing
                (anim.GetCurrentAnimatorStateInfo(currentAnimationLayer).IsTag(Constants.ATTACK_ANIMATION_TAG) // The animation is an Attack
                || (Time.time - lastClickedTime > anim.GetCurrentAnimatorClipInfo(currentAnimationLayer)[0].clip.length * 0.3f))) // Player is able to press the attack button again
        {
            CancelInvoke("EndCombo");
            isAttacking = true;
            anim.SetBool("isAttacking", isAttacking);
            lastClickedTime = Time.time;
        }
    }

    void ExitAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(currentAnimationLayer).normalizedTime < 0.3f 
            && anim.GetCurrentAnimatorStateInfo(currentAnimationLayer).IsTag(Constants.ATTACK_ANIMATION_TAG)) // Checks if animation currently playing is an attack
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

    public void CanHit()
    {
        weapon.canHit = !weapon.canHit;
    }
    public void CantHit()
    {
        weapon.canHit = false;
    }
}
