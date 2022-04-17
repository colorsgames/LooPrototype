using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [Header("Enemy")]
    protected bool aggression;
    [SerializeField]
    protected Weapons weapon;

    protected override void Update()
    {
        if (target && aggression)
        {
            GoTo(target.position);
        }
        base.Update();
    }

    protected virtual void Attack() { }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        if (collision.GetComponent<Player>())
        {
            running = true;
            aggression = true;
            target = collision.GetComponent<Player>().transform;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        print(collision.name);
        if (collision.GetComponent<Player>())
        {
            running = false;
            startStrayingPos = transform.position;
            aggression = false;
            target = null;
        }
    }
}
