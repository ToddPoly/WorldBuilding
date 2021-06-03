using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform destination;
    public float rotSpeed = 20;
    public bool examineMode;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !examineMode)
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.parent = GameObject.Find("Destination").transform;
            this.transform.position = destination.position;

            //Pause The Game
            Time.timeScale = 0;

            examineMode = true;
        }

        if(Input.GetMouseButton(0) && examineMode)
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

            transform.Rotate(Vector3.up, -rotX);
            transform.Rotate(Vector3.right, rotY);      
        }

        if (Input.GetMouseButton(1) && examineMode)
        {
            GetComponent<BoxCollider>().enabled = true;
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;

            //Unpause The Game
            Time.timeScale = 1;

            examineMode = false;
        }
    }
}
