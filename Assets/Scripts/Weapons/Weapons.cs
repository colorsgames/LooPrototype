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
    [SerializeField]
    private LayerMask attackMask;
    [SerializeField]
    private Transform rayTarget;
    [SerializeField]
    private float distane;
    [Header("Gun shot")]
    [SerializeField]
    private float fxLifeTime;
    [SerializeField]
    private ShotTrail shotTrail;
    [SerializeField]
    private int maxAmmo;
    [SerializeField]
    private float rechargeTime;
    [Header("CloseWeapon")]
    [SerializeField]
    private TrailRenderer swordTrail;

    private ParticleSystem shotFX;
    private Inventory inventory;

    [HideInInspector]
    public bool attack;
    [HideInInspector]
    public int curretAmmo;

    bool recharge;

    float curretTime;
    float curretFXTime;
    float curretRechTime;

    RaycastHit2D oldHit;

    void Start()
    {
        inventory = transform.GetComponentInParent<Inventory>();
        creature = GetComponentInParent<Creature>();
        curretTime = attackDelay;
        if (!isCloseWeapon)
        {
            shotFX = GetComponentInChildren<ParticleSystem>();
            SetGunEffects(false);
        }
    }

    private void Update()
    {
        if (creature.Alive)
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
                    else
                    {
                        if (curretAmmo > 0)
                        {
                            Shot();
                        }
                        else
                        {
                            if (creature.isPlayer)
                            {
                                inventory.DropGun();
                            }
                            else
                            {
                                recharge = true;
                            }
                        }
                    }
                    curretTime = 0;
                }
            }
            if (isCloseWeapon)
            {
                SwordEffects();
            }
            if (recharge)
            {
                curretRechTime += Time.deltaTime;
                if(curretRechTime > rechargeTime)
                {
                    curretAmmo = maxAmmo;
                    curretRechTime = 0;
                    recharge = false;
                }
            }
        }

        if (shotFX)
        {
            var emiss = shotFX.emission;
            if (emiss.enabled)
            {
                curretFXTime += Time.deltaTime;
                if(curretFXTime > fxLifeTime)
                {
                    SetGunEffects(false);
                    curretFXTime = 0;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isCloseWeapon)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayTarget.position, rayTarget.up, distane, attackMask);
            if (hit)
            {
                if (inventory.attack)
                {
                    if (oldHit)
                    {
                        if (oldHit.collider.name != hit.collider.name)
                        {
                            Attack(hit);
                        }
                    }
                    else
                    {
                        Attack(hit);
                    }
                    //inventory.attack = false;
                }
                else
                {
                    oldHit = new RaycastHit2D();
                }
            }
        }
    }


    void Shot()
    {
        curretAmmo--;
        SetGunEffects(true);
        ShotTrail trail = Instantiate<ShotTrail>(shotTrail, rayTarget.position, Quaternion.identity);
        RaycastHit2D hit = Physics2D.Raycast(rayTarget.position, rayTarget.right * creature.MyDirection(), distane, attackMask);
        if (hit)
        {
            trail.SetTarget(hit.point);
            Attack(hit);
        }
        else
        {
            trail.SetTarget(creature.MyDirection() * 100);
        }
    }

    void Attack(RaycastHit2D hit)
    {
        oldHit = hit;
        if (hit.collider.GetComponent<Rigidbody2D>())
        {
            hit.collider.GetComponent<Rigidbody2D>().AddForce((creature.MyDirection() + Vector2.up) * force, ForceMode2D.Impulse);
        }
        if (hit.collider.GetComponent<Creature>())
        {
            hit.collider.GetComponent<Creature>().MakeDamage(damage);
        }
    }

    void SwordAttack()
    {
        armAnimator.SetTrigger("Attack1");
    }

    void SwordEffects()
    {
        if (isCloseWeapon && inventory.attack)
        {
            if (swordTrail)
            {
                swordTrail.enabled = true;
            }
        }
        else
        {
            if (swordTrail)
            {
                swordTrail.enabled = false;
            }
        }
    }

    void SetGunEffects(bool state)
    {
        var emiss = shotFX.emission;
        emiss.enabled = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(rayTarget.position, rayTarget.right * distane);
    }
}
