using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject sword;
    public GameObject backSword;

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
