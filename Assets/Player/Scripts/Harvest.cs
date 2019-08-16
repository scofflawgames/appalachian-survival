using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Harvest : MonoBehaviour
{
    [Header("Public References")]
    [SerializeField]
    public GameObject pickUpPrompt = null;
   

    private TextMeshProUGUI pickUpPromptText;
    private Camera cam;
    private ItemDatabase itemDatabase;
    private Inventory inventory;
    private ToolBelt toolBelt;
    

    private void Awake()
    {
        toolBelt = FindObjectOfType<ToolBelt>();
        pickUpPrompt = GameObject.FindGameObjectWithTag("PickupPrompt");
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        pickUpPromptText = pickUpPrompt.GetComponent<TextMeshProUGUI>();
        //pickUpPrompt.SetActive(false);        
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
            DirtyWater dirtyWater = hit.collider.GetComponent<DirtyWater>();

            if (harvestable != null)
            {
                harvestPrompt(hit.transform.gameObject, hit.transform.name, harvestable.objectID, harvestable.objectAmt);
            }
            else if (dirtyWater != null)
            {
                //print("Is looking at dirty water?");
                //print(toolBelt.currentItemID);
                dirtyWaterPrompt(hit.transform.name, dirtyWater.waterID);
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
            print("Harvested " + objectAMT + " " + objectName + "(S)");
            //some kind of audio for harvesting
            inventory.AddItem(itemDatabase.GetItemById(objectID), objectAMT);
            Destroy(harvestObject);
        }
    }

    private void dirtyWaterPrompt(string objectName, int objectID)
    {
        if (toolBelt.currentItemID == 6)
        {
            pickUpPrompt.SetActive(true);
            pickUpPromptText.text = ("PRESS <E> TO FILL EMPTY BOTTLE(S) WITH: " + objectName);
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.AddItem(itemDatabase.GetItemById(objectID), toolBelt.currentItemAMT);

                //refresh until I can add item specific to the slot that the previous item was removed from
                toolBelt.currentItemID = 0;
                toolBelt.currentItemAMT = 0;

                toolBelt.DestroyCurrentItem();
                pickUpPrompt.SetActive(false);
            }
        }

    }

}

