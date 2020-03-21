using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject item;
    public bool empty;
    public Sprite _icon;
    public int Id;
    public string type;
    public string description;

    public void UpdateSlot()
    {
        this.GetComponent<Image>().sprite = _icon;
    }
}
