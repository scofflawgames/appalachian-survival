using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;

    public GameObject playerObject;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            print("Player has made contact, teleport his ass to " + destination);
            playerObject.transform.position = destination.position;
        }
    }


}
