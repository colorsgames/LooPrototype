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
    [Header("Animations")]
    public Animator Animator;
    public float changeAnimSpeed;
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

        if(hor > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(hor < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        float curretSpeedAnim = Animator.GetFloat("Speed");
        float maxChangeSpeed = changeAnimSpeed * Time.deltaTime;
        if (hor != 0 && isGrounded())
        {
            Animator.SetFloat("Speed", running ? Mathf.Lerp(curretSpeedAnim, 1f, maxChangeSpeed) : Mathf.Lerp(curretSpeedAnim, 0.5f, maxChangeSpeed));
        }
        else if (!isGrounded())
        {
            Animator.SetFloat("Speed", Mathf.Lerp(curretSpeedAnim, 1.5f, maxChangeSpeed));
        }
        else
        {
            Animator.SetFloat("Speed", Mathf.Lerp(curretSpeedAnim, 0f, maxChangeSpeed));
        }

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
