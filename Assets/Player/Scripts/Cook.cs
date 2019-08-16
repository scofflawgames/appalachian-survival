using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cook : MonoBehaviour
{
    [Header("Public References")]
    [SerializeField]
    public GameObject pickUpPrompt = null;


    private TextMeshProUGUI pickUpPromptText;
    private Camera cam;
    private ItemDatabase itemDatabase;
    private Inventory inventory;
    private ToolBelt toolBelt;

    
    private void Awake()
    {
        toolBelt = FindObjectOfType<ToolBelt>();
        pickUpPrompt = GameObject.FindGameObjectWithTag("PickupPrompt");
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        pickUpPromptText = pickUpPrompt.GetComponent<TextMeshProUGUI>();
        //pickUpPrompt.SetActive(false);        
        inventory = FindObjectOfType<Inventory>();
        itemDatabase = FindObjectOfType<ItemDatabase>();
    }

    // Update is called once per frame
    private void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2))
        {
            OvenType ovenType = hit.collider.GetComponent<OvenType>();

            if (ovenType != null)
            {
                print("You can cook shit here!!");
            }
        }
    }
}
