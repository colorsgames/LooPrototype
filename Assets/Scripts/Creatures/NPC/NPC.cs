using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Creature
{
    [Header("NPC")]
    protected Transform target;
    [Header("ObstaclesVis")]
    [SerializeField]
    private Transform obstaclesRayPoint;
    [SerializeField]
    private LayerMask obstaclesMask;
    [SerializeField]
    private float distance;
    [Header("WalkSettings")]
    [SerializeField]
    private float jumpDelay;
    [SerializeField]
    private float stopRadius;
    [SerializeField]
    private float strayingRandomRadius;

    protected bool came;

    float curretTime;

    protected Vector2 startStrayingPos;

    Vector2 randomTarget;

    protected override void Start()
    {
        startStrayingPos = transform.position;
        base.Start();
    }

    protected override void Update()
    {
        if (!target)
        {
            Straying();
        }
        base.Update();
    }

    public void Straying()
    {
        if (came)
        {
            randomTarget = new Vector2(startStrayingPos.x + Random.Range(-strayingRandomRadius, strayingRandomRadius), 0);
        }
        GoTo(randomTarget);

    }

    public void GoTo(Vector3 target)
    {
        came = false;

        Vector2 offset = target - transform.position;

        if (offset.x < -stopRadius)
        {
            Movement(-1f);
        }
        else if (offset.x > stopRadius)
        {
            Movement(1f);
        }
        else
        {
            came = true;
            Movement(0f);
            return;
        }

        curretTime += Time.deltaTime;
        if (curretTime > jumpDelay)
        {
            RaycastHit2D hit = Physics2D.Raycast(obstaclesRayPoint.position, MyDirection(), distance, obstaclesMask);
            if (hit)
                desiredJump = true;
            curretTime = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(obstaclesRayPoint.position, MyDirection() * distance);
    }
}
