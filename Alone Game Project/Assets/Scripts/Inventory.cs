using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
 [SerializeField] bool _inventoryEnabled;
 public GameObject inventory;

 private int allSlots;
 private int enabledSlots;

 private GameObject[] slot;
 
 public GameObject slotHolder;

 public void Start()
 {
     allSlots = 40;
    slot = new GameObject[allSlots];

    for (int i = 0; i < allSlots; i++)
    {
        slot[i] = slotHolder.transform.GetChild(i).gameObject;
    }
     
 }

 public void Update()
 {
     if(Input.GetKeyDown(KeyCode.I))
     _inventoryEnabled = !_inventoryEnabled;

     if (_inventoryEnabled == true)
     {
         inventory.SetActive(true);
     }
     else{
         inventory.SetActive(false);
     }
 }
}
