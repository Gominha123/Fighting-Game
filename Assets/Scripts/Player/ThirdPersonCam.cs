using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [SerializeField] private InputHandler inputActions;

    [Header("References")]
    [SerializeField] private Transform tollowTarget;
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;
    [SerializeField] private CinemachineCameraOffset cinemachineCameraOffset;
    private Rigidbody rb;

    [SerializeField] private float rotSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomValue;
    [SerializeField] private float currentZoom;
    private float offsetValue;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    private Quaternion deltaRotation;
    private float cinemachineYValue;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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