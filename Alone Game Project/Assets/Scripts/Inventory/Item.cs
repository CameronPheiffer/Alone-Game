using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public static Item instance;
    public int Id;
    public string type;
    public string description;

    public GameObject _WeaponInHand;

    public Sprite icon;

    public bool pickedUp;

    [HideInInspector]
    public bool equipped;

    // [HideInInspector]
    public GameObject weapon;
    // [HideInInspector]
    public GameObject weaponManager;
    public bool playersWeapon;

    public void FindWeapon () {

        weaponManager = GameObject.FindWithTag ("WeaponManager");

        if (!playersWeapon) {
            int allWeapons = weaponManager.transform.childCount;
            for (int i = 0; i < allWeapons; i++) {
                Debug.Log ("this is happening");
                if (weaponManager.transform.GetChild (i).gameObject.GetComponent<Item> ().Id == Id) {
                    weapon = weaponManager.transform.GetChild (i).gameObject;
                    // weapon = transform.gameObject;
                    Debug.Log (weapon);
                }
            }
        }

    }

    public void Update () {
        if (equipped) {
            // perform weapon acts here
            // Debug.Log ("Equipped");
            if (Input.GetKeyDown (KeyCode.O)) {
                equipped = false;
                // _UIManager.instance.deEquipBtn.SetActive (true);
                // StartCoroutine (_UIManager.instance.WeaponDeEquip ());
            }
            if (equipped == false) {
                this.gameObject.SetActive (false);

            }
        }
    }

    public void ItemUsage () {
        //Weapoon

        if (type == "weapon1") {
            FindWeapon ();
            weapon.SetActive (true);
            // Debug.Log(weapon);
            // Debug.Log(weapon.GetComponent<Item>());
            weapon.GetComponent<Item> ().equipped = true;

        }

        //health item

        //beverage
    }
}