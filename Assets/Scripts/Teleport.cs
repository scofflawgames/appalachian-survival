using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;

    [SerializeField]
    private GameManager gameManager = null;

    public GameObject playerObject;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Player has made contact, teleport his ass to " + destination);
            // Destroy(playerObject);
            //gameManager.SpawnPlayer(destination.position, destination.rotation);
            //playerObject = GameObject.FindGameObjectWithTag("Player");
            playerObject.transform.position = destination.position;
        }
    }


}
