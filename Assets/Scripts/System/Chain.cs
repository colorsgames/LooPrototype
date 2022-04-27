using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;

    HingeJoint2D joint;

    private void Start()
    {
        joint = GetComponent<HingeJoint2D>();
    }

    public void Destroy()
    {
        joint.enabled = false;
        Destroy(gameObject, lifeTime);
    }
}
