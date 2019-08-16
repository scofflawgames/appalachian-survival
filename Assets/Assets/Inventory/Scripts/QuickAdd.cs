using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickAdd : MonoBehaviour
{

    private ItemDatabase dataBase;
    private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        dataBase = FindObjectOfType<ItemDatabase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Inventory.inventoryActive)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                inventory.AddItem(dataBase.GetItemById(1), 1);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                inventory.AddItem(dataBase.GetItemById(2), 1);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                inventory.AddItem(dataBase.GetItemById(3), 1);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                inventory.AddItem(dataBase.GetItemById(4), 1);
            }
        }

    }
}
