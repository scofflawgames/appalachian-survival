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


    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentPlaceableObjectToMouse();
            RotateWithMouseClick();
            ReleaseIfClicked();
        }
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            placeableObjectCollider.enabled = true;
            currentPlaceableObject = null;
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
            currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.5f, hitInfo.point.z);
            //currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal)
        }
    }

    private void HandleNewObjectHotkey()
    {
        if (Input.GetKeyDown(newObjectHotKey))
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
}
