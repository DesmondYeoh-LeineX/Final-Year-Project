using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float movementForce;
    public float airStraffeForce;
    public float maxHorizontalMovementSpeed;
    public float jumpForce;
    public float wallHopForce;
    // public float wallJumpForce;
    [Range(0, 1)] public float jumpHeightVariable;
    [Range(0, 1)] public float airDragVariable;
    [Range(0, 1)] public float groundFrictionAmt;
    public Transform groundCheck;
    public Vector2 groundCheckSize;
    public LayerMask whatIsGround;
    public Transform wallCheck;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float rollMultiplier = 2.0f;
    public Transform ledgeCheck;
    public float ledgeXOffset1 = 0;
    public float ledgeYOffset1 = 0;
    public float ledgeXOffset2 = 0;
    public float ledgeYOffset2 = 0;
    public float dashCooldown;
    public int attackDamage = 10;
    public float attackRadius;
    public Vector2 attackOffset;
    public LayerMask attackMask;

    public Vector2 wallHopDirection;
    // public Vector2 wallJumpDirection;


    private float moveDirection;
    private Vector2 velocity;
    private Rigidbody2D rb;
    private Animator playerAnim;
    private int faceDirection = 1;          // 1 = Facing Right, -1 = Facing Left
    private bool isFacingRight = true;
    private bool canJump = true;
    private bool canMove = true;
    private bool canFlip = true;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isDashing; // or dash
    private bool canDash;
    private bool isTouchingLedge;
    public bool canClimbLedge = false;
    private bool ledgeDetected;
    private bool isAttacking;

    private Vector2 ledgeBotPos;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        wallHopDirection.Normalize();
        canDash = true;
        // wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        CheckWallSliding();
        CheckFaceDirection();
        CheckLedgeClimb();
        Attack();
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y >= 0.01f)
        {
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * jumpHeightVariable);
        }

        if(isGrounded && !isAttacking && !ledgeDetected && !isDashing)
        {
            CombatManager.instance.canReceiveInput = true;
            canJump = true;
            playerAnim.SetBool("WallSlide", false);
            isWallSliding = false;
            
        }

        if (Input.GetKeyDown(KeyCode.Z) && !isDashing && !ledgeDetected && !canClimbLedge && isGrounded)
        {
            if(moveDirection != faceDirection)
            {
                canFlip = true;
            }
            canFlip = false;
            canMove = true;
            StartCoroutine(Dash());
        }

        if(!isGrounded)
        {
            CombatManager.instance.canReceiveInput = false;
        }



        // if(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && isAttacking)
        // {
        //     Debug.Log("running");
        //     isAttacking = false;
        //     canMove = true;
        //     canJump = true;
        //     canFlip = true;
        // }

        playerAnim.SetBool("Grounded", isGrounded);
        playerAnim.SetFloat("Velocity", Mathf.Abs(moveDirection));
        playerAnim.SetFloat("AirSpeedY", rb.velocity.y);
    }

    private void FixedUpdate() 
    {
        CheckSurroundings();
        ApplyMovement();
    }

    private void LateUpdate() 
    {
        
        
    }

    public void ResetAttack()
    {
        if (isAttacking)
        {
            isAttacking = false;
            canMove = true;
            canJump = true;
            canFlip = true;
            CombatManager.instance.canReceiveInput = true;
        }
    }

    private void Attack()
    {
        if(isGrounded && !ledgeDetected && !isDashing && !isWallSliding)
        {
            if(CombatManager.instance.inputReceived && !isAttacking)
            {
                rb.velocity = new Vector3(0,0,0);
                playerAnim.SetTrigger("Attack1");
                canMove = false;
                canJump = false;
                canFlip = false;
                isAttacking = true;
                CombatManager.instance.inputReceived = false;
            }
        }
    }

    private void ApplyMovement()
    {
        if(isGrounded && canMove)
        {
            if(!isDashing)
            {
                Vector2 force = new Vector2(movementForce * moveDirection, 0);

                rb.AddForce(force, ForceMode2D.Impulse);

                if(Mathf.Abs(rb.velocity.x) > maxHorizontalMovementSpeed)
                {
                    rb.velocity = new Vector2 (maxHorizontalMovementSpeed * moveDirection, rb.velocity.y);
                }
                if(moveDirection == 0)
                {
                    velocity = rb.velocity;
                    velocity.x *= groundFrictionAmt;
                    rb.velocity = velocity;
                }
            }

            // rb.velocity = new Vector2(movementForce * moveDirection, rb.velocity.y);
        }
        else if(!isGrounded && !isWallSliding && canMove)
        {
            if(moveDirection != 0)
            {
                Vector2 airForce = new Vector2(airStraffeForce * moveDirection, 0);
                rb.AddForce(airForce);

                if(Mathf.Abs(rb.velocity.x) > maxHorizontalMovementSpeed)
                {
                    rb.velocity = new Vector2(maxHorizontalMovementSpeed * moveDirection, rb.velocity.y);
                }
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x * airDragVariable, rb.velocity.y);
            }
        }
        
        if (isWallSliding)
        {
            canJump = true;
            playerAnim.SetBool("WallSlide", true);
            if(rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(0, -wallSlideSpeed);
            }
        }
    }

    private void CheckSurroundings()
    {   
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, groundCheckSize, CapsuleDirection2D.Vertical, 0.0f, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if(isTouchingWall && !isTouchingLedge && !ledgeDetected && !isGrounded)
        {
            ledgeDetected = true;
            ledgeBotPos = wallCheck.position;
        }
    }

    private void CheckLedgeClimb()
    {
        if(ledgeDetected && !canClimbLedge && !isDashing)
        {
            canClimbLedge = true;

            if(isFacingRight)
            {
                ledgePos1 = new Vector2(
                    Mathf.Floor(ledgeBotPos.x + wallCheckDistance) - ledgeXOffset1, 
                    Mathf.Floor(ledgeBotPos.y) + ledgeYOffset1);
                ledgePos2 = new Vector2(
                    Mathf.Floor(ledgeBotPos.x + wallCheckDistance) + ledgeXOffset2, 
                    Mathf.Floor(ledgeBotPos.y) + ledgeYOffset2);
            }
            else
            {
                ledgePos1 = new Vector2(
                    Mathf.Floor(ledgeBotPos.x + wallCheckDistance) + ledgeXOffset1, 
                    Mathf.Floor(ledgeBotPos.y) + ledgeYOffset1);
                ledgePos2 = new Vector2(
                    Mathf.Floor(ledgeBotPos.x + wallCheckDistance) - ledgeXOffset2, 
                    Mathf.Floor(ledgeBotPos.y) + ledgeYOffset2);
            }
            canMove = false;
            canFlip = false;
            canJump = false;
            playerAnim.SetTrigger("climbLedge");
            Debug.Log(canFlip);
        }

        if(canClimbLedge)
        {
            transform.position = ledgePos1;
        }
    }

    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        canJump = true;
        ledgeDetected = false;

    }

    public void FinishDash()
    {
        // To prevent multiple calls of roll without finishing animation first
        canJump = true;
        isDashing = false;
        canFlip = true;
    }

    private void Jump()
    {
        if(canJump && isGrounded)
        {
            CombatManager.instance.canReceiveInput = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            Vector2 force = new Vector2(0, jumpForce);
            rb.AddForce(force, ForceMode2D.Impulse);
            playerAnim.SetTrigger("Jump");
            canJump = false;
        }
        else if (isWallSliding && moveDirection == faceDirection && canJump) //Hopping up the side of the wall
        {
            CombatManager.instance.canReceiveInput = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            isWallSliding = false;
            Vector2 hopForce = new Vector2(wallHopForce * wallHopDirection.x * -faceDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(hopForce, ForceMode2D.Impulse);
            playerAnim.SetTrigger("Jump");
            canJump = false;
        }
        // else if ((isWallSliding || isTouchingWall) && moveDirection != faceDirection && canJump)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, 0);
        //     isWallSliding = false;
        //     Vector2 jumpForce = new Vector2(wallJumpForce * wallJumpDirection.x * moveDirection, wallJumpForce * wallJumpDirection.y);
        //     rb.AddForce(jumpForce, ForceMode2D.Impulse);
        //     playerAnim.SetTrigger("Jump");
        //     canJump = false;
        // }

    }

    private void CheckFaceDirection()
    {
        if(isFacingRight && moveDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && moveDirection > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        if(!isWallSliding && canFlip)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
            faceDirection *= -1;
        }
    }

    private void CheckWallSliding()
    {
        if(isTouchingWall && rb.velocity.y < 0 && !isGrounded && moveDirection == faceDirection && !canClimbLedge && !isDashing)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
            playerAnim.SetBool("WallSlide", false);
        }
    }

    private IEnumerator Dash()
    {
        if(canDash)
        {
            isDashing = true;
            canDash = false;
            canJump = false;
            playerAnim.SetTrigger("Roll");
            StartCoroutine(PlayerManager.instance.DashIFrame());
            if(moveDirection == 0)
            {
                Debug.Log(moveDirection);

                Vector2 force = new Vector2(movementForce * faceDirection * rollMultiplier, 0);
            
                rb.AddForce(force, ForceMode2D.Impulse);
                
                if(Mathf.Abs(rb.velocity.x) > maxHorizontalMovementSpeed * rollMultiplier)
                {
                    rb.velocity = new Vector2 (maxHorizontalMovementSpeed * faceDirection * rollMultiplier, rb.velocity.y);
                }

            }
            else
            {
                Vector2 force = new Vector2(movementForce * moveDirection * rollMultiplier, 0);
            
                rb.AddForce(force, ForceMode2D.Impulse);

                if(Mathf.Abs(rb.velocity.x) > maxHorizontalMovementSpeed * rollMultiplier)
                {
                    rb.velocity = new Vector2 (maxHorizontalMovementSpeed * moveDirection * rollMultiplier, rb.velocity.y);
                }
            }
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }
    }

    public void CheckHit()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRadius, attackMask);
        if(colInfo != null)
        {
            MobHealth enemyHealth = colInfo.GetComponent<MobHealth>();
            enemyHealth.TakeDamage(attackDamage);
            StartCoroutine(enemyHealth.KnockBack());
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(
            new Vector3(transform.position.x + attackOffset.x, transform.position.y + attackOffset.y, transform.position.z), 
            attackRadius
            );
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
