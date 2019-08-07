using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTest : MonoBehaviour
{
    private int[] itemIDs = null;
    private int[] slotIDs = null;
    private int[] itemAMTs = null;

    public ItemDatabase dataBase;
    public Inventory inventory;

    public bool runLoadTest = false;

    // Start is called before the first frame update
    void Start()
    {
        if (runLoadTest)
        {
            for (int i = 0; i < slotIDs.Length; i++)
            {
                Slot currentSlot = inventory.slots[i].GetComponent<Slot>();
                inventory.AddItemSpecific(dataBase.GetItemById(itemIDs[i]), itemAMTs[i], slotIDs[i]);
                currentSlot.myAmount = itemAMTs[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
