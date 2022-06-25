using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float emissionTime;

    private ParticleSystem partSys;

    float curretTime;

    private void Start()
    {
        partSys = GetComponent<ParticleSystem>();
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        curretTime += Time.deltaTime;
        if(curretTime > emissionTime)
        {
            var emiss = partSys.emission;
            emiss.enabled = false;
        }
    }

    public void SetParent(Transform newParent)
    {
        transform.parent = newParent;
    }
}
