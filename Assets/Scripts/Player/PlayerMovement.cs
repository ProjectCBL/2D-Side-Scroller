using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb;
    public GameObject groundCheck;
    public GameObject ceilingCheck;
    public LayerMask whatIsCeiling;
    public LayerMask whatIsGrounded;
    public bool playerFacingRight = true;
    [Range(0,10)] public float radiusCheck;
    [Range(0,80)] public float speed = 40f;
    [Range(0, 400)] public float jumpForce = 40f;
    [Range(0,0.5f)] public float movementSmoothing = 0.5f;
    [Range(0, 10.0f)] public float lowJumpMultiplier = 2.0f;
    [Range(0, 10.0f)] public float gravityMultiplier = 2.5f;

    private float inputValueX = 0;
    private float jumpCooldown = 0.0f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private bool pressedJump = false;
    [Range(0, 3.0f)] [SerializeField] private float jumpCooldownLimit = 1.0f;

    void FixedUpdate()
    {
        Jump();
        Move();
    }

    /*===========================Movement Action Functions=========================*/

    private void Move(){

        Vector3 targetVelocity = new Vector2(
            inputValueX * speed * Time.fixedDeltaTime * 10f, 
            rb.velocity.y
        );

        //Approach target velocity over time
        rb.velocity = Vector3.SmoothDamp(
            rb.velocity,                    // Original Velocity
            targetVelocity,                 // Target Velocity to Reach
            ref velocity,                   // Velocity that gets modified each call
            movementSmoothing               // Steps to take per frame to get to target
        );

        // Sprite Changes
        if (inputValueX != 0) FlipSprite();
        ChangeToRunAnimation();

    }

    private void Jump(){

        // Initial Jump Action -------------------------------------------------------------

        Vector3 groundPoint = groundCheck.transform.position;
        bool isGrounded = Physics2D.OverlapCircle(groundPoint, radiusCheck, whatIsGrounded);

        if(isGrounded && pressedJump && jumpCooldown == jumpCooldownLimit){
            rb.velocity = Vector2.up * jumpForce;
            jumpCooldown = 0.0f;
        }

        // Altitude control --------------------------------------------------------

        // Falling 
        if(rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (gravityMultiplier - 1) * Time.deltaTime;
        }
        // Quick Release Jump
        else if(rb.velocity.y > 0 && !pressedJump){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Jump Timer --------------------------------------------------------

        if(isGrounded){
            jumpCooldown += Time.deltaTime;
            jumpCooldown = Mathf.Clamp(jumpCooldown, 0, jumpCooldownLimit);
        }

        // Animation Change ---------------------------------------------------

        ChangeToJumpAnimation(isGrounded);

    }

    /*=============================Sprite Changes================================*/

    private void FlipSprite(){

        Vector3 newScale = transform.localScale;
        newScale.x = (inputValueX > 0) ? 1 : -1;
        transform.localScale = newScale;

        if (inputValueX > 0) 
            playerFacingRight = true;
        else if (inputValueX < 0) 
            playerFacingRight = false;

    }

    private void ChangeToRunAnimation()
    {
        if (inputValueX != 0) anim.SetBool("Running", true);
        else anim.SetBool("Running", false);
    }

    private void ChangeToJumpAnimation(bool isGrounded)
    {
        if (isGrounded) anim.SetBool("IsGrounded", true);
        else anim.SetBool("IsGrounded", false);
    }

    /*===========================Input Action Events=============================*/

    public void OnMove(InputAction.CallbackContext ctx){
        inputValueX += (ctx.performed) ? ctx.ReadValue<Vector2>().x : -inputValueX;
    }

    public void OnJump(InputAction.CallbackContext ctx){ 
        pressedJump = (ctx.performed) ? true : false;
    }

}
