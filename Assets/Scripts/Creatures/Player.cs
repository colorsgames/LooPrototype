using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    private float horInput;

    protected override void Update()
    {
        horInput = Input.GetAxis("Horizontal");
        running = Input.GetButton("Run");
        desiredJump |= Input.GetButtonDown("Jump");
        base.Update();
    }

    private void FixedUpdate()
    {
        Movement(horInput);
    }
}
