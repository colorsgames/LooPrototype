using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [Header("General")]
    private Creature creature;
    [SerializeField]
    private Animator armAnimator;
    [SerializeField]
    private float force;
    [SerializeField]
    private bool isCloseWeapon;
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private float damage;
    [Header("Gunshot")]
    [SerializeField]
    private LayerMask attackMask;
    [SerializeField]
    private Transform rayTarget;
    [SerializeField]
    private float distane;
    [Header("CloseWeapon")]
    private Inventory inventory;

    float curretTime;

    [HideInInspector]
    public bool attack;

    void Start()
    {
        inventory = transform.GetComponentInParent<Inventory>();
        creature = GetComponentInParent<Creature>();
        curretTime = attackDelay;
    }

    private void Update()
    {
        curretTime += Time.deltaTime;
        if (curretTime > attackDelay)
        {
            if (attack)
            {
                if (isCloseWeapon)
                {
                    SwordAttack();
                }
                curretTime = 0;
            }
        }

        if (isCloseWeapon && inventory.attack)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayTarget.position, rayTarget.right, distane, attackMask);
            if (hit)
            {
                if (hit.collider.GetComponent<Rigidbody2D>())
                {
                    hit.collider.GetComponent<Rigidbody2D>().AddForce((creature.MyDirection() + Vector2.up) * force, ForceMode2D.Impulse);
                }
                if (hit.collider.GetComponent<Creature>())
                {
                    hit.collider.GetComponent<Creature>().MakeDamage(damage);
                }
            }
            inventory.attack = false;
        }
    }

    void SwordAttack()
    {
        armAnimator.SetTrigger("Attack1");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(rayTarget.position, rayTarget.up * distane);
    }
}
