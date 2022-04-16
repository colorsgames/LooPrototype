using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private GameObject backSword;

    public bool attack;

    private void Start()
    {
        Instance = this;
    }

    public void StartAttack()
    {
        attack = true;
    }

    public void EndAttack()
    {
        attack = false;
    }

    public void TakeSword()
    {
        sword.SetActive(true);
        backSword.SetActive(false);
    }
    public void PutAwaySword()
    {
        sword.SetActive(false);
        backSword.SetActive(true);
    }
}
