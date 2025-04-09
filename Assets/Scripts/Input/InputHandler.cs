using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    public PlayerInput inputActions;
    public Vector2 MovementInput { get; private set; }
    public float CameraZoomInput { get; private set; }
    public bool AttackInput { get; set; }
    public bool HeavyAttackInput { get; set; }
    public bool SprintInput { get; private set; }
    public bool InteractInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool CrouchInput { get; private set; }
    public int SelectedWeapon { get; set; }
    public bool DodgeRollInput { get; set; }


    private void OnEnable()
    {
        if (inputActions == null)
        {
            EnableInputs();
        }
    }

    private void EnableInputs()
    {
        inputActions = new PlayerInput();

        inputActions.PlayerMovement.Move.performed += Move;
        inputActions.PlayerMovement.Move.canceled += Move;

        inputActions.PlayerMovement.CameraZoom.started += CameraZoom;
        inputActions.PlayerMovement.CameraZoom.canceled += CameraZoom;

        inputActions.PlayerMovement.Run.started += Run;
        inputActions.PlayerMovement.Run.performed += Run;
        inputActions.PlayerMovement.Run.canceled += Run;

        inputActions.PlayerMovement.Interact.started += Interact;
        inputActions.PlayerMovement.Interact.performed += Interact;
        inputActions.PlayerMovement.Interact.canceled += Interact;

        inputActions.PlayerMovement.Jump.started += Jump;
        inputActions.PlayerMovement.Jump.canceled += Jump;

        inputActions.PlayerMovement.Crouch.started += Crouch;
        inputActions.PlayerMovement.Crouch.canceled += Crouch;

        inputActions.PlayerMovement.Attack.started += Attack;
        inputActions.PlayerMovement.Attack.canceled += Attack;

        //inputActions.PlayerMovement.HeavyAttack.started += HeavyAttack;
        //inputActions.PlayerMovement.HeavyAttack.canceled += HeavyAttack;

        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Move(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    private void CameraZoom(InputAction.CallbackContext context)
    {
        CameraZoomInput = context.ReadValue<float>();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
        }
        else if (context.canceled)
        {
            JumpInput = false;
        }
    }

    private void Run(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SprintInput = true;
        }
        else if (context.canceled)
        {
            SprintInput = false;
        }
    }
    private void Crouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CrouchInput = !CrouchInput;
        }
    }
    private void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput = true;
        }
        else if (context.canceled)
        {
            AttackInput = false;
        }
    }
    private void HeavyAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            HeavyAttackInput = true;
        }
        else if (context.canceled)
        {
            HeavyAttackInput = false;
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log(SelectedWeapon);
        }
        else if (context.canceled)
        {
            Debug.Log("Canceled");
        }
        else if (context.performed)
        {
            Debug.Log("Performed");
        }
    }
}