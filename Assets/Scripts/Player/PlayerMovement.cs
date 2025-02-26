using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputHandler inputActions;

    private float maxMovSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintMoveSpeed;
    [SerializeField] private float jumpForce;
    private float horInput;
    private float vertInput;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    public LayerMask whatIsGround;

    [SerializeField] private Transform orientation;

    public bool isGrounded;

    [SerializeField] private Vector3 showNumber;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //capsuleCollider = GetComponent<CapsuleCollider>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        IsGrounded();
    }

    private void Jump()
    {
        if (inputActions.jumpInput && isGrounded)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }
    }

    private void IsGrounded()
    {
        //isGrounded = Physics.Raycast(capsuleCollider.bounds.center, Vector3.down, 
        //  capsuleCollider.bounds.extents.y + 0.1f, whatIsGround);

        isGrounded = true;
    }

    private void Move()
    {
        horInput = inputActions.movementInput.x;
        vertInput = inputActions.movementInput.y;

        //calculate movement direction
        moveDirection = orientation.forward * vertInput + orientation.right * horInput;

        moveDirection.y = 0;
        moveDirection.Normalize();

        if (inputActions.sprintInput && isGrounded)
        {
            rb.MovePosition(rb.position + sprintMoveSpeed * Time.deltaTime * moveDirection);
        }
        else if (isGrounded)
        {
            rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * moveDirection);
        }
    }
}
