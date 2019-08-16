using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Public Shit")]
    public GameObject playerObject;
    public Vector3 playerPos;
    public Quaternion playerRot;

    [Header("Static References")]
    public static bool devConsoleActive = false;

    void Awake()
    {
        //spawn in player
        //Instantiate(playerObject, playerPos, playerRot);
        //GameObject thePlayer = Instantiate(playerObject, playerPos, playerRot);
    }


    void Update()
    {

    }

    public void SpawnPlayer(Vector3 playerPosition, Quaternion playerRotation)
    {
        GameObject thePlayer = Instantiate(playerObject, playerPosition, playerRotation);
    }



}
