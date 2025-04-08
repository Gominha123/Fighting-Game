using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TerrainUtils;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private InputHandler inputActions;
    [SerializeField] private PlayerMovement pM;

    int isMovingHash;
    int horInputHash;
    int vertInputHash;
    int isSprintingHash;
    int isCrouchingHash;
    int velocityHash;
    int isHeavyAttackHash;

    private bool isMoving;
    private bool isSprinting;
    private bool isCrouching;
    private bool isHeavyAttacking;
    private bool canCrouch;

    private float maxValueToCameraMovement;
    private float sprintMultiplier;

    float horInput;
    float vertInput;

    float horMultiplier = 0.0f;
    float vertMultiplier = 0.0f;

    float acceleration = 0.1f;
    float decelaration = 0.1f;

    public float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        isMovingHash = Animator.StringToHash("isMoving");
        isSprintingHash = Animator.StringToHash("isSprinting");
        isCrouchingHash = Animator.StringToHash("isCrouching");
        isHeavyAttackHash = Animator.StringToHash("isHeavyAttacking");

        horInputHash = Animator.StringToHash("horInput");
        vertInputHash = Animator.StringToHash("vertInput");
        velocityHash = Animator.StringToHash("velocity");
        canCrouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        GetInputValues();
        SetHashValues();
        SetMovingAnimationsToInputMovement();
        SetMovingAnimationsToCameraMovement();
    }

    private void SetHashValues()
    {
        anim.SetBool(isMovingHash, isMoving);
        anim.SetBool(isSprintingHash, isSprinting);
        anim.SetBool(isCrouchingHash, isCrouching);
        if (isHeavyAttacking)
        {
            anim.SetTrigger(isHeavyAttackHash);
            inputActions.HeavyAttackInput = false;
        }
    }

    private void GetInputValues()
    {
        isMoving = inputActions.MovementInput != Vector2.zero;
        isSprinting = inputActions.SprintInput;
        isCrouching = inputActions.CrouchInput; // Not implemented
        horInput = inputActions.MovementInput.x;
        vertInput = inputActions.MovementInput.y;
        isHeavyAttacking = inputActions.HeavyAttackInput;
    }

    private float IsPositive(float value)
    {
        if (value < 0) return -1;
        else if (value > 0) return 1;
        else return 0;
    }

    // Use if crouch is to be used as toggle, if not, just return isCrouching value and don't change it when used
    private bool IsCrouching()
    {
        if (isCrouching)
        {
            canCrouch = !canCrouch;
        }

        return canCrouch;
        /*
         * if(!toggle) return isCrouching;
         */
    }

    /// <summary>
    /// Sets the values for the blend tree when character rotates according to the input
    /// </summary>
    private void SetMovingAnimationsToInputMovement()
    {
        anim.SetFloat(velocityHash, pM.currentSpeed);
    }

    /// <summary>
    /// Sets the values for the blend tree when charater only rotates to where camera is looking
    /// </summary>
    private void SetMovingAnimationsToCameraMovement()
    {
        // Setting values for smoother transition between animations
        if (isSprinting)
        {
            maxValueToCameraMovement = 2.0f;
            sprintMultiplier = 2.0f;
        }
        else if (maxValueToCameraMovement > 1.0f)
        {
            maxValueToCameraMovement -= 0.1f;
        }
        else
        {
            sprintMultiplier = 1.0f;
            maxValueToCameraMovement = 1f;
        }

        if (horInput > 0)
        {
            horMultiplier += acceleration * sprintMultiplier;
        }
        else if (horInput < 0)
        {
            horMultiplier -= acceleration * sprintMultiplier;
        }
        else if (horMultiplier < -0.1f)
        {
            horMultiplier += decelaration * sprintMultiplier;
        }
        else if (horMultiplier > 0.1f)
        {
            horMultiplier -= decelaration * sprintMultiplier;
        }
        else horMultiplier = 0;

        if (horMultiplier > maxValueToCameraMovement)
        {
            horMultiplier = maxValueToCameraMovement;
        }
        else if (horMultiplier < -maxValueToCameraMovement)
        {
            horMultiplier = -maxValueToCameraMovement;
        }

        if (vertInput > 0)
        {
            vertMultiplier += acceleration * sprintMultiplier;
        }
        else if (vertInput < 0)
        {
            vertMultiplier -= acceleration * sprintMultiplier;
        }
        else if (vertMultiplier < -0.1f)
        {
            vertMultiplier += decelaration * sprintMultiplier;
        }
        else if (vertMultiplier > 0.1f)
        {
            vertMultiplier -= decelaration * sprintMultiplier;
        }
        else vertMultiplier = 0;

        if (vertMultiplier > maxValueToCameraMovement)
        {
            vertMultiplier = maxValueToCameraMovement;
        }
        else if (vertMultiplier < -maxValueToCameraMovement)
        {
            vertMultiplier = -maxValueToCameraMovement;
        }

        anim.SetFloat(horInputHash, horMultiplier);
        anim.SetFloat(vertInputHash, vertMultiplier);
    }
}
