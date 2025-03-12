using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Idle, Walk, Run
}

public class Move : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private RotateModel modelRotator;

    [SerializeField]
    private Animator playerAnim;

    private Vector2 direction;

    private void Start()
    {
        moveSpeed = 0.06f;
    }

    private void FixedUpdate()
    {
        Vector3 camForward = Camera.main.transform.forward;
        
        camForward.y = 0;

        Vector3 camRight = Camera.main.transform.right;

        camRight.y = 0;

        if (direction != Vector2.zero)
        {
            Vector3 moveDir = (camRight * direction.x + camForward * direction.y).normalized;

            Vector3 move = moveSpeed * new Vector3(moveDir.x, 0, moveDir.z);

            rb.MovePosition(transform.position + move);

            modelRotator.SetTargetDirection(move);

            playerAnim.SetBool("IsWalking", true);
        }
        else
        {
            playerAnim.SetBool("IsWalking", false);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            direction = ctx.ReadValue<Vector2>();
        } 
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            direction = Vector2.zero;

            rb.velocity = Vector3.zero;
        }
    }

    public void OnRun(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            moveSpeed = 0.12f;
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            moveSpeed = 0.06f;
        }
    }
}
