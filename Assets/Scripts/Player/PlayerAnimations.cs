using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private InputHandler inputActions;
    [SerializeField] private Rigidbody rb;
    int isMovingHash;

    int horInputHash;
    int vertInputHash;

    // Start is called before the first frame update
    void Start()
    {
        isMovingHash = Animator.StringToHash("isMoving");
        horInputHash = Animator.StringToHash("horInput");
        vertInputHash = Animator.StringToHash("vertInput");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat(horInputHash, inputActions.movementInput.x);
        anim.SetFloat(vertInputHash, inputActions.movementInput.y);

        if (inputActions.movementInput == Vector2.zero)
        {
            anim.SetBool(isMovingHash, false);
        }
        else
        {
            anim.SetBool(isMovingHash, true);
        }
    }
}
