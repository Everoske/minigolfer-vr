using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigolf.UI.Keyboard.Input
{
    public abstract class VRKey : MonoBehaviour
    {
        [SerializeField]
        protected string buttonText = string.Empty;

        private Button keyButton;
        private TextMeshProUGUI textField;

        protected virtual void Awake()
        {
            keyButton = GetComponentInChildren<Button>();
            textField = GetComponentInChildren<TextMeshProUGUI>();
            textField.text = buttonText;
        }

        private void OnEnable()
        {
            keyButton.onClick.AddListener(HandleClicked);
        }

        private void OnDisable()
        {
            keyButton.onClick.RemoveListener(HandleClicked);
        }

        private void DeactivateButton()
        {
            keyButton.onClick.RemoveListener(HandleClicked);
            keyButton.gameObject.SetActive(false);
        }

        private void ActivateButton()
        {
            keyButton.gameObject.SetActive(true);
            keyButton.onClick.AddListener(HandleClicked);
        }

        public virtual void HideButton()
        {
            DeactivateButton();
        }

        public virtual void ShowButton()
        {
            ActivateButton();
        }

        protected abstract void HandleClicked();
    }

}