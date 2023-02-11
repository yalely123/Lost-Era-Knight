using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerHealth = 100;

    public float movementSpeed = 5f;
    private float movementInputDirection;
    public float jumpForce = 12f;
    public int jumpAmount = 2;
    public int jumpAmountLeft;

    private bool isWalking;
    private bool isFacingRight = true;
    public bool isGrounded;
    public bool canJump;

    public float groundCheckRadius;

    private Rigidbody2D rb;
    private Animator anim;
    
    public Transform groundCheck;
    public LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpAmountLeft = jumpAmount;
    }

    void Update()
    {
        checkInput();
        checkMovementDirection();
        updateAnimation();
        checkIfCanJump();

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            transform.SetPositionAndRotation(new Vector2(-2, 3), transform.rotation);
            rb.velocity = new Vector2(0f, -1.0f);
        }
         
    }

    private void FixedUpdate()
    {
        
        ApplyMovement();
        checkSurroundings();
    }

    private void checkInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Jump();
        }

    }

    private void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpAmountLeft -= 1;
            Debug.Log("in Jump Function " + jumpAmountLeft);
        }
        
    }

    private void ApplyMovement()
    {
        rb.velocity = new Vector2(movementInputDirection * movementSpeed, rb.velocity.y);
    }
    private void checkIfCanJump()
    {
        //Debug.Log("in checkIfCanJump: " + jumpAmountLeft);
        if (jumpAmountLeft <= 0)
        {
            canJump = false;
        }else if (jumpAmountLeft > 0)
        {
            canJump = true;
        }

        
        if (isGrounded && rb.velocity.y <= 0)
        {
            jumpAmountLeft = jumpAmount;
        }
        
    }

    private void checkMovementDirection()
    {
        if (!isFacingRight && movementInputDirection > 0)
        {
            FlipDirection();
        }else if (isFacingRight && movementInputDirection < 0)
        {
            FlipDirection();
        }
        /*
        Debug.Log(rb.velocity.x);
        if (rb.velocity.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        */

        if (movementInputDirection != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void checkSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void FlipDirection()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180, 0f);
    }

    private void updateAnimation()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}
