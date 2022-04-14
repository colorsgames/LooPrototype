using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    public float Health;
    [Header("Movement Settings")]
    public float WalkSpeed;
    public float RunSpeed;
    public float MaxAcceleration;
    public float JumpHeight;
    [Header("Ground Check")]
    public Transform groundCheck;
    public float radiusCheckCircle;
    public LayerMask checkMask;

    protected float curretSpeed;

    protected bool running;
    protected bool desiredJump;

    protected Rigidbody2D rb;

    private Vector2 velocity;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        curretSpeed = WalkSpeed;
    }

    protected virtual void Update()
    {
        curretSpeed = running ? RunSpeed : WalkSpeed;
    }

    private Vector2 DesiredVelocity(float hor)
    {
        return transform.right * curretSpeed * hor;
    }

    protected void Movement(float hor)
    {
        velocity = rb.velocity;

        float maxSpeedChange = MaxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, DesiredVelocity(hor).x, maxSpeedChange);

        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }

        rb.velocity = velocity;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            print("Jump");
            float jumpSpeed = Mathf.Sqrt(-2 * Physics.gravity.y * JumpHeight);
            velocity.y += jumpSpeed;
        }
    }

    protected bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, radiusCheckCircle, checkMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radiusCheckCircle);
    }
}
