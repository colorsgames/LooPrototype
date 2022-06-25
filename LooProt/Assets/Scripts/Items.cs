using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField]
    private GameObject indicator;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private int Id;
    [SerializeField]
    private float lifeTime;

    private Player player;

    public int ammo;

    private void Awake()
    {
        indicator.transform.parent = null;
        indicator.SetActive(false);
    }

    private void Start()
    {
        if(ammo > 0)
        {
            Destroy(gameObject, lifeTime);
        }
        else
        {
            Destroy(gameObject, lifeTime/2f);
        }
    }

    public int ID { get { return Id; } }

    private void Update()
    {
        indicator.transform.position = transform.position + offset;
    }

    public void Use()
    {
        Destroy(indicator);
        Destroy(gameObject);
    }

    public void ShowIndicator(bool state)
    {
        if (state)
        {
            indicator.SetActive(true);
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            player = collision.GetComponent<Player>();
            if (!player.takeGun && ammo > 0)
            {
                player.inventory.SetItem(this);
                if (!player.takeGun && !player.takeSword)
                {
                    ShowIndicator(true);
                }
            }
            else
            {
                ShowIndicator(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            ShowIndicator(false);
            player = null;
        }
    }
}
