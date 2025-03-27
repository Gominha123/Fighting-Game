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
    public bool AttackInput { get; private set; }
    public bool SprintInput { get; private set; }
    public bool InteractInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool CrouchInput { get; private set; }

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

        inputActions.Enable();
    }


    private void OnDisable()
    {
        inputActions.PlayerMovement.Move.performed -= Move;
        inputActions.PlayerMovement.Move.canceled -= Move;

        inputActions.PlayerMovement.CameraZoom.started -= CameraZoom;
        inputActions.PlayerMovement.CameraZoom.canceled -= CameraZoom;

        inputActions.PlayerMovement.Run.started -= Run;
        inputActions.PlayerMovement.Run.performed -= Run;
        inputActions.PlayerMovement.Run.canceled -= Run;
        inputActions.PlayerMovement.Interact.started -= Interact;
        inputActions.PlayerMovement.Interact.performed -= Interact;
        inputActions.PlayerMovement.Interact.canceled -= Interact;
        inputActions.PlayerMovement.Jump.started -= Jump;
        inputActions.PlayerMovement.Jump.canceled -= Jump;
        inputActions.PlayerMovement.Crouch.started -= Crouch;
        inputActions.PlayerMovement.Crouch.canceled -= Crouch;
        inputActions.PlayerMovement.Attack.started -= Attack;
        inputActions.PlayerMovement.Attack.canceled -= Attack;

        inputActions.Disable();
    }

    // Create functions for every action
    /* Ex:
     * 
     */
    public void Move(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void CameraZoom(InputAction.CallbackContext context)
    {
        CameraZoomInput = context.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext context)
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

    public void Run(InputAction.CallbackContext context)
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
    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CrouchInput = !CrouchInput;
        }
    }
    public void Attack(InputAction.CallbackContext context)
    {

    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Started");
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