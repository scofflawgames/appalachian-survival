using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBelt : MonoBehaviour
{
    public List<GameObject> toolBeltSlots = new List<GameObject>();

    public List<GameObject> wieldableItems = new List<GameObject>();

    public int selectedItem = 0;
    //private int maxItems = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) //list toolbelt items
        {
            //Slot emptySlot = null;
            for (int i = 0; i < toolBeltSlots.Count; i++)
            {
                Slot currentSlot = toolBeltSlots[i].GetComponent<Slot>();
                Debug.Log(currentSlot.myItem);   
            }
        }

        ItemSelector();
    }

    public void ItemSelector()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedItem < 6)
            {
                selectedItem += 1;
            }

            if (selectedItem == 6)
            {
                selectedItem = 0;
            }

            Debug.Log("Selected Item: " + selectedItem);
            Slot currentSlot = toolBeltSlots[selectedItem].GetComponent<Slot>();
            Debug.Log(currentSlot.myItem);

            if (currentSlot.myItem != null)
            {
                WieldableEquip(currentSlot.myItem.itemName);
            }
            else
            {
                WieldableEquip("null");
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (selectedItem > -1)
            {
                selectedItem -= 1;
            }

            if (selectedItem == -1)
            {
                selectedItem = 5;
            }

            Debug.Log("Selected Item: " + selectedItem);
            Slot currentSlot = toolBeltSlots[selectedItem].GetComponent<Slot>();
            Debug.Log(currentSlot.myItem);

            if (currentSlot.myItem != null)
            {
                WieldableEquip(currentSlot.myItem.itemName);
            }
            else
            {
                WieldableEquip("null");
            }
        }

    }

    public void WieldableEquip(string ItemName)
    {
        if (ItemName == "Axe001")
        {
            wieldableItems[0].SetActive(true);
        }
        else if(ItemName == "null")
        {
            wieldableItems[0].SetActive(false);
        }
    }

}
