using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigolf.UI.Keyboard.Input
{
    public class VRGroupChange : VRKey
    {
        [SerializeField]
        private VRPageGroup parentGroup;

        [SerializeField]
        private VRPageGroup targetGroup;

        protected override void HandleClicked()
        {
            parentGroup.HideGroup();
            targetGroup.ShowGroup();
        }
    }
}
