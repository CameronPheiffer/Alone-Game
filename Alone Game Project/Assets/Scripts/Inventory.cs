using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    //MUST BE SET ACTIVE OR ELSE IT WILL SHOW A ERROR.
    [SerializeField] bool _inventoryEnabled;
    public GameObject inventory;

    private int allSlots;
    private int enabledSlots;

    private GameObject[] slot;

    public GameObject slotHolder;

    public void Start () {
        
        
        allSlots = 40;
        slot = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++) {
            slot[i] = slotHolder.transform.GetChild (i).gameObject;

            if (slot[i].GetComponent<Slot> ().item == null)
                slot[i].GetComponent<Slot> ().empty = true;
        }

    }

    public void Update () {
        if (Input.GetKeyDown (KeyCode.I))
            _inventoryEnabled = !_inventoryEnabled;

        if (_inventoryEnabled == true) {
            inventory.SetActive (true);
        } else {
            inventory.SetActive (false);
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.tag == "item") {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item> ();

            AddItem (itemPickedUp, item.Id, item.type, item.description, item.icon);
        }
    }

    void AddItem (GameObject itemObject, int itemId, string itemtype, string itemDescription, Sprite itemIcon) {
        for (int i = 0; i < allSlots; i++) {
            if (slot[i].GetComponent<Slot> ().empty) {
                //add item to slot
                itemObject.GetComponent<Item> ().pickedUp = true;

                slot[i].GetComponent<Slot> ().item = itemObject;
                slot[i].GetComponent<Slot> ()._icon = itemIcon;
                slot[i].GetComponent<Slot> ().type = itemtype;
                slot[i].GetComponent<Slot> ().Id = itemId;
                slot[i].GetComponent<Slot> ().description = itemDescription;

                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive (false);

                slot[i].GetComponent<Slot> ().UpdateSlot();
                slot[i].GetComponent<Slot> ().empty = false;
            }
            return;
        }
    }
}