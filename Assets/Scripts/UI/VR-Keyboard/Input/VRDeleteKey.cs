using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Minigolf.UI.Keyboard.Input
{
    public class VRDeleteKey : VRKey
    {
        public UnityAction onDeleteClicked;

        protected override void HandleClicked()
        {
            onDeleteClicked?.Invoke();
        }
    }
}
