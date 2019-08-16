using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Harvest : MonoBehaviour
{
    [Header("Public References")]
    public GameObject pickUpPrompt;

    private TextMeshProUGUI pickUpPromptText;
    private Camera cam;
    private ItemDatabase itemDatabase;
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        pickUpPromptText = pickUpPrompt.GetComponent<TextMeshProUGUI>();

        cam = GetComponent<Camera>();
        inventory = FindObjectOfType<Inventory>();
        itemDatabase = FindObjectOfType<ItemDatabase>();
    }

    private void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2))
        {
            Harvestable harvestable = hit.collider.GetComponent<Harvestable>();

            if (harvestable != null)
            {
                harvestPrompt(hit.transform.gameObject, hit.transform.name, harvestable.objectID, harvestable.objectAmt);
            }
            //print("I'm looking at " + hit.transform.name);
            //if (hit.transform.name == "Mushroom001")
            //{
            //  Destroy(hit.transform.gameObject);
            // inventory.AddItem(itemDatabase.GetItemById(5), 2);
            // }
        }
        else
        {
            pickUpPrompt.SetActive(false);
        }

    }

    private void harvestPrompt(GameObject harvestObject, string objectName, int objectID, int objectAMT)
    {
        pickUpPrompt.SetActive(true);
        pickUpPromptText.text = ("PRESS <E> TO PICK UP: " + objectName);

        if (Input.GetKeyDown(KeyCode.E))
        {
            //some kind of audio for harvesting
            inventory.AddItem(itemDatabase.GetItemById(objectID), objectAMT);
            Destroy(harvestObject);
        }
    }
}

