using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SaveInventory : ScriptableObject
{
    public int[] itemIDs;
    public int[] slotIDs;
    public int[] itemAMTs;
}
