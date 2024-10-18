using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Minigolf.UI
{
    public class HandMenuController : MonoBehaviour
    {
        [Header("UI Content Containers")]
        [SerializeField]
        private GameObject mainContainer;
        [SerializeField]
        private GameObject menusContainer;
        [SerializeField]
        private GameObject submenusContainer;
        [SerializeField]
        private GameObject quitContainer;

        [Header("Main Menu Navigation Buttons")]
        [SerializeField]
        private Button optionsButton;
        [SerializeField]
        private Button puttingButton;
        [SerializeField]
        private Button scoreButton;

        [Header("Main Menus")]
        [SerializeField]
        private GameObject optionsMenu;
        [SerializeField]
        private GameObject puttingMenu;
        [SerializeField]
        private GameObject scoreMenu;

        [Header("Submenu Buttons")]
        [SerializeField]
        private Button settingsSubmenuButton;
        [SerializeField]
        private Button changePutterButton;
        
        [Header("Submenus")]
        [SerializeField]
        private GameObject settingsSubmenu;
        [SerializeField]
        private GameObject changePutterSubmenu;

        [SerializeField]
        private Button closeSubmenuButton;
        [SerializeField]
        private Button closeQuitMenuButton;

        [Header("Quitting Game Buttons")]
        [SerializeField]
        private Button quitToTitleButton;
        [SerializeField]
        private Button quitAppButton;
        [SerializeField]
        private Button confirmQuitButton;
        

        private GameObject activeMenu;

        private Button activeButton;

        private GameObject activeSubmenu;


        private void Start()
        {
            SetInitialState();
        }

        private void OnEnable()
        {
            optionsButton.onClick.AddListener(delegate { MenuButtonClicked(optionsButton, optionsMenu); });
            puttingButton.onClick.AddListener(delegate { MenuButtonClicked(puttingButton, puttingMenu); });
            scoreButton.onClick.AddListener(delegate { MenuButtonClicked(scoreButton, scoreMenu); });

            changePutterButton.onClick.AddListener(delegate { SubmenuButtonClicked(changePutterSubmenu); });
            settingsSubmenuButton.onClick.AddListener(delegate { SubmenuButtonClicked(settingsSubmenu); });

            closeSubmenuButton.onClick.AddListener(CloseSubmenus);
            closeQuitMenuButton.onClick.AddListener(CloseQuitContainer);

            quitToTitleButton.onClick.AddListener(QuitToTitleClicked);
            quitAppButton.onClick.AddListener(QuitAppClicked);
        }

        private void OnDisable()
        {
            optionsButton.onClick.RemoveAllListeners();
            puttingButton.onClick.RemoveAllListeners();
            scoreButton.onClick.RemoveAllListeners();

            changePutterButton.onClick.RemoveAllListeners();
            settingsSubmenuButton.onClick.RemoveAllListeners();

            closeSubmenuButton.onClick.RemoveListener(CloseSubmenus);
            closeQuitMenuButton.onClick.RemoveListener(CloseQuitContainer);

            quitToTitleButton.onClick.RemoveAllListeners();
            quitAppButton.onClick.RemoveAllListeners();
            confirmQuitButton.onClick.RemoveAllListeners();
        }

        public void OpenHandMenu()
        {
            SetInitialState();
            gameObject.SetActive(true);
        }

        public void CloseHandMenu()
        {
            gameObject.SetActive(false);
        }

        private void SetInitialState()
        {
            puttingButton.interactable = true;
            scoreButton.interactable = true;
            puttingMenu.SetActive(false);
            scoreMenu.SetActive(false);

            CloseQuitContainer();
            CloseSubmenus();

            activeButton = optionsButton;
            activeMenu = optionsMenu;
            OpenActiveMenu();
        }

        private void MenuButtonClicked(Button button, GameObject menu)
        {
            CloseActiveMenu();
            activeButton = button;
            activeMenu = menu;
            OpenActiveMenu();
        }

        private void SubmenuButtonClicked(GameObject submenu)
        {
            menusContainer.SetActive(false);
            submenusContainer.SetActive(true);
            submenu.SetActive(true);
            activeSubmenu = submenu;
        }

        private void QuitToTitleClicked()
        {
            confirmQuitButton.onClick.AddListener(QuitToTitle);
            OpenQuitContainer();
        }

        private void QuitAppClicked()
        {
            confirmQuitButton.onClick.AddListener(QuitApplication);
            OpenQuitContainer();
        }

        private void OpenQuitContainer()
        {
            mainContainer.SetActive(false);
            quitContainer.SetActive(true);
        }

        private void QuitToTitle()
        {
            Debug.Log("Quitting to Title...");
        }

        private void QuitApplication()
        {
            Debug.Log("Quitting Application...");
        }

        private void CloseSubmenus()
        {
            if (activeSubmenu != null) activeSubmenu.SetActive(false);
            submenusContainer.SetActive(false);
            menusContainer.SetActive(true);
        }

        private void CloseQuitContainer()
        {
            quitContainer.SetActive(false);
            confirmQuitButton.onClick.RemoveAllListeners();
            mainContainer.SetActive(true);
        }

        private void CloseActiveMenu()
        {
            if (activeMenu == null) return;
            if (activeButton == null) return;

            activeMenu.SetActive(false);
            activeButton.interactable = true;
        }

        private void OpenActiveMenu()
        {
            if (activeMenu == null) return;
            if (activeButton == null) return;

            activeButton.interactable = false;
            activeMenu.SetActive(true);
        }
    }
}
