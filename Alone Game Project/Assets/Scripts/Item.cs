using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public int Id;
    public string type;
    public string description;

    public Sprite icon;

    public bool pickedUp;

    [HideInInspector]
    public bool equipped;

    [HideInInspector]
    public GameObject weapon;
     [HideInInspector]
    public GameObject weaponManager;
    public bool playersWeapon;

    public void Start () {

        weaponManager = GameObject.FindWithTag("WeaponManager");
        
        if (!playersWeapon)
        {
            int allWeapons = weaponManager.transform.childCount;
            for (int i = 0; i < allWeapons; i++)
            {
                if (weaponManager.transform.GetChild(i).gameObject.GetComponent<Item>().Id == Id)
                {
                    weapon = transform.GetChild(i).gameObject;
                }
            }
        }
    }

    public void Update () {
        if (equipped) {
            // perform weapon acts here
        }
    }

    public void ItemUsage () {
        //Weapoon

        if (type == "Weapon") {
            weapon.SetActive(true);
            equipped = true;
        }

        //health item

        //beverage
    }
}