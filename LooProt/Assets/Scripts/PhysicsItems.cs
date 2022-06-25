using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsItems : MonoBehaviour
{
    [SerializeField]
    private float maxSpeedDamage;

    Rigidbody2D rb;

    float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        speed = rb.velocity.magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Creature>())
        {
            if (speed > maxSpeedDamage)
            {
                collision.collider.GetComponent<Creature>().MakeDamage(speed * 10);
            }
        }
    }
}
