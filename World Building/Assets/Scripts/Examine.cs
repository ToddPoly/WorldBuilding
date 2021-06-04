using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Examine : MonoBehaviour
{
    Camera mainCam;//Camera Object Will Be Placed In Front Of
    GameObject clickedObject;//Currently Clicked Object
    public SC_FPSController FPSController;

    public TextMeshProUGUI textMesh;
    public Object ExaminedObject;
    public Image highlight;

    //Holds Original Postion And Rotation So The Object Can Be Replaced Correctly
    Vector3 originalPosition;
    Vector3 originalRotation;

    //If True Allow Rotation Of Object
    bool examineMode;

    void Start()
    {
        textMesh.enabled = false;
        mainCam = Camera.main;
        examineMode = false;
    }

    private void Update()
    {
        ClickObject();//Decide What Object To Examine

        TurnObject();//Allows Object To Be Rotated

        ExitExamineMode();//Returns Object To Original Postion
    }

    void ClickObject()
    {
        if (Input.GetMouseButtonDown(0) && examineMode == false)
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Object")
                {
                    ExaminedObject = hit.transform.GetComponent<Object>();
                    textMesh.text = ExaminedObject.objectText;
                    textMesh.enabled = true;

                    //ClickedObject Will Be The Object Hit By The Raycast
                    clickedObject = hit.transform.gameObject;

                    //Save The Original Postion And Rotation
                    originalPosition = clickedObject.transform.position;
                    originalRotation = clickedObject.transform.rotation.eulerAngles;

                    //Now Move Object In Front Of Camera
                    clickedObject.transform.position = mainCam.transform.position + (transform.forward * 3f);

                    //Pause The Game
                    Time.timeScale = 0;

                    //Stop player camera rotation
                    FPSController.lookSpeed = 0;

                    //Turn Examine Mode To True
                    examineMode = true;
                }
            }
        }
    }

    void TurnObject()
    {
        if (Input.GetMouseButton(0) && examineMode)
        {
            float rotationSpeed = 15;

            float xAxis = Input.GetAxis("Mouse X") * rotationSpeed;
            float yAxis = Input.GetAxis("Mouse Y") * rotationSpeed;

            clickedObject.transform.Rotate(Vector3.up, -xAxis, Space.World);
            clickedObject.transform.Rotate(Vector3.right, yAxis, Space.World);
        }
    }

    void ExitExamineMode()
    {
        if (Input.GetMouseButtonDown(1) && examineMode)
        {
            //Reset Object To Original Position
            clickedObject.transform.position = originalPosition;
            clickedObject.transform.eulerAngles = originalRotation;

            //Unpause Game
            Time.timeScale = 1;

            //Start player camera rotation
            FPSController.lookSpeed = FPSController.intLookSpeed;

            //Return To Normal State
            examineMode = false;

            textMesh.enabled = false;
        }
    }
}