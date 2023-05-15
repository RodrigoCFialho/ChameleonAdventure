using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
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

    [Header("Spawn")]
    [SerializeField]
    private Transform checkpoint;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        Spawn();
    }
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0f && transform.right.x < 0f ||
            horizontalInput < 0f && transform.right.x > 0f)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Lower Limit"))
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        transform.position = checkpoint.position;
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

    private void Flip()
    {
        transform.right = -transform.right;
    }
}
