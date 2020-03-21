using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject item;

    public Transform slotIconGO;
    public bool empty;
    public Sprite _icon;
    public int Id;
    public string type;
    public string description;

    private void Start()
    {
        slotIconGO = transform.GetChild(0);
    }

    public void UpdateSlot()
    {
        slotIconGO.GetComponent<Image>().sprite = _icon;
    }
}
