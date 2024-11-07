using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Minigolf.UI
{
    [RequireComponent(typeof(CanvasGroup))]
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

        [SerializeField]
        private float fadeTime = 0.5f;

        private GameObject activeMenu;

        private Button activeButton;

        private GameObject activeSubmenu;

        private CanvasGroup alphaController;

        private bool isOpen;
        private bool shouldFadeIn = false;
        private bool shouldFadeOut = false;

        private float fadeCounter = 0.0f;

        private void Awake()
        {
            alphaController = GetComponent<CanvasGroup>();
        }


        private void Start()
        {
            isOpen = gameObject.activeInHierarchy;
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

        private void Update()
        {
            HandleFade();
        }

        private void HandleFade()
        {
            if (!shouldFadeIn && !shouldFadeOut) return;

            fadeCounter += Time.deltaTime;

            if (shouldFadeIn)
            {
                FadeIn();
            }
            else
            {
                FadeOut();
            }
        }

        private void FadeIn()
        {
            if (fadeCounter < fadeTime)
            {
                alphaController.alpha = Mathf.Lerp(0.0f, 1.0f, fadeCounter / fadeTime);
                return;
            } 

            alphaController.alpha = 1.0f;
            shouldFadeIn = false;
        }


        private void FadeOut()
        {
            if (fadeCounter < fadeTime)
            {
                alphaController.alpha = Mathf.Lerp(1.0f, 0.0f, fadeCounter / fadeTime);
                return;
            }

            alphaController.alpha = 0.0f;
            shouldFadeOut = false;
            gameObject.SetActive(false);
            CloseQuitContainer();
        }

        public void OpenHandMenu()
        {
            if (isOpen) return;

            gameObject.SetActive(true);
            shouldFadeOut = false;
            isOpen = true;

            if (alphaController.alpha > 0.0f)
            {
                fadeCounter = (1 - alphaController.alpha / 1) * fadeTime;
            }
            else
            {
                fadeCounter = 0;
            }

            shouldFadeIn = true;
        }

        public void CloseHandMenu()
        {
            if (!isOpen) return;

            shouldFadeIn = false;
            isOpen = false;

            if (alphaController.alpha < 1.0f)
            {
                fadeCounter = (alphaController.alpha / 1) * fadeTime;
            }
            else
            {
                fadeCounter = 0;
            }

            shouldFadeOut = true;
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
