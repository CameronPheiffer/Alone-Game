using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    // Declaring the variables that the camera needs to move and follow the player
    public float hspeed = 5f;
    public float vspeed = 5f;
    public float moveSpeed = 5;
    public float rotateSpeed = 5;
    private Vector3 _camPosOriginal;

    public static CamController instance;

    void Start ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        instance = this;
    }

    void Update ()
    {
        // Controlling the camera with the mouse
        float h = hspeed * Input.GetAxisRaw ("Mouse X");
        float v = vspeed * Input.GetAxisRaw ("Mouse Y");

        //variables needed to coordinate look direction
        transform.Rotate (0, h, 0);
        transform.GetChild (0).Rotate (-v, 0, 0);

        // Follow the player
        transform.position = Vector3.Lerp (transform.position, PlayerController.instance.transform.position, Time.deltaTime * moveSpeed);
    
    }
}