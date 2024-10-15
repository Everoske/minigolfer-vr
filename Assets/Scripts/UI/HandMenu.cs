using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigolf.UI
{
    public class HandMenu : MonoBehaviour
    {
        [SerializeField]
        private NavUIButton[] navButtons;

        private int activeNavButton = -1;

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
            if (activeNavButton > -1) navButtons[activeNavButton].DeactivateGroup();

            activeNavButton = index;
            navButtons[activeNavButton].ActivateGroup();
        }

    }
}
