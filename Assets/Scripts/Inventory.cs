using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private GameObject backSword;

    public bool attack;

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
