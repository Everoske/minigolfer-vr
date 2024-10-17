using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigolf.UI
{
    public class HandMenu : MonoBehaviour
    {
        [SerializeField]
        private NavUIButton[] navButtons;

        [SerializeField]
        private GameObject menuContainer;

        [SerializeField]
        private GameObject mainMenuContainer;

        [SerializeField]
        private GameObject subMenuContainer;

        [SerializeField]
        private GameObject[] subMenus;

        [SerializeField]
        private GameObject quitMenuContainer;

        [SerializeField]
        private Button confirmQuitButton;

        private int activePage = -1;

        private int activeSubmenu = -1;

        private void Start()
        {
            AssignIndices();
            HandleSelectionActivated(0);
        }

        private void OnEnable()
        {
           EnableOnSelectionActivated();
        }

        private void OnDisable()
        {
            DisableOnSelectionActivated();
        }

        public void OpenHandMenu()
        {
            gameObject.SetActive(true);
            HandleSelectionActivated(0);
        }

        public void OpenMenuContainer()
        {
            CloseQuitMenu();
            menuContainer.SetActive(true);
        }

        public void OpenSubmenu(int index)
        {
            if (index > subMenus.Length) return;

            mainMenuContainer.SetActive(false);
            subMenuContainer.SetActive(true);

            activeSubmenu = index;
            subMenus[activeSubmenu].SetActive(true);
        }

        public void CloseSubmenus()
        {
            subMenus[activeSubmenu].SetActive(false);
            subMenuContainer.SetActive(false);
            mainMenuContainer.SetActive(true);
        }    

        public void CloseHandMenu()
        {
            subMenuContainer.SetActive(false);
            CloseQuitMenu();
            menuContainer.SetActive(true);
            mainMenuContainer.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OpenQuitToTitleConfirmation()
        {
            menuContainer.SetActive(false);
            quitMenuContainer.SetActive(true);
            confirmQuitButton.onClick.AddListener(QuitToTitle);
        }

        public void OpenQuitAppConfirmation()
        {
            menuContainer.SetActive(false);
            quitMenuContainer.SetActive(true);
            confirmQuitButton.onClick.AddListener(QuitApp);
        }

        

        private void CloseQuitMenu()
        {
            if (!quitMenuContainer.activeInHierarchy) return;

            confirmQuitButton.onClick.RemoveAllListeners();
            quitMenuContainer.SetActive(false);
        }

        private void QuitToTitle()
        {
            Debug.Log("Quitting to title...");
        }

        private void QuitApp()
        {
            Debug.Log("Closing application...");
            Application.Quit();
        }

        private void AssignIndices()
        {
            for (int i = 0; i < navButtons.Length; i++)
            {
                navButtons[i].Index = i;
            }
        }


        private void EnableOnSelectionActivated()
        {
            for (int i = 0; i < navButtons.Length; i++)
            {
                navButtons[i].onSelectionActivated += HandleSelectionActivated;
            }
        }

        private void DisableOnSelectionActivated()
        {
            for (int i = 0; i < navButtons.Length; i++)
            {
                navButtons[i].onSelectionActivated -= HandleSelectionActivated;
            }
        }

        private void HandleSelectionActivated(int index)
        {
            if (index >= navButtons.Length) return;
            if (activePage > -1) navButtons[activePage].DeactivateGroup();

            activePage = index;
            navButtons[activePage].ActivateGroup();
        }

    }
}
