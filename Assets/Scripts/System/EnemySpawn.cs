using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private int maxCount;

    private void Start()
    {
        StartCoroutine(spawnCoroutine());
    }

    IEnumerator spawnCoroutine()
    {
        int i = 0;
        while (i < maxCount)
        {
            yield return new WaitForSeconds(spawnDelay);
            i++;
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(spawnObject, transform.position, Quaternion.identity);
    }
}
