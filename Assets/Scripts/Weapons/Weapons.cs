using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [Header("General")]
    private Player player;
    [SerializeField]
    private Animator armAnimator;
    [SerializeField]
    private float force;
    [SerializeField]
    private bool isCloseWeapon;
    [SerializeField]
    private float attackDelay;
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

    public bool attack;

    void Start()
    {
        inventory = transform.GetComponentInParent<Inventory>();
        player = FindObjectOfType<Player>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (inventory.attack)
        {
            if (collision.GetComponent<Rigidbody2D>())
            {
                collision.GetComponent<Rigidbody2D>().AddForce(player.MyDirection() * force, ForceMode2D.Impulse);
            }
            inventory.attack = false;
        }
    }
}
