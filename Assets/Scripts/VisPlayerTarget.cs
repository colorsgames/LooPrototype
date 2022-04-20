using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisPlayerTarget : MonoBehaviour
{
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (collision.GetComponent<Player>().Alive)
            {
                enemy.TargetVisUpdate(collision.GetComponent<Player>(), Enemy.VisTargetType.Enter);
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            enemy.TargetVisUpdate(collision.GetComponent<Player>(), Enemy.VisTargetType.Exit);
        }
    }
}
