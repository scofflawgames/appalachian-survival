using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBelt : MonoBehaviour
{
    public List<GameObject> toolBeltSlots = new List<GameObject>();

    public List<GameObject> wieldableItems = new List<GameObject>();

    public int selectedItem = 0;
    public int previousSelected = 0;
    //private int maxItems = 5;

    Image selectorImage = null;
        
    // Start is called before the first frame update
    void Start()
    {
        selectorImage = toolBeltSlots[selectedItem].transform.GetChild(2).GetComponent<Image>();
        selectorImage.enabled = true;
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
                previousSelected = selectedItem;
                selectedItem += 1;
            }

            if (selectedItem == 6)
            {
                previousSelected = 5;
                selectedItem = 0;
            }

            Debug.Log("Selected Item: " + selectedItem);
            Slot currentSlot = toolBeltSlots[selectedItem].GetComponent<Slot>();
            Debug.Log(currentSlot.myItem);
            //SPACE
            //remove outline from past item
            selectorImage = toolBeltSlots[previousSelected].transform.GetChild(2).GetComponent<Image>();
            selectorImage.enabled = false;

            //outline current item
            selectorImage = toolBeltSlots[selectedItem].transform.GetChild(2).GetComponent<Image>();
            selectorImage.enabled = true;

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
                previousSelected = selectedItem;
                selectedItem -= 1;
            }

            if (selectedItem == -1)
            {
                previousSelected = 0;
                selectedItem = 5;
            }

            Debug.Log("Selected Item: " + selectedItem);
            Slot currentSlot = toolBeltSlots[selectedItem].GetComponent<Slot>();
            Debug.Log(currentSlot.myItem);
            //SPACE
            //remove outline from past item
            selectorImage = toolBeltSlots[previousSelected].transform.GetChild(2).GetComponent<Image>();
            selectorImage.enabled = false;

            //outline current item
            selectorImage = toolBeltSlots[selectedItem].transform.GetChild(2).GetComponent<Image>();
            selectorImage.enabled = true;

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
