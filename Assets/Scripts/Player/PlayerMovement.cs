using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputHandler inputActions;


    public int MoveSpeed { get { return moveSpeed; } private set { moveSpeed = value; } }

    [SerializeField]
    private int moveSpeed = 0;
    public int SprintMoveSpeed { get { return sprintMoveSpeed; } private set { sprintMoveSpeed = value; } }

    [SerializeField]
    private int sprintMoveSpeed = 0;

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

        if (inputActions.SprintInput)
        {
            rb.MovePosition(rb.position + SprintMoveSpeed * Time.deltaTime * moveDirection);
        }
        else if(rb.velocity.magnitude <= 10 )
        {
            rb.MovePosition(rb.position + MoveSpeed * Time.deltaTime * moveDirection);
        }
    }
}
