using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D = null;

    private Animator myAnimator = null;

    [Header("Movement")]
    [SerializeField]
    private float speed = 3f;
    private float horizontalInput;

    [Header("Jump")]
    [SerializeField]
    private float jumpForce = 300f;
    private bool jump = false;
    [SerializeField]
    private Transform[] GroundCheckPoints;
    [SerializeField]
    private LayerMask groundLayerMask;

    private bool canMove = true;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    public void EnableHorizontalInput(float enableHorizontalInput)
    {
        if (canMove)
        {
            horizontalInput = enableHorizontalInput;

            if (horizontalInput > 0f && transform.right.x < 0f ||
                horizontalInput < 0f && transform.right.x > 0f)
            {
                Flip();
            }
        }
    }

    public void EnableJump()
    {
        if (canMove)
        {
            jump = true;
        }
    }

    public void EnableLocomotion()
    {
        if (!canMove)
        {
            canMove = true;
        }
    }

    public void DisableLocomotion()
    {
        if (canMove)
        {
            canMove = false;
            myRigidbody2D.velocity = new Vector2(0f, 0f);
        }
    }

    private void Flip()
    {
        transform.right = -transform.right;
    }

    private bool IsGrounded()
    {
        for (int i = 0; i < GroundCheckPoints.Length; i++)
        {
            if (Physics2D.OverlapPoint(GroundCheckPoints[i].position, groundLayerMask) != null)
            {
                return true;
            }
        }
        return false;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            bool isGrounded = IsGrounded();

            if (jump && isGrounded && -0.01 < myRigidbody2D.velocity.y && 0.01 > myRigidbody2D.velocity.y)
            {
                myRigidbody2D.velocity = Vector2.zero;
                myRigidbody2D.AddForce(Vector2.up * jumpForce);
            }
            jump = false;

            myRigidbody2D.velocity =
                new Vector2(horizontalInput * speed, myRigidbody2D.velocity.y);

            myAnimator.SetBool("IsGrounded", isGrounded);
            myAnimator.SetFloat("HorizontalSpeed", Mathf.Abs(myRigidbody2D.velocity.x));
            myAnimator.SetFloat("VerticalSpeed", myRigidbody2D.velocity.y);
        }

        if (!canMove)
        {
            myAnimator.SetTrigger("Camouflage");
        }
    }
}
