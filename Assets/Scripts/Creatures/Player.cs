using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    [Header("Player")]
    [SerializeField]
    private Weapons sword;

    private float horInput;

    bool takeBackWeapon;


    protected override void Update()
    {
        if (!Alive) return;
        horInput = Input.GetAxis("Horizontal");
        running = Input.GetButton("Run");
        desiredJump |= Input.GetButtonDown("Jump");

        if (Input.GetButtonDown("TakeBackWeapon"))
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

        sword.attack = Input.GetButton("Fire1");

        base.Update();
    }
    private void FixedUpdate()
    {
        Movement(horInput);
    }
}
