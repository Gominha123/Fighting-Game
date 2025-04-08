using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputHandler inputActions;

    [SerializeField]
    private int moveSpeed = 0;

    [SerializeField]
    private int sprintMoveSpeed = 0;


    public float currentSpeed { get; private set; }
    [SerializeField] private float acceleration = 0.3f;

    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Transform orientation;
    [SerializeField] private Vector3 showNumber;

    private float horInput;
    private float vertInput;
    private Vector3 moveDirection;

    public LayerMask whatIsGround;
    public bool isGrounded;
    private float castDistance = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //capsuleCollider = GetComponent<CapsuleCollider>();
        Application.targetFrameRate = 60;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        IsGrounded();
    }

    public void Jump()
    {
        if (inputActions.JumpInput && isGrounded)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Detect if player is on ground
    /// </summary>
    private void IsGrounded()
    {
        isGrounded = Physics.BoxCast(
            capsuleCollider.bounds.center,
            transform.localScale * 0.5f,
            Vector3.down,
            Quaternion.identity,
            castDistance, whatIsGround);

        if (isGrounded)
        {
            //rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }
    }

    private void Move()
    {
        // Get player movement inputs
        horInput = inputActions.MovementInput.x;
        vertInput = inputActions.MovementInput.y;

        //calculate movement direction
        moveDirection = orientation.forward * vertInput + orientation.right * horInput;
        moveDirection.y = 0;
        moveDirection.Normalize();

        if (moveDirection == Vector3.zero)
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= acceleration * 2f;
            }
            else currentSpeed = 0f;
        }
        else if (moveDirection != Vector3.zero)
        {
            if (inputActions.SprintInput)
            {
                if (currentSpeed >= sprintMoveSpeed)
                {
                    currentSpeed = sprintMoveSpeed;
                }
                else currentSpeed += acceleration;
            }
            else
            {
                if (currentSpeed > moveSpeed + 1f)
                {
                    currentSpeed -= acceleration;
                }
                else if (currentSpeed >= moveSpeed)
                {
                    currentSpeed = moveSpeed;
                }
                else currentSpeed += acceleration;
            }
        }

        if (inputActions.SprintInput)
        {
            rb.MovePosition(rb.position + currentSpeed * Time.deltaTime * moveDirection);
        }
        else
        {
            rb.MovePosition(rb.position + currentSpeed * Time.deltaTime * moveDirection);
        }
    }
}
