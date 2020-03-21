using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCameron : MonoBehaviour
{
    [HideInInspector]
    Animator _playerAnim;
    public static PlayerControllerCameron instance;

    bool running = false;
    [HideInInspector]
    public Rigidbody _rb;

    // Jumping
    bool onGround = true;
    bool canDoubleJump = false;

    void Start ()
    {
        instance = this;
        _rb = GetComponent<Rigidbody> ();
        _playerAnim = GetComponent<Animator> ();
    }

    void TakeOff ()
    {
        GetComponent<Animator> ().applyRootMotion = false;
    }
    void Landing ()
    {
        GetComponent<Animator> ().applyRootMotion = true;
    }

    void Update ()
    {
        // Walking
        if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0)
        {
            _playerAnim.SetFloat ("Z", Input.GetAxis ("Vertical"));
            _playerAnim.SetFloat ("X", Input.GetAxis ("Horizontal"));
            transform.rotation = Quaternion.Lerp (transform.rotation, CamController.instance.transform.rotation, Time.deltaTime * CamController.instance.rotateSpeed);
        }
        else
        {
            _playerAnim.SetFloat ("Z", 0);
            _playerAnim.SetFloat ("X", 0);
        }

        if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0)
        {
            _playerAnim.SetBool ("Moving", true);

        }
        else
        {
            _playerAnim.SetBool ("Moving", false);
        }

        // Running
        if (Input.GetKeyDown ("left shift"))
        {
            running = true;
            Debug.Log ("Shift Down");
        }
        if (Input.GetKeyUp ("left shift"))
        {
            running = false;
            _playerAnim.SetFloat ("Z", Input.GetAxis ("Vertical") * 1);
            _playerAnim.SetFloat ("X", Input.GetAxis ("Horizontal") * 1);
            Debug.Log ("Shift Up");
        }

        if (running == true)
        {
            _playerAnim.SetFloat ("Z", Input.GetAxis ("Vertical") * 2);
            _playerAnim.SetFloat ("X", Input.GetAxis ("Horizontal") * 2);
            Debug.Log ("Running!");

            if (Input.GetButtonDown ("Jump"))
            {
                _playerAnim.SetTrigger ("RunningJump");
                Debug.Log ("RunJumpCalled");
            }
        }

        // Jumping
        RaycastHit hit;
        Vector3 physicsCenter = this.transform.position + this.GetComponent<CapsuleCollider> ().center;

        if (Physics.Raycast (physicsCenter, Vector3.down, out hit, 2f))
        {
            if (hit.transform.gameObject.tag != "Player")
            {

                onGround = true;
                _playerAnim.SetBool ("Falling", false);
                Landing ();
                Debug.Log ("Rootmotion On");

            }
        }
        else
        {
            _playerAnim.SetBool ("Falling", true);
            onGround = false;
            // TakeOff ();
            Debug.Log ("Rootmotion Off");

        }

        if (!running)
        {
            if (Input.GetKeyDown ("space") && !onGround && canDoubleJump)
            {
                this.GetComponent<Rigidbody> ().AddForce (Vector3.up * 10);
                Landing ();
                canDoubleJump = false;
            }
            else if (Input.GetKeyDown ("space") && onGround)
            {
                _playerAnim.SetTrigger ("JumpUp");
                this.GetComponent<Rigidbody> ().AddForce (Vector3.up * 20);
                Landing ();
                canDoubleJump = true;
            }
        }

        if (Input.GetKeyDown (KeyCode.LeftControl))
        {
            _playerAnim.SetTrigger ("Roll");
        }

        if (Input.GetKeyDown (KeyCode.F))
        {
            _playerAnim.SetTrigger ("RunningSlide");
        }

        if (Input.GetKeyDown (KeyCode.G))
        {
            _playerAnim.SetTrigger ("JumpOver");
        }

        if (Input.GetKeyDown (KeyCode.E))
        {
            _playerAnim.SetTrigger ("JumpGap");
        }
    }

    // private void OnTriggerExit (Collider other)
    // {
    //     if (other.CompareTag ("TowerGrid"))
    //     {
    //         GetComponent<Rigidbody> ().useGravity = false;
    //     }
    // }

    // private void OnTriggerEnter (Collider other)
    // {
    //     if (other.CompareTag ("TowerGrid"))
    //     {
    //         GetComponent<Rigidbody> ().useGravity = true;
    //     }
    // }
    private void OnCollisionEnter (Collision other)
    {
        if (other.collider.CompareTag ("platform"))
        {
            transform.SetParent (other.transform);
        }
    }
    private void OnCollisionExit (Collision other)
    {
        if (other.collider.CompareTag ("platform"))
        {
            transform.SetParent (null);
        }
    }

}