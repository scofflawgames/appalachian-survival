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
    private ItemDatabase database;

    private void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
        database = GameObject.FindObjectOfType<ItemDatabase>();
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

        if (File.Exists(Application.persistentDataPath + "/game_save/inventory_data/save001"))
        {
            File.Delete(Application.persistentDataPath + "/game_save/inventory_data/save001");
        }

            FileStream file = File.Create(Application.persistentDataPath + "/game_save/inventory_data/save001");
        var json = JsonUtility.ToJson(inventoryData);
        bf.Serialize(file, json);
        file.Close();
    }

    public void LoadGame()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/inventory_data"))
        {
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/inventory_data/save001"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/inventory_data/save001", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), inventoryData);

            for (int i = 0; i < inventoryData.slotIDs.Length; i++)
            {
                Slot currentSlot = inventory.slots[i].GetComponent<Slot>();

                if (currentSlot.myItem != null)
                {
                    currentSlot.RemoveItem(currentSlot.myAmount);
                }

                if (inventoryData.itemAMTs[i] > 0)
                {
                    inventory.AddItemSpecific(database.GetItemById(inventoryData.itemIDs[i]), inventoryData.itemAMTs[i], inventoryData.slotIDs[i]);
                }
                //currentSlot.myAmount = itemAMTs[i];
            }

            file.Close();
        }
    }


}

[Serializable]
public class InventoryData
{
    public int[] itemIDs = null;
    public int[] slotIDs = null;
    public int[] itemAMTs = null;
}
