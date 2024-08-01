using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Minigolf.UI.Keyboard.Input
{
    public class VRKeyInput : VRKey
    {
        private string inputValue;

        public UnityAction<string> onKeyPressed;

        protected override void Awake()
        {
            base.Awake();
            inputValue = buttonText;
        }

        protected override void HandleClicked()
        {
            onKeyPressed?.Invoke(inputValue);
        }
    }
}
