using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Creature creature;
    [SerializeField]
    private Weapons[] weapons;
    [SerializeField]
    private GameObject[] items;
    [SerializeField]
    private Animator armAnimator;
    [SerializeField]
    private float dropForce;
    [Header("For SwordsMan")]
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private GameObject backSword;
    [SerializeField]
    private GameObject shield;

    private Items item;

    private int itemId = -1;


    public bool attack;

    private void Awake()
    {
        creature = GetComponentInParent<Creature>();

        if (!creature.isPlayer && creature.GetComponent<Shooter>())
        {
            itemId = Random.Range(0, weapons.Length);
            armAnimator.SetFloat("HaveWeapon", 1);
        }
    }

    private void Start()
    {
        if (sword)
        {
            sword.SetActive(false);
            shield.SetActive(false);
        }
    }

    private void Update()
    {
        if (creature.isPlayer && creature.Alive)
        {
            if (Input.GetButtonDown("Use") && !creature.takeSword)
            {
                if (item && item.ammo > 0)
                {
                    TakeGun();
                }
            }
            if (Input.GetButtonDown("Drop") && creature.takeGun)
            {
                DropGun();
            }
        }
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == itemId)
            {
                weapons[i].gameObject.SetActive(true);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetItem(Items _item)
    {
        item = _item;
    }

    public Weapons GetWeapon()
    {
        return weapons[itemId];
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
        creature.takeSword = true;
        sword.SetActive(true);
        backSword.SetActive(false);
    }
    public void PutAwaySword()
    {
        creature.takeSword = false;
        sword.SetActive(false);
        backSword.SetActive(true);
    }

    public void EnableShield(bool state)
    {
        if (shield)
        {
            shield.SetActive(state);
        }
    }

    void TakeGun()
    {
        // work only with the player
        itemId = item.ID;
        weapons[itemId].curretAmmo = item.ammo;
        item.Use();
        creature.takeGun = true;
        armAnimator.SetFloat("HaveWeapon", 1);
    }

    public void DropGun()
    {
        // work only with the player
        Rigidbody2D rb = Instantiate<Rigidbody2D>(items[itemId].GetComponent<Rigidbody2D>(), transform.position, Quaternion.identity);
        item = rb.GetComponent<Items>();
        item.ammo = weapons[itemId].curretAmmo;
        rb.AddForce((creature.MyDirection() + Vector2.up) * dropForce, ForceMode2D.Impulse);
        Vector3 scale = rb.transform.localScale;
        rb.transform.localScale = new Vector3(scale.x * creature.MyDirection().x, scale.y, scale.z);
        itemId = -1;
        armAnimator.SetFloat("HaveWeapon", 0);
        creature.takeGun = false;
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!creature.isPlayer) return;
        if (!takeSword && !takeGun)
        {
            if (collision.GetComponent<Items>())
            {
                item = collision.GetComponent<Items>();
                if (item.ammo > 0)
                {
                    item.ShowIndicator(true);
                }
            }
        }
        else
        {
            if (item)
            {
                item.ShowIndicator(false);
                item = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!creature.isPlayer) return;
        if (collision.GetComponent<Items>())
        {
            if (item)
            {
                item.ShowIndicator(false);
                item = null;
            }
        }
    }
    */
}
