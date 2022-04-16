using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    private float horInput;

    bool takeBackWeapon;

    protected override void Update()
    {
        horInput = Input.GetAxis("Horizontal");
        running = Input.GetButton("Run");
        desiredJump |= Input.GetButtonDown("Jump");

        if (Input.GetButtonDown("TakeBackWeapon"))
        {
            print("ss");
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

        base.Update();
    }
    private void FixedUpdate()
    {
        Movement(horInput);
    }
}
