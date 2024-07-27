using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigolf.UI.Keyboard.Input
{
    public abstract class VRKey : MonoBehaviour
    {
        private Button keyButton;

        protected virtual void Awake()
        {
            keyButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            keyButton.onClick.AddListener(HandleClicked);
        }

        private void OnDisable()
        {
            keyButton.onClick.RemoveListener(HandleClicked);
        }

        protected abstract void HandleClicked();
    }

}