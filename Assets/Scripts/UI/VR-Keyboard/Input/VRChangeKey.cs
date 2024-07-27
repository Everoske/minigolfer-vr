using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigolf.UI.Keyboard.Input
{
    public class VRChangeKey : VRKey
    {
        [SerializeField]
        private GameObject parentGroup;
        [SerializeField]
        private GameObject targetGroup;

        protected override void HandleClicked()
        {
            parentGroup.SetActive(false);
            targetGroup.SetActive(true);
        }
    }
}
