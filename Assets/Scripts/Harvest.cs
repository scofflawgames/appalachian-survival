using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    private Camera cam;
    private ItemDatabase itemDatabase;
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        inventory = FindObjectOfType<Inventory>();
        itemDatabase = FindObjectOfType<ItemDatabase>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2))
        {
            //print("I'm looking at " + hit.transform.name);
            if (hit.transform.name == "Mushroom001")
            {
                Destroy(hit.transform.gameObject);
                inventory.AddItem(itemDatabase.GetItemById(5), 2);
            }
        }
        else
        {
            //print("I'm looking at nothing!");
        }
    }
}

