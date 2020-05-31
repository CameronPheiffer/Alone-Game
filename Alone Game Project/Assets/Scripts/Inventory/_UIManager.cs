using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _UIManager : MonoBehaviour {

    //OBJECT UI MUST BE SET ACTIVE OR ELSE IT WILL SHOW A ERROR.

    public static _UIManager instance;
    [SerializeField] bool _inventoryEnabled;

    public bool _inInventory = false;
    public GameObject inventory;
    public GameObject popUp1;
    public GameObject popUp2;

    private int allSlots;
    private int enabledSlots;

    private Canvas _Canvas;

    private GameObject[] slot;

    public GameObject slotHolder;

    // UI MANAGER _____________________________

    //   public static UIManager instance;
    [SerializeField] bool PauseEnabled;

    [SerializeField] Canvas PauseCanvas;
    [SerializeField] GameObject PauseCanvasObject;
    public GameObject InGameHudCanvas;
    public Transform Player;
    public Camera ThirdPersonCam, SidePersonCamera;
    public KeyCode TKey;
    public bool camSwitch = false;

    //END_______________________________________

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

            if (_inventoryEnabled) {
                InGameHudCanvas.SetActive (false);  
                _inInventory = true;
                Cursor.lockState = CursorLockMode.None;
                PlayerControllerCameron.instance.basicAttack = false;
                CamController.instance.CameraInInventory = false;
                popUp1.SetActive (false);
                popUp2.SetActive (true);
                //    Destroy(popUp1);

            } else {
                InGameHudCanvas.SetActive (true);
                _inInventory = false;
                Cursor.lockState = CursorLockMode.Locked;
                PlayerControllerCameron.instance.basicAttack = true;
                CamController.instance.CameraInInventory = true;
                popUp2.SetActive (false);

            }

        }

        if (Input.GetKeyDown (KeyCode.Escape)) {
            PauseCanvasObject.SetActive (true);
            InGameHudCanvas.SetActive (false);
            PauseEnabled = !PauseEnabled;
            PauseCanvas.enabled = PauseEnabled;

            Cursor.visible = _inventoryEnabled;
         Cursor.visible = PauseEnabled;

            if (PauseEnabled) {
                Cursor.lockState = CursorLockMode.None;
                PlayerControllerCameron.instance.basicAttack = false;
                CamController.instance.CameraInInventory = false;
                camSwitch = !camSwitch;
                SidePersonCamera.gameObject.SetActive (camSwitch);
                ThirdPersonCam.gameObject.SetActive (!camSwitch);

            } else {
                camSwitch = !camSwitch;
                SidePersonCamera.gameObject.SetActive (camSwitch);
                ThirdPersonCam.gameObject.SetActive (!camSwitch);
                Cursor.lockState = CursorLockMode.Locked;
                PlayerControllerCameron.instance.basicAttack = true;
                CamController.instance.CameraInInventory = true;
                PauseCanvasObject.SetActive (false);
                InGameHudCanvas.SetActive (true);
                Cursor.visible = false;

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