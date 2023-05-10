using System.Collections;
using System.Collections.Generic;
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

    [Header("Interact")]
    private InteractableObject interactableObject = null;
    private bool canInteract = false;

    [Header("Camouflage")]
    private SpriteRenderer mySpriteRenderer = null;
    private Color initialColor;
    private bool isBlue = false;
    private float blueWaitTime = 5f;
    private bool isRed = false;
    private float redWaitTime = 5f;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = mySpriteRenderer.color;
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

        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if (interactableObject != null)
            {
                interactableObject.Interact();
            }
        }
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

    public void ColorRed()
    {
        if (isBlue)
        {
            CancelInvoke(nameof(DisableColorBlue));
            isBlue = false;
        }

        if (isRed == false)
        {
            isRed = true;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            CancelInvoke(nameof(DisableColorRed));
        }

        Invoke(nameof(DisableColorRed), redWaitTime);
    }

    private void DisableColorRed()
    {
        isRed = false;
        GetComponent<SpriteRenderer>().color = initialColor;
    }

    public void ColorBlue()
    {
        if (isRed)
        {
            CancelInvoke(nameof(DisableColorRed));
            isRed = false;
        }

        if (isBlue == false)
        {
            isBlue = true;
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            CancelInvoke(nameof(DisableColorBlue));
        }

        Invoke(nameof(DisableColorBlue), blueWaitTime);
    }

    private void DisableColorBlue()
    {
        isBlue = false;
        GetComponent<SpriteRenderer>().color = initialColor;
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
