using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTest : MonoBehaviour
{
    public Item[] item;
    public int[] slotIDs;
    public int[] itemAMTs;

    public ItemDatabase dataBase;
    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slotIDs.Length; i++)
        {
            Slot currentSlot = inventory.slots[i].GetComponent<Slot>();
            currentSlot.myItem = item[i];
            currentSlot.myAmount = itemAMTs[i];
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
