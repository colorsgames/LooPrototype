using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    private Inventory inventory;

    protected override void Start()
    {
        base.Start();
        inventory = GetComponentInChildren<Inventory>();
        weapon = inventory.GetWeapon();
    }

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

    protected override void Dead()
    {
        base.Dead();
        inventory.DropGun();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(attackVisPoint.position, MyDirection() * rayAttackDistance);
    }
}
