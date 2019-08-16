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
    public PlayerStateData playerStateData;
    public SaveFileDateTime saveFileDateTime;

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
    private int inventorySize = 24;
    private ItemDatabase database;
    private PauseMenu pauseMenu;
    private ToolBelt toolBelt;

    [Header("Player References")]
    public PlayerFPSController playerFPSController;
    public GameObject playerObject;
    public GameObject newPlayerObject;
    public Harvest harvest;

    private void Awake()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
        database = GameObject.FindObjectOfType<ItemDatabase>();
        pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
        toolBelt = GameObject.FindObjectOfType<ToolBelt>();
        //RemoveAllItems(); 
    }

    private void Start()
    {
        playerFPSController = GameObject.FindObjectOfType<PlayerFPSController>();
        playerObject = playerFPSController.gameObject;
        harvest = GameObject.FindObjectOfType<Harvest>();
        harvest.pickUpPrompt.SetActive(true);
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    /// <summary>
    /// Removes All Items from inventory
    /// </summary>
    public void RemoveAllItems()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            Slot currentSlot = inventory.slots[i].GetComponent<Slot>();

            if (currentSlot.myItem != null)
            {
                currentSlot.RemoveItem(currentSlot.myAmount);
            }
        }
    }


    public void NewGame()
    {
        RemoveAllItems();
        pauseMenu.UnpauseGame();
        PauseMenu.isPaused = false;
        Inventory.inventoryActive = false;
        Inventory.craftingActive = false;
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void SaveGame(string saveFile, string saveType)
    {
        playerFPSController = GameObject.FindObjectOfType<PlayerFPSController>();
        playerObject = playerFPSController.gameObject;

        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/" + saveFile))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/" + saveFile);
        }


        //handle types of save files
        if (saveType == "inventory.txt")
        {
            for (int i = 0; i < inventorySize; i++)
            {
                Slot currentSlot = inventory.slots[i].GetComponent<Slot>();
                //inventoryData.slotIDs[i] = currentSlot.slotID;

                if (currentSlot.myItem != null)
                {
                    inventoryData.itemIDs[i] = currentSlot.myItem.itemID;                  
                    inventoryData.itemAMTs[i] = currentSlot.myAmount;
                }
            }
        }

        if (saveType == "player.txt")
        {
            playerStateData.playerPos = playerObject.transform.position;
            playerStateData.playerRot = playerObject.transform.rotation;
        }

        if (saveType == "dateTime.txt")
        {
            saveFileDateTime.time = DateTime.Now.ToString("hh:mm:ss");
        }
        //end of handling types of save files


        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/game_save/" + saveFile + "/" + saveType))
        {
            File.Delete(Application.persistentDataPath + "/game_save/" + saveFile + "/" + saveType);
        }

        FileStream file = File.Create(Application.persistentDataPath + "/game_save/" + saveFile + "/" + saveType);

        //different saveTypes fo Json-ing
        if (saveType == "inventory.txt")
        {
            var json = JsonUtility.ToJson(inventoryData);
            bf.Serialize(file, json);
        }

        if (saveType == "player.txt")
        {
            var json = JsonUtility.ToJson(playerStateData);
            bf.Serialize(file, json);
        }

        if (saveType == "dateTime.txt")
        {
            var json = JsonUtility.ToJson(saveFileDateTime);
            bf.Serialize(file, json);
        }
        //end of jsoning


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
            

            //handle save types
            if (saveType == "inventory.txt")
            {
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), inventoryData);

                //removing old inventory
                for (int i = 0; i < inventorySize; i++)
                {
                    Slot currentSlot = inventory.slots[i].GetComponent<Slot>();
                    currentSlot.myAmount = 0;

                    if (currentSlot.myImage != null)
                    {
                        currentSlot.myImage.enabled = false;
                    }

                    currentSlot.myItem = null;
                    currentSlot.ShowUI();
                }

                //adding the new inventory
                for (int i = 0; i < inventorySize; i++)
                {

                    if (inventoryData.itemIDs[i] > 0)
                    {
                        inventory.AddItemSpecific(database.GetItemById(inventoryData.itemIDs[i]), inventoryData.itemAMTs[i], i);
                    }
                }

            }


            //handles loading for playerState
            if (saveType == "player.txt")
            {
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), playerStateData);
                Destroy(playerObject);
                playerObject = Instantiate(newPlayerObject, playerStateData.playerPos, playerStateData.playerRot);
            }

            toolBelt.DragAndDropCheck();
            file.Close();

            loadMenu.SetActive(false);
            pauseMenu.pauseMenu.SetActive(false);
            PauseMenu.isPaused = false;
            pauseMenu.UnpauseGame();

            //set pickupPrompt to active
            harvest = GameObject.FindObjectOfType<Harvest>();
            harvest.pickUpPrompt.SetActive(true);

        }
    }

    public void SaveMenu()
    {
        loadMenu.SetActive(false);
        saveMenu.SetActive(true);
        SaveButtonText();
    }

    public void LoadMenu()
    {
        saveMenu.SetActive(false);
        loadMenu.SetActive(true);
        LoadButtonText();
    }




    //methods for the save/load buttons
    public void Save001()
    {
        string date = DateTime.Now.ToString("hh:mm:ss");
        print(date);

        SaveGame("save001", "inventory.txt");
        SaveGame("save001", "player.txt");
        SaveGame("save001", "dateTime.txt");
    }

    public void Save002()
    {
        SaveGame("save002", "inventory.txt");
        SaveGame("save002", "player.txt");
        SaveGame("save002", "dateTime.txt");
    }

    public void Save003()
    {
        SaveGame("save003", "inventory.txt");
        SaveGame("save003", "player.txt");
        SaveGame("save003", "dateTime.txt");
    }

    //load methods
    public void Load001()
    {
        LoadGame("save001", "inventory.txt");
        LoadGame("save001", "player.txt");
    }

    public void Load002()
    {
        LoadGame("save002", "inventory.txt");
        LoadGame("save002", "player.txt");
    }

    public void Load003()
    {
        LoadGame("save003", "inventory.txt");
        LoadGame("save003", "player.txt");
    }

    //checks for save files
    public void SaveButtonText()
    {

        if (Directory.Exists(Application.persistentDataPath + "/game_save/save001"))
        {
            GetDateTimeSave("save001");       
        } 

        if (Directory.Exists(Application.persistentDataPath + "/game_save/save002"))
        {
            GetDateTimeSave("save002");
        }

        if (Directory.Exists(Application.persistentDataPath + "/game_save/save003"))
        {
            GetDateTimeSave("save003");
        }
    }

    public void LoadButtonText()
    {
        if (Directory.Exists(Application.persistentDataPath + "/game_save/save001"))
        {
            GetDateTimeSave("save001");
        }

        if (Directory.Exists(Application.persistentDataPath + "/game_save/save002"))
        {
            GetDateTimeSave("save002");
        }

        if (Directory.Exists(Application.persistentDataPath + "/game_save/save003"))
        {
            GetDateTimeSave("save003");
        }
    }

    //gets and applies date/time data to saveFile BUTTON!!!
    public void GetDateTimeSave(string saveFile)
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/" + saveFile + "/dateTime.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/" + saveFile + "/dateTime.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), saveFileDateTime);
            file.Close();
        }

        //go back and refactor this to take in last 3 characters in saveFile string and apply to the button text
        if (saveFile == "save001")
        {
            save001.text = saveFileDateTime.time;
            load001.text = saveFileDateTime.time;
        }

        if (saveFile == "save002")
        {
            save002.text = saveFileDateTime.time;
            load002.text = saveFileDateTime.time;
        }

        if (saveFile == "save003")
        {
            save003.text = saveFileDateTime.time;
            load003.text = saveFileDateTime.time;
        }



    }

}

[Serializable]
public class InventoryData
{
    public int[] itemIDs = null;
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
    public string time;
}
