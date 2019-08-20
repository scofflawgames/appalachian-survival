using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DeepWolf.HungerThirstSystem;
using UnityEngine.UI;
using System;

public class PlayerManager : MonoBehaviour
    {
        private HungerThirst hungerThirst;

        [Header("Hunger, Thirst and Health")]
        [SerializeField] private TextMeshProUGUI hungerText = null;
        public TextMeshProUGUI thirstText;
        public TextMeshProUGUI healthText;

        public static float maxPlayerHealth = 100;
        public static float curPlayerHealth = 100;

        public bool isDehydrating = false;
        public bool isStarving = false;

        [Space]

        [Header("Player FPS Animations")]
        public Animator playerFPSArms;

        private ToolBelt toolBelt;


        private void Awake()
        {
            hungerThirst = FindObjectOfType<HungerThirst>();

            RefreshPlayerHealth();
            RefreshHunger();
            RefreshThirst();

            //fps animations shit
            toolBelt = FindObjectOfType<ToolBelt>();
        }

        private void FixedUpdate()
        {
            //if player is dehydrating, take damage
            if (isDehydrating)
            {
                if (curPlayerHealth > 0)
                {
                    curPlayerHealth -= 0.5f * Time.deltaTime;
                    RefreshPlayerHealth();
                }
            }

            if (isStarving)
            {
                if (curPlayerHealth > 0)
                {
                    curPlayerHealth -= 0.5f * Time.deltaTime;
                    RefreshPlayerHealth();
                }
            }

        }

    private void Update()
    {
        if (!PauseMenu.isPaused && toolBelt.currentItemID == 1 && Input.GetMouseButton(0))
        {
            playerFPSArms.Play("AxeSwing");
        }
    }

    public void RefreshHunger()
        {
            hungerText.text = string.Format("Hunger: " + hungerThirst.Hunger);
        }

        public void RefreshThirst()
        {
            thirstText.text = string.Format("Thirst: " + hungerThirst.Thirst);
        }

        public void RefreshPlayerHealth()
        {
            int displayPlayerHealth = (int)Math.Round(curPlayerHealth);
            healthText.text = string.Format("Health: " + displayPlayerHealth);
        }

        public void DehydrationStart()
        {
            isDehydrating = true;
            print("You Need to Find Water!!");
        }

        public void DehydrationStop()
        {
            isDehydrating = false;
        }

        public void StarvingStart()
        {
            isStarving = true;
            print("You Need to Find Food!!");
        }

        public void StarvingStop()
        {
            isStarving = false;
        }
    
}
