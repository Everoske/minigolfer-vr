using Minigolf.UI.Keyboard.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigolf.UI.Keyboard
{
    public class VRKeyPage : MonoBehaviour
    {
        [SerializeField]
        private VRKey[] keys;


        public void ClosePage()
        {
            HideKeys();
        }

        public void OpenPage()
        {
            ShowKeys();
        }

        private void HideKeys()
        {
            foreach (VRKey key in keys)
            {
                key.HideButton();
            }
        }

        private void ShowKeys()
        {
            foreach (VRKey key in keys)
            {
                key.ShowButton();
            }
        }
    }
}