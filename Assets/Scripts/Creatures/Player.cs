using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    [Header("Player")]
    [SerializeField]
    private Weapons[] weapons;
    [HideInInspector]
    public Inventory inventory;

    private float horInput;

    bool takeBackWeapon;


    protected override void Start()
    {
        inventory = GetComponentInChildren<Inventory>();
        base.Start();
    }

    protected override void Update()
    {
        if (!Alive) return;
        horInput = Input.GetAxis("Horizontal");
        running = Input.GetButton("Run");
        desiredJump |= Input.GetButtonDown("Jump");

        if (Input.GetButtonDown("TakeBackWeapon") && !takeGun)
        {
            if (!takeBackWeapon)
            {
                armAnimator.SetTrigger("Take");
                takeBackWeapon = true;
            }
            else
            {
                armAnimator.SetTrigger("PutAway");
                takeBackWeapon = false;
            }
        }

        foreach (Weapons item in weapons)
        {
            if (item.gameObject.activeInHierarchy)
            {
                item.attack = Input.GetButton("Fire1");
            }
        }

        base.Update();
    }
    private void FixedUpdate()
    {
        Movement(horInput);
    }
}
