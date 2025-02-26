using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    public Vector2 movementInput;
    public float cameraZoom;
    public PlayerInput inputActions;
    public bool fireInput;
    public bool sprintInput;
    public bool interactInput;
    public bool jumpInput;

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

        inputActions.PlayerMovement.Move.performed += i => movementInput = i.ReadValue<Vector2>();
        inputActions.PlayerActions.Run.started += i => sprintInput = true;
        inputActions.PlayerActions.Run.canceled += i => sprintInput = false;
        inputActions.PlayerActions.Interact.started += i => interactInput = true;
        inputActions.PlayerActions.Interact.canceled += i => interactInput = false;
        inputActions.PlayerActions.Jump.started += i => jumpInput = true;
        inputActions.PlayerActions.Jump.canceled += i => jumpInput = false;
        inputActions.PlayerCamera.CameraZoom.started += i => cameraZoom = i.ReadValue<float>();
        inputActions.PlayerCamera.CameraZoom.canceled += i => cameraZoom = i.ReadValue<float>();

        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}