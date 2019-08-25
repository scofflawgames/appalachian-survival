using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    Inventory inventory;
    ToolBelt toolBelt;
    public Image myImage = null;
    Image selector = null;
    TextMeshProUGUI myText = null;
    //GameObject itemAmount;
    //Slider durabilityBar;
    

    public Item myItem;
    public int myAmount;
    public int slotID;

    void Awake()
    {
        selector = transform.GetChild(2).GetComponent<Image>();
        selector.enabled = false;
    }

    void Start()
    {
        //itemAmount = transform.GetChild(1).GetComponent<GameObject>(); //(Goal is to get gameobject in 2nd child and disable/enable in regards to amount)
        inventory = GameObject.FindObjectOfType<Inventory>();
        toolBelt = GameObject.FindObjectOfType<ToolBelt>();
        myImage = transform.GetChild(0).GetComponent<Image>();
        myText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        
        //durabilityBar = transform.GetChild(2).GetComponent<Slider>();
        
        ShowUI();
    }


    public void AddItem(Item itemToAdd, int amnt)
    {
        if (itemToAdd == myItem)
        {
            myAmount += amnt;
        }
        else
        {
            myItem = itemToAdd;
            myAmount = amnt;
        }
        ShowUI();
    }

    public void RemoveItem(int amnt)
    {
        if (myItem != null)
        {
            myAmount -= amnt;
            if (myAmount <= 0)
            {
                //itemAmount.SetActive(false);
                myItem = null;
            }
        }
        ShowUI();
    }

    public void ShowUI()
    {
        if (myItem != null)
        {
            if (myImage != null)
            {
                myImage.enabled = true;
                myImage.sprite = myItem.itemIcon;
            }

            if (myText != null)
            { 
            myText.enabled = true;           
            myText.text = myAmount.ToString();
            }

            if (myItem.useDurability)
            {
               // durabilityBar.gameObject.SetActive(true);
               // durabilityBar.maxValue = myItem.maxDurabilty;
                //durabilityBar.value = myItem.currentDurability;
            }
            else
            {
                //durabilityBar.gameObject.SetActive(false);
            }
        }
        else
        {
            if (myImage != null)
            {
                myImage.enabled = false;
            }

            if (myText != null)
            {
                myText.enabled = false;
            }

            //durabilityBar.gameObject.SetActive(false);
        }
    }



    //--------------------------------------Drag and Drop stuff---------------------------
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (myItem != null)
        {
            inventory.DoDrag(myItem, myAmount);
            RemoveItem(myAmount);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventory.draggingImage.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            print(eventData.pointerCurrentRaycast.gameObject.tag);
            if (!eventData.pointerCurrentRaycast.gameObject.CompareTag("Slot"))
            {
                inventory.AddItemSpecific(inventory.draggingItem, inventory.draggingAmount, slotID);
                inventory.EndDrag();
            }

        }
        else
        {
            inventory.AddItemSpecific(inventory.draggingItem, inventory.draggingAmount, slotID);
            inventory.EndDrag();
        }


    }

    public void OnDrop(PointerEventData eventData)
    {
        Slot dropSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>();

        if (dropSlot.myItem == null && eventData.pointerCurrentRaycast.gameObject.CompareTag("Slot"))
        {
            AddItem(inventory.draggingItem, inventory.draggingAmount);
            inventory.EndDrag();
            toolBelt.DragAndDropCheck();
        }
        //else if(dropSlot != null && eventData.pointerCurrentRaycast.gameObject.CompareTag("Slot") && dropSlot.slotID == slotID)
       // {
            //print("Same slot as it was, currently and error!!");
            //inventory.AddItemSpecific(inventory.draggingItem, inventory.draggingAmount, slotID);
         //   inventory.EndDrag();
        //}


    }


    //---------------------------------Mouse Over Stuff------------------

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (myItem != null)
        {
            inventory.ShowToolTip(myItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventory.HideToolTip();
    }

    //end o class
}