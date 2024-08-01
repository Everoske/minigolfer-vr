using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigolf.UI.Keyboard
{
    public class VRPageGroup : MonoBehaviour
    {
        [SerializeField]
        private VRKeyPage[] pages;

        [SerializeField]
        private VRKeyPage alwaysActivePage;

        public void HideGroup()
        {
            foreach (VRKeyPage page in pages)
            {
                page.ClosePage();
            }
            alwaysActivePage.ClosePage();
        }

        public void ShowGroup()
        {
            if (pages.Length > 0)
            {
                pages[0].OpenPage();
            }
            alwaysActivePage.OpenPage();
        }
    }
}
