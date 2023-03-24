using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    public bool isHit = false;

    public float knockDuration;
    private bool isKnocking;
    private float knockTriggerTime;
    public float knockFiction;
    public float knockBackForce;
    private bool isKnockBackReachLastFrame; // to make sure that animation reach last Frame
    public float attackXPos;

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
        CheckInput();
        CheckMovementDirection();
        updateAnimation();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckIfIsHit();

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            transform.SetPositionAndRotation(new Vector2(-2, 3), transform.rotation);
            rb.velocity = new Vector2(0f, -1.0f);
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Jump();
        }
        
        if (Input.GetKeyUp(KeyCode.Z) && rb.velocity.y > 0) // velocity.y > 0 mean that not in falling phase
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpHeightMultiplier);
        }

    }
    private void ApplyMovement()
    {
        if (isKnocking)
        {
            if (isGrounded)
            {
                rb.AddForce(new Vector2(rb.velocity.x * -Time.deltaTime * knockFiction, 0), ForceMode2D.Impulse);
            }
            if (Time.time > knockTriggerTime + knockDuration && isKnockBackReachLastFrame && isGrounded)
            {
                isKnocking = false;
            }
        }
        else
        {
            if (isGrounded && !isWallSliding) // add this condition to fix walljump x axis cant add force
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
    }

    private void CheckIfCanJump()
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

    private void CheckIfWallSliding()
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

    private void CheckMovementDirection()
    {
        if (!isFacingRight && movementInputDirection > 0 && !isKnocking)
        {
            FlipDirection();
        }else if (isFacingRight && movementInputDirection < 0 && !isKnocking)
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

    private void CheckIfIsHit()
    {
        if (isHit)
        {
            isKnocking = true;
            // TODO: apply Knocking
            KnockBack();
            isHit = false;
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void KnockBack()
    {
        int attackFrom; // 1 mean get attack from right side and -1 mean get from left side
        if (transform.position.x > attackXPos)
        {
            attackFrom = -1;
        }
        else
        {
            attackFrom = 1;
        }
        Vector2 knockDirection = new Vector2(5 * -attackFrom, 3).normalized;
        isKnockBackReachLastFrame = false;
        knockTriggerTime = Time.time;
        rb.velocity = Vector2.zero;
        rb.AddForce(knockDirection * knockBackForce, ForceMode2D.Impulse);
    }

    public void KnockBackLastFrameCheck()
    {
        isKnockBackReachLastFrame = true;
    }

    private void Jump()
    {
        if (canJump && !isWallSliding && !isKnocking) // proper jump (jump from the ground)
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
        anim.SetBool("isHit", isKnocking);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance,
                        wallCheck.position.y, wallCheck.position.z));
    }

}
