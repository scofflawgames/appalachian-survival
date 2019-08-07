using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveManager : MonoBehaviour
{
    public InventoryData inventoryData;
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    public void SaveGame()
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/inventory_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/inventory_data");
        }

        for (int i = 0; i < 24; i++)
        {
            Slot currentSlot = inventory.slots[i].GetComponent<Slot>();
            if (currentSlot.myItem != null)
            {
                inventoryData.itemIDs[i] = currentSlot.myItem.itemID;
                inventoryData.slotIDs[i] = i;
                inventoryData.itemAMTs[i] = currentSlot.myAmount;
            }
        }


        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/inventory_data/save001");
        var json = JsonUtility.ToJson(inventoryData);
        bf.Serialize(file, json);
        file.Close();
    }


}

[Serializable]
public class InventoryData
{
    public int[] itemIDs = null;
    public int[] slotIDs = null;
    public int[] itemAMTs = null;
}
