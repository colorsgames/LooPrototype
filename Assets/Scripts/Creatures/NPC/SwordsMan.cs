using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsMan : Enemy
{
    [Header("SwordsMan")]
    [SerializeField]
    private LayerMask attackMask;
    [SerializeField]
    private float rayAttackDistance;
    [SerializeField]
    private Transform attackVisPoint;

    protected override void Update()
    {
        Attack();
        base.Update();
    }

    protected override void Attack()
    {
        if (!Alive) { weapon.attack = false; return; }
        RaycastHit2D hit = Physics2D.Raycast(attackVisPoint.position, MyDirection(), rayAttackDistance, attackMask);
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
        else
        {
            weapon.attack = false;
        }
    }

    public override void TargetVisUpdate(Creature creature, VisTargetType visType)
    {
        if (!Alive) return;
        if (visType == VisTargetType.Enter)
        {
            armAnimator.SetTrigger("Take");
        }
        else
        {
            armAnimator.SetTrigger("PutAway");
        }
        base.TargetVisUpdate(creature, visType);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(attackVisPoint.position, MyDirection() * rayAttackDistance);
    }
}
