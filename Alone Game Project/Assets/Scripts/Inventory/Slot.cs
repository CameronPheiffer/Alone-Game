using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour , IPointerClickHandler
{
    public GameObject item;

    public Transform slotIconGO;
    public bool empty;
    public Sprite _icon;
    public int Id;
    public string type;
    public string description;

    public GameObject itemEquippedPopup;
    
    public void OnPointerClick(PointerEventData pointerEventData){
        UseItem();
        Debug.Log("UseItem");
    }

    private void Start()
    {
        slotIconGO = transform.GetChild(0);
    }

    public void UpdateSlot()
    {
        slotIconGO.GetComponent<Image>().sprite = _icon;
    }

    public void UseItem()
    {   
        itemEquippedPopup.SetActive(true);
        item.GetComponent<Item>().ItemUsage();
        Debug.Log("item" + item);
    // Debug.Log("useitem" + (item.GetComponent<Item>().ItemUsage());
    StartCoroutine (WeaponEquipped ());
    }

     IEnumerator WeaponEquipped () {
        yield return new WaitForSeconds (2);
        itemEquippedPopup.SetActive (false);
        yield return null;
    }
}
