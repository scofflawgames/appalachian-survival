﻿using UnityEngine;
using UnityEngine.UI;

namespace DeepWolf.HungerThirstSystem.Examples
{
    public class HungerThirstUI : MonoBehaviour
    {
        #region Fields
        /// <summary>
        /// Reference to the hunger and thirst script
        /// </summary>
        [SerializeField] private HungerThirst _hungerThirst;

        /*******************
         * UI - Fields
         *******************/
        /// <summary>
        /// Should the hunger UI be shown?
        /// </summary>
        [Header("[UI]")]
        [Tooltip("Should the hunger UI be shown?")] [SerializeField] private bool _showHungerUI = false;

        /// <summary>
        /// The hunger label that shows the current hunger.
        /// </summary>
        [Tooltip("The hunger label that shows the current hunger.")] [SerializeField] private Text _hungerLbl = null;

        /// <summary>
        /// The hunger bar that shows the current thirst.
        /// </summary>
        [Tooltip("The hunger bar that shows the current thirst.")] [SerializeField] private Slider _hungerBar = null;

        /// <summary>
        /// Should the thirst UI be shown?
        /// </summary>
        [Tooltip("Should the thirst UI be shown?")] [SerializeField] private bool _showThirstUI = false;

        /// <summary>
        /// The thirst label that shows the current thirst.
        /// </summary>
        [Tooltip("The thirst label that shows the current thirst.")] [SerializeField] private Text _thirstLbl = null;

        /// <summary>
        /// The thirst bar that shows the current thirst.
        /// </summary>
        [Tooltip("The thirst bar that shows the current thirst.")] [SerializeField] private Slider _thirstBar = null;
        #endregion

        /*******************
         * Properties
         *******************/
        #region Properties
        /// <summary>
        /// Gets or sets the show hunger UI state
        /// </summary>
        public bool ShowHungerUI
        {
            get { return _showHungerUI && HungerThirst.UseHunger; }
            set
            {
                _showHungerUI = value;
                CheckUI();
            }
        }

        /// <summary>
        /// Gets or sets the show thirst UI state
        /// </summary>
        public bool ShowThirstUI
        {
            get { return _showThirstUI && HungerThirst.UseThirst; }
            set
            {
                _showThirstUI = value;
                CheckUI();
            }
        }

        private HungerThirst HungerThirst
        {
            get
            {
                if (_hungerThirst == null)
                {
                    _hungerThirst = GetComponent<HungerThirst>();
                }
                return _hungerThirst;
            }
        }
        #endregion

        /*============================
         * Unity methods
         *============================*/
        #region Unity Methods
        private void Start()
        {
            CheckUI();
        }
        #endregion

        /*============================
         * UI methods
         *============================*/
        #region UI Methods
        /// <summary>
        /// Refreshes the hunger UI.
        /// </summary>
        public void RefreshHunger()
        {
            if (_hungerLbl != null)
            {
                _hungerLbl.text = Mathf.Round(_hungerThirst.Hunger).ToString();
            }

            if (_hungerBar != null)
            {
                _hungerBar.value = _hungerThirst.Hunger / _hungerThirst.HungerSettings.MaxValue;
            }
        }

        /// <summary>
        /// Refreshes the thirst UI.
        /// </summary>
        public void RefreshThirst()
        {
            if (_thirstLbl != null)
            {
                _thirstLbl.text = Mathf.Round(_hungerThirst.Thirst).ToString();
            }

            if (_thirstBar != null)
            {
                _thirstBar.value = _hungerThirst.Thirst / _hungerThirst.ThirstSettings.MaxValue;
            }
        }

        /// <summary>
        /// <para>Checks if the UI needs to be shown or not</para>
        /// <para>If the UI needs to be shown it will enable it, or if the UI needs to be hidden, it will hide it</para>
        /// </summary>
        private void CheckUI()
        {
            if (_thirstLbl != null)
            {
                _thirstLbl.gameObject.SetActive(ShowThirstUI);
                _thirstBar.gameObject.SetActive(ShowThirstUI);
            }
            else
            {
                if (ShowThirstUI)
                {
                    Debug.LogError("Thirst label is missing.", this);
                }
            }

            if (_hungerLbl != null)
            {
                _hungerLbl.gameObject.SetActive(ShowHungerUI);
                _hungerBar.gameObject.SetActive(ShowHungerUI);
            }
            else
            {
                if (ShowHungerUI)
                {
                    Debug.LogError("Hunger label is missing.", this);
                }
            }
        }

        #endregion
    }
}