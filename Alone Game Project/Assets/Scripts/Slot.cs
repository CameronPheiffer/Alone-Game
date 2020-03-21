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
    
    public void OnPointerClick(PointerEventData pointerEventData){
        UseItem();
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
        item.GetComponent<Item>().ItemUsage();
    }
}
