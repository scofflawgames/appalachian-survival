using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    //general info
    [Header("General Info")]
    public string itemName;
    public Sprite itemIcon;
    public bool isWieldable;
    public int itemID;

    //stackable stuff
    [Header("Stackable Info")]
    public bool isStackable;
    public int maxStackAmount;

    //durability stuff
    [Header("Durability Info")]
    public bool useDurability;
    public int maxDurabilty;
    public int currentDurability;

    //crafting stuff
    [Header("Crafting Info")]
    public bool isCraftable;
    public List<Item> crftItems = new List<Item>();
    public List<int> crftAmnt = new List<int>();
    public int makesHowMany;
    public int MinLevelToCraft;

    //consumable stuff
    [Header("Consumable Info")]
    public bool isConsumable;
    public bool isFood;
    public bool isWater;
    public int foodAmount;
    public int waterAmount;

}