using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private float spawnDelay;

    private void Start()
    {
        StartCoroutine(spawnCoroutine());
    }

    IEnumerator spawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(spawnObject, transform.position, Quaternion.identity);
    }
}
