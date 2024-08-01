using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigolf.UI.Keyboard.Input
{
    public class VRChangeKey : VRKey
    {
        [SerializeField]
        private VRKeyPage parentPage;
        [SerializeField]
        private VRKeyPage targetPage;



        protected override void HandleClicked()
        {
            parentPage.ClosePage();
            targetPage.OpenPage();
        }
    }
}
