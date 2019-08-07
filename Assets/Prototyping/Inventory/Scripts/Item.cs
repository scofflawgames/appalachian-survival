﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{

    //general info
    public string itemName;
    public Sprite itemIcon;
    public bool isWieldable;
    public int itemID;

    //stackable stuff
    public bool isStackable;
    public int maxStackAmount;

    //durability stuff
    public bool useDurability;
    public int maxDurabilty;
    public int currentDurability;

    //crafting stuff
    public bool isCraftable;
    public List<Item> crftItems = new List<Item>();
    public List<int> crftAmnt = new List<int>();
    public int makesHowMany;
    public int MinLevelToCraft;


}