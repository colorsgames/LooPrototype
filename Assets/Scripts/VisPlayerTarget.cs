using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisPlayerTarget : MonoBehaviour
{
    [SerializeField]
    private LayerMask obstacles;
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
                Vector3 offset = collision.transform.position - transform.position;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, offset, 100, obstacles);
                if (hit)
                {
                    if (hit.collider.GetComponent<Player>())
                    {
                        enemy.TargetVisUpdate(collision.GetComponent<Player>(), Enemy.VisTargetType.Enter);
                    }
                    else
                    {
                        return;
                    }
                }
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
