using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    //OBJECT UI MUST BE SET ACTIVE OR ELSE IT WILL SHOW A ERROR.
    [SerializeField] bool _inventoryEnabled;
    public GameObject inventory;

    private int allSlots;
    private int enabledSlots;

    private Canvas _Canvas;

    private GameObject[] slot;

    public GameObject slotHolder;

    private void Awake () {
        _Canvas = inventory.GetComponent<Canvas> ();
    }

    public void Start () {

        allSlots = slotHolder.transform.childCount;
        slot = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++) {
            slot[i] = slotHolder.transform.GetChild (i).gameObject;

            if (slot[i].GetComponent<Slot> ().item == null)
                slot[i].GetComponent<Slot> ().empty = true;
        }

    }

    public void Update () {
        // OPEN AND CLOSING OF INVENTORY:
        if (Input.GetKeyDown (KeyCode.I)) {
            _inventoryEnabled = !_inventoryEnabled;
            _Canvas.enabled = _inventoryEnabled;

            Cursor.visible = _inventoryEnabled;
            
            if (_inventoryEnabled)
            {
               Cursor.lockState = CursorLockMode.None; 
            }
            else{
                 Cursor.lockState = CursorLockMode.Locked; 
            }
            

        }

    }

    private void OnTriggerEnter (Collider other) {
        if (other.tag == "item") {
            // PICK UP OBJECTS TAGGED ITEMS
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item> ();

            AddItem (itemPickedUp, item.Id, item.type, item.description, item.icon);
        }
    }

    void AddItem (GameObject itemObject, int itemId, string itemtype, string itemDescription, Sprite itemIcon) {
        for (int i = 0; i < allSlots; i++) {
            if (slot[i].GetComponent<Slot> ().empty) {
                //ADDS ITEM TO SLOT
                itemObject.GetComponent<Item> ().pickedUp = true;

                slot[i].GetComponent<Slot> ().item = itemObject;
                slot[i].GetComponent<Slot> ()._icon = itemIcon;
                slot[i].GetComponent<Slot> ().type = itemtype;
                slot[i].GetComponent<Slot> ().Id = itemId;
                slot[i].GetComponent<Slot> ().description = itemDescription;

                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive (false);

                slot[i].GetComponent<Slot> ().UpdateSlot ();
                slot[i].GetComponent<Slot> ().empty = false;
            }
            return;
        }
    }
}