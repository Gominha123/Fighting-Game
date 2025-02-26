using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movecamera : MonoBehaviour
{
    [SerializeField] InputHandler inputActions;
    public bool moveRight = true;
    public bool moveLeft;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (inputActions.interactInput)
        {
            moveRight = !moveRight;
            rb.velocity = Vector3.zero;
            moveLeft = !moveRight;
        }

        if (moveRight)
        {
            rb.AddForce(10 * Vector3.right);
        }

        if (moveLeft)
        {
            rb.AddForce(10 * Vector3.left);
        }
        if (!moveLeft && !moveRight)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
