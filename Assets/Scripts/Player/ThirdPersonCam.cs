using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [SerializeField] private InputHandler inputActions;

    [Header("References")]
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cam;
    [SerializeField] private float rotSpeed;

    private Vector2 inputDir;

    private Quaternion lookTarget;
    private float cinemachineXValue;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void FixedUpdate()
    {
        //RotatePlayerToCamera();
        RotatePlayerToInput();
    }

    //Rotate player to where camera is looking when moving
    private void RotatePlayerToCamera()
    {
        if (inputActions.movementInput != Vector2.zero)
        {
            cinemachineXValue = cinemachineFreeLook.m_XAxis.Value;
            lookTarget = Quaternion.Euler(new Vector3(0, cinemachineXValue, 0));
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, lookTarget, rotSpeed));
        }
    }

    private void RotatePlayerToInput()
    {
        if (inputActions.movementInput != Vector2.zero)
        {
            Vector3 moveDir = inputActions.movementInput.y * cam.forward + inputActions.movementInput.x * cam.right;
            moveDir.y = 0;
            moveDir.Normalize();
            lookTarget = Quaternion.LookRotation(moveDir, Vector3.up);

            rb.MoveRotation(Quaternion.Lerp(rb.rotation, lookTarget, rotSpeed));
        }
    }
}