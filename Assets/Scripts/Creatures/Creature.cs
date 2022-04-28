using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    public float MaxHealth;
    public bool Alive = true;
    public bool isPlayer;
    [Header("Movement Settings")]
    public float WalkSpeed;
    public float RunSpeed;
    public float MaxAcceleration;
    public float JumpHeight;
    [Header("Animations")]
    public Animator moveAnimator;
    public Animator armAnimator;
    public float changeAnimSpeed;
    [Header("Ground Check")]
    public Transform groundCheck;
    public float radiusCheckCircle;
    public LayerMask checkMask;

    [HideInInspector]
    public bool takeGun;
    [HideInInspector]
    public bool takeSword;

    protected bool running;
    protected bool desiredJump;

    protected Rigidbody2D rb;
    protected Rigidbody2D[] limbs;

    private Rigidbody2D connectedBody;
    private Rigidbody2D previousConnectedBody;

    private Vector2 velocity;
    private Vector2 connectionWorldPosition;
    private Vector2 connectionVelocity;

    private float curretSpeed;
    private float curretSize;
    private float curretHealth;

    protected virtual void Start()
    {
        curretHealth = MaxHealth;
        rb = GetComponent<Rigidbody2D>();
        if (GetComponentInChildren<Rigidbody2D>())
        {
            limbs = GetComponentsInChildren<Rigidbody2D>();
        }
        curretSpeed = WalkSpeed;
        curretSize = transform.localScale.x;
    }

    protected virtual void Update()
    {
        curretSpeed = running ? RunSpeed : WalkSpeed;

    }

    protected void Movement(float hor)
    {
        if (!Alive)
        {
            hor = 0;
        }
        velocity = rb.velocity;

        if (!isGrounded())
        {
            connectionVelocity = Vector2.zero;
            previousConnectedBody = connectedBody;
            connectedBody = null;
        }

        if (connectedBody)
        {
            if (connectedBody.mass > rb.mass)
            {
                UpdateConnectionState();
            }
        }

        float maxSpeedChange = MaxAcceleration * Time.deltaTime;
        Vector2 relativeVelocity = velocity - connectionVelocity;

        float curretX = Vector3.Dot(relativeVelocity, Vector2.right);
        float newX = Mathf.MoveTowards(curretX, DesiredVelocity(hor).x, maxSpeedChange);

        velocity += Vector2.right * (newX - curretX);

        Rotate(hor);

        if (moveAnimator != null && armAnimator != null)
        {
            float curretSpeedAnim = moveAnimator.GetFloat("Speed");
            float maxChangeSpeed = changeAnimSpeed * Time.deltaTime;

            if (hor != 0 && isGrounded())
            {
                moveAnimator.SetFloat("Speed", running ? Mathf.Lerp(curretSpeedAnim, 1f, maxChangeSpeed) : Mathf.Lerp(curretSpeedAnim, 0.5f, maxChangeSpeed));
                armAnimator.SetFloat("Speed", running ? Mathf.Lerp(curretSpeedAnim, 1f, maxChangeSpeed) : Mathf.Lerp(curretSpeedAnim, 0.5f, maxChangeSpeed));
            }
            else if (!isGrounded())
            {
                moveAnimator.SetFloat("Speed", Mathf.Lerp(curretSpeedAnim, 1.5f, maxChangeSpeed));
                armAnimator.SetFloat("Speed", Mathf.Lerp(curretSpeedAnim, 1.5f, maxChangeSpeed));
            }
            else
            {
                if (curretSpeedAnim >= 0.01f)
                {
                    moveAnimator.SetFloat("Speed", Mathf.Lerp(curretSpeedAnim, 0f, maxChangeSpeed));
                    armAnimator.SetFloat("Speed", Mathf.Lerp(curretSpeedAnim, 0f, maxChangeSpeed));
                }
            }
        }

        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }

        rb.velocity = velocity;
    }

    public void Rotate(float rot)
    {
        if (rot > 0)
        {
            transform.localScale = new Vector3(curretSize, curretSize, curretSize);
        }
        else if (rot < 0)
        {
            transform.localScale = new Vector3(-curretSize, curretSize, curretSize);
        }
    }

    public void MakeDamage(float damage)
    {
        curretHealth -= damage;
        if (curretHealth <= 0)
        {
            if (Alive)
            {
                Dead();
            }
            Alive = false;
        }
    }

    public Vector2 MyDirection()
    {
        if (transform.localScale.x > 0)
        {
            return Vector2.right;
        }
        else if (transform.localScale.x < 0)
        {
            return Vector2.left;
        }
        else
        {
            return Vector2.zero;
        }
    }

    private Vector2 DesiredVelocity(float hor)
    {
        return transform.right * curretSpeed * hor;
    }

    protected virtual void Dead()
    {
        moveAnimator.enabled = false;
        armAnimator.enabled = false;

        foreach (Rigidbody2D item in limbs)
        {
            if (item.isKinematic)
            {
                item.isKinematic = false;
            }
        }
    }

    protected bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, radiusCheckCircle, checkMask);
    }

    private void UpdateConnectionState()
    {
        Vector2 connectionMovement = connectedBody.position - connectionWorldPosition;
        connectionVelocity = connectionMovement / Time.deltaTime;

        connectionWorldPosition = connectedBody.position;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            float jumpSpeed = Mathf.Sqrt(-2 * Physics.gravity.y * JumpHeight);
            velocity.y += jumpSpeed;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isGrounded())
        {
            if (collision.rigidbody)
            {
                connectedBody = collision.rigidbody;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radiusCheckCircle);
    }
}
