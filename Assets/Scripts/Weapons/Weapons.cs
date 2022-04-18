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
    }

    void SwordAttack()
    {
        armAnimator.SetTrigger("Attack1");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (inventory.attack)
        {
            if (collision.GetComponent<Rigidbody2D>())
            {
                collision.GetComponent<Rigidbody2D>().AddForce((creature.MyDirection() + Vector2.up) * force, ForceMode2D.Impulse);
            }
            if (collision.GetComponent<Creature>())
            {
                collision.GetComponent<Creature>().MakeDamage(damage);
            }
            inventory.attack = false;
        }
    }
}
