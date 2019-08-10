using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSaveManager : MonoBehaviour
{
    public InventoryData inventoryData;

    [Header("Save/Load Menus")]
    public GameObject saveMenu;
    public GameObject loadMenu;

    [Header("Save Button Text")]
    public TextMeshProUGUI save001;
    public TextMeshProUGUI save002;
    public TextMeshProUGUI save003;

    [Header("Load Button Text")]
    public TextMeshProUGUI load001;
    public TextMeshProUGUI load002;
    public TextMeshProUGUI load003;

    private Inventory inventory;
    private ItemDatabase database;
    private PauseMenu pauseMenu;
    private ToolBelt toolBelt;

    private void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
        database = GameObject.FindObjectOfType<ItemDatabase>();
        pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
        toolBelt = GameObject.FindObjectOfType<ToolBelt>();
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    public void NewGame()
    {
        pauseMenu.UnpauseGame();
        Inventory.inventoryActive = false;
        Inventory.craftingActive = false;

        for (int i = 0; i < inventoryData.slotIDs.Length; i++)
        {
            Slot currentSlot = inventory.slots[i].GetComponent<Slot>();

            if (currentSlot.myItem != null)
            {
                currentSlot.RemoveItem(currentSlot.myAmount);
            }
        }

        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void SaveGame(string saveFile, string saveType)
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/" + saveFile))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/" + saveFile);
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

        if (File.Exists(Application.persistentDataPath + "/game_save/" + saveFile + "/" + saveType))
        {
            File.Delete(Application.persistentDataPath + "/game_save/" + saveFile + "/" + saveType);
        }

            FileStream file = File.Create(Application.persistentDataPath + "/game_save/" + saveFile + "/" + saveType);
        var json = JsonUtility.ToJson(inventoryData);
        bf.Serialize(file, json);
        file.Close();

        saveMenu.SetActive(false);

        if (!PauseMenu.isPaused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void LoadGame(string saveFile, string saveType)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/" + saveFile))
        {
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/" + saveFile + "/" + saveType))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/" + saveFile + "/" + saveType, FileMode.Open);
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
            toolBelt.DragAndDropCheck();
            file.Close();

            loadMenu.SetActive(false);

            if (!PauseMenu.isPaused)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void SaveMenu()
    {
        saveMenu.SetActive(true);
        SaveButtonText();
    }

    public void LoadMenu()
    {
        loadMenu.SetActive(true);
        LoadButtonText();
    }




    //methods for the save/load buttons
    public void Save001()
    {
        SaveGame("save001", "inventory.txt");
    }

    public void Save002()
    {
        SaveGame("save002", "inventory.txt");
    }

    public void Save003()
    {
        SaveGame("save003", "inventory.txt");
    }

    //load methods
    public void Load001()
    {
        LoadGame("save001", "inventory.txt");
    }

    public void Load002()
    {
        LoadGame("save002", "inventory.txt");
    }

    public void Load003()
    {
        LoadGame("save003", "inventory.txt");
    }

    //checks for save files
    public void SaveButtonText()
    {
        if (Directory.Exists(Application.persistentDataPath + "/game_save/save001"))
        {
            save001.text = "Save001";
        }

        if (Directory.Exists(Application.persistentDataPath + "/game_save/save002"))
        {
            save002.text = "Save002";
        }

        if (Directory.Exists(Application.persistentDataPath + "/game_save/save003"))
        {
            save003.text = "Save003";
        }
    }

    public void LoadButtonText()
    {
        if (Directory.Exists(Application.persistentDataPath + "/game_save/save001"))
        {
            load001.text = "Save001";
        }

        if (Directory.Exists(Application.persistentDataPath + "/game_save/save002"))
        {
            load002.text = "Save002";
        }

        if (Directory.Exists(Application.persistentDataPath + "/game_save/save003"))
        {
            load003.text = "Save003";
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

[Serializable]
public class PlayerStateData
{
    public Vector3 playerPos;
    public Quaternion playerRot;

}

[Serializable]
public class SaveFileDateTime
{

}
