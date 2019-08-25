using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementManager : MonoBehaviour
{
    [SerializeField]
    private GameObject placeableObjectPrefab =  null; //will need to refactor for modular sake

    [SerializeField]
    private KeyCode newObjectHotKey = KeyCode.F;

    private GameObject currentPlaceableObject;

    private BoxCollider placeableObjectCollider;

    private GroundGrid groundGrid;

    private ToolBelt toolBelt;

    public static bool activeBlock = false;

    public int blockSide;

    private void Awake()
    {
        groundGrid = FindObjectOfType<GroundGrid>();
        toolBelt = FindObjectOfType<ToolBelt>();
    }

    private void Update()
    {
        if (activeBlock)
        {
            DetectWhichSide();

            HandleNewObjectHotkey();
            if (currentPlaceableObject != null)
            {
                MoveCurrentPlaceableObjectToMouse();
                RotateWithMouseClick();
                ReleaseIfClicked();
            }
        }
    }

    public void DetectWhichSide()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 MyNormal = hitInfo.normal;
            //print(hitInfo.collider.tag);
            if (hitInfo.collider.CompareTag("Block"))
            {
                MyNormal = hitInfo.transform.TransformDirection(MyNormal);

                if (MyNormal == hitInfo.transform.up)
                {
                    //print("Hitting top of plane/object!!");
                    blockSide = 1;
                }

                if (MyNormal == -hitInfo.transform.up)
                {
                   // print("Hitting bottom of plane/object!!");
                    blockSide = 2;
                }

                if (MyNormal == hitInfo.transform.right)
                {
                   // print("Hitting right side!!");
                }

                if (MyNormal == -hitInfo.transform.right)
                {
                   // print("Hitting left side!!");
                }

                if (MyNormal == hitInfo.transform.forward)
                {
                   // print("Hitting front side!!");
                }

                if (MyNormal == -hitInfo.transform.forward)
                {
                    //print("Hitting back side!!");
                }
            }
        }
    }
            //currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.5f, hitInfo.point.z);
            //currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal)
 


    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            placeableObjectCollider.enabled = true;
            currentPlaceableObject = null;
            toolBelt.DestroyCurrentItemSpecific(1);
        }
    }

    private void RotateWithMouseClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            currentPlaceableObject.transform.Rotate(Vector3.up * 90);
        }
    }

    private void MoveCurrentPlaceableObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 defaulthitInfoPoints = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);

            PlaceObjectNear(hitInfo.point);

            //currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
            //currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal)
        }
    }

    private void HandleNewObjectHotkey()
    {
        if (Input.GetKeyDown(newObjectHotKey))
        //if(activeBlock)
        {
            if (currentPlaceableObject == null)
            {
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
                placeableObjectCollider = currentPlaceableObject.GetComponent<BoxCollider>();
                placeableObjectCollider.enabled = false;
            }
            else
            {
                Destroy(currentPlaceableObject);
            }
        }
    }

    private void PlaceObjectNear(Vector3 clickPoint)
    {
        var finalPosition = groundGrid.GetNearestPointOnGrid(clickPoint);

        currentPlaceableObject.transform.position = finalPosition;
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;

        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;
    }
}
