using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsMan : Enemy
{
    [Header("SwordsMan")]
    [SerializeField]
    private LayerMask attackMask;
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private Transform attackVisPoint;

    protected override void Update()
    {
        Attack();
        base.Update();
    }

    protected override void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(attackVisPoint.position, MyDirection(), attackDistance, attackMask);
        if (hit)
        {
            if (hit.collider.GetComponent<Player>())
            {
                weapon.attack = true;
            }
            else
            {
                weapon.attack = false;
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        armAnimator.SetTrigger("Take");
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        armAnimator.SetTrigger("PutAway");
        base.OnTriggerExit2D(collision);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(attackVisPoint.position, MyDirection() * attackDistance);
    }
}
