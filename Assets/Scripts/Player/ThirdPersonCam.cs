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
    [SerializeField] private float rotSpeed;

    private Quaternion deltaRotation;
    private float cinemachineYValue;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void FixedUpdate()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        if (inputActions.movementInput != Vector2.zero)
        {
            cinemachineYValue = cinemachineFreeLook.m_XAxis.Value;
            deltaRotation = Quaternion.Euler(new Vector3(0, cinemachineYValue, 0));
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, deltaRotation, rotSpeed));
        }
    }
}