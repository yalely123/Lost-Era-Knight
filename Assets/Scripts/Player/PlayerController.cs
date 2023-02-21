using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerHealth = 100;

    public int facingDirection = 1; // 1 is facing to the right and -1 is facing to the left
    public int movementForceInAir;
    public float movementSpeed = 10f;
    private float movementInputDirection;
    public float jumpForce = 20f;
    public float jumpHeightMultiplier = 0.5f;
    public int jumpAmount = 2;
    public int jumpAmountLeft;
    public float wallSlidingSpeed;
    public float wallJumpForce = 15f;

    private bool isWalking;
    private bool isFacingRight = true;
    public bool isGrounded;
    public bool canJump;
    public bool isTouchingWall;
    public bool isWallSliding;

    public float groundCheckRadius;
    public float wallCheckDistance;

    public Vector2 wallJumpDirection;

    private Rigidbody2D rb;
    private Animator anim;
    
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpAmountLeft = jumpAmount;
        wallJumpDirection.Normalize(); // to set range of variable to [-1, 1]
    }

    void Update()
    {
        checkInput();
        checkMovementDirection();
        updateAnimation();
        checkIfCanJump();
        checkIfWallSliding();

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
        
        if (Input.GetKeyUp(KeyCode.Z))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpHeightMultiplier);
        }


    }
    private void ApplyMovement()
    {
        if (isGrounded) // add this condition to fix walljump x axis cant add force
        {
            rb.velocity = new Vector2(movementInputDirection * movementSpeed, rb.velocity.y);
        }

        if (!isGrounded && movementInputDirection != 0)
        {
            Vector2 forceToAdd = new Vector2(movementForceInAir * movementInputDirection, 0);
            rb.AddForce(forceToAdd); // to make when start move in the air. player will move by adding force(increase speed every fram)
            if (Mathf.Abs(rb.velocity.x) >= movementSpeed) // limit speed when adding force to not over movementSpeed
            {
                rb.velocity = new Vector2(movementInputDirection * movementSpeed, rb.velocity.y);
            }
        }

        if (isWallSliding) // apply wall sliding by limiting falling velocity 
        {
            if (rb.velocity.y < -wallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            }
        }
    }

    private void checkIfCanJump()
    {
        if (!isGrounded && jumpAmount == jumpAmountLeft) // this mean falling without jump such as slip of platform
        {
            jumpAmountLeft -= 1;
        }

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

    private void checkIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
            jumpAmountLeft = jumpAmount;
        }
        else
        {
            isWallSliding = false;
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
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }
    private void Jump()
    {
        if (canJump && !isWallSliding) // proper jump (jump from the ground)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpAmountLeft -= 1;
        }else if (isWallSliding) { // wall jumping/hopping
            isWallSliding = false;
            Vector2 calculatedWallJumpForceToAdd = new Vector2(wallJumpDirection.x * wallJumpForce * -facingDirection, 
                                    wallJumpDirection.y * wallJumpForce);
            rb.AddForce(calculatedWallJumpForceToAdd, ForceMode2D.Impulse);
        }

    }



    private void FlipDirection()
    {
        facingDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180, 0f);
    }

    private void updateAnimation()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance,
                        wallCheck.position.y, wallCheck.position.z));
    }

}
