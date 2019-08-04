using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    Inventory inventory;
    Image myImage = null;
    TextMeshProUGUI myText = null;
    //GameObject itemAmount;
    //Slider durabilityBar;


    public Item myItem;
    public int myAmount;

    void Start()
    {
        //itemAmount = transform.GetChild(1).GetComponent<GameObject>(); //(Goal is to get gameobject in 2nd child and disable/enable in regards to amount)
        inventory = GameObject.FindObjectOfType<Inventory>();
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

    void ShowUI()
    {
        if (myItem != null)
        {
            myImage.enabled = true;
            myText.enabled = true;
            myImage.sprite = myItem.itemIcon;
            myText.text = myAmount.ToString();
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
            myImage.enabled = false;
            myText.enabled = false;
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

    public void OnDrop(PointerEventData eventData)
    {
        AddItem(inventory.draggingItem, inventory.draggingAmount);
        inventory.EndDrag();
    }


    //---------------------------------Mouse Over Stuff------------------

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (myItem != null)
        //{
          //  inventory.ShowToolTip(myItem);
        //}
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //inventory.HideToolTip();
    }

    //end o class
}