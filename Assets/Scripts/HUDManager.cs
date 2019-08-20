using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public GameObject hudDisplay;

    private void Awake()
    {
        hudDisplay.SetActive(true);
    }


}
