using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limbs : MonoBehaviour
{
    private HingeJoint2D joint;

    private void Start()
    {
        joint = GetComponentInParent<HingeJoint2D>();
    }

    public void Destroy()
    {
        if (joint)
        {
            joint.enabled = false;
        }
    }
}
