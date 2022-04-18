using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [Header("Enemy")]
    protected bool aggression;
    [SerializeField]
    protected Weapons weapon;
    [SerializeField]
    protected Creature targetCreature;

    public enum VisTargetType
    {
        Enter,
        Exit
    }

    protected override void Update()
    {
        if (target && aggression)
        {
            GoTo(target.position);
        }
        if (targetCreature && !targetCreature.Alive)
        {
            TargetVisUpdate(targetCreature, VisTargetType.Exit);
        }
        base.Update();
    }

    protected virtual void Attack() { }

    public virtual void TargetVisUpdate(Creature creature, VisTargetType visType)
    {
        if (visType == VisTargetType.Enter)
        {
            running = true;
            aggression = true;
            target = creature.transform;
            targetCreature = creature;

        }
        else
        {
            running = false;
            startStrayingPos = transform.position;
            aggression = false;
            target = null;
            targetCreature = null;
        }
    }
}
