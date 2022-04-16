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
    [SerializeField]
    private LayerMask attackMask;
    [SerializeField]
    private Transform rayTarget;
    [SerializeField]
    private float distane;

    float curretTime;

    void Start()
    {
        player = FindObjectOfType<Player>();
        curretTime = attackDelay;
    }

    private void Update()
    {
        print(player.MyDirection());
        curretTime += Time.deltaTime;
        if (curretTime > attackDelay)
        {
            if (Input.GetButton("Fire1"))
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
        if (Inventory.Instance.attack)
        {
            if (collision.GetComponent<Rigidbody2D>())
            {
                collision.GetComponent<Rigidbody2D>().AddForce(player.MyDirection() * force, ForceMode2D.Impulse);
            }
        }
        Inventory.Instance.attack = false;
    }
}
