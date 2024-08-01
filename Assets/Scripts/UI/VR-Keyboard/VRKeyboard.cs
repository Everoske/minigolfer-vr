using Minigolf.UI.Keyboard.Input;
using UnityEngine;

namespace Minigolf.UI.Keyboard
{
    public class VRKeyboard : MonoBehaviour
    {
        [SerializeField]
        private VRPageGroup mainGroup;
        [SerializeField]
        private VRPageGroup alwaysActiveGroup;
        [SerializeField]
        private VRInputField targetField;

        private VRDeleteKey deleteKey;
        private VRKeyInput[] keys;

        private void Awake()
        {
            keys = GetComponentsInChildren<VRKeyInput>();
            deleteKey = GetComponentInChildren<VRDeleteKey>();
        }

        private void Start()
        {
            foreach (VRPageGroup group in GetComponentsInChildren<VRPageGroup>())
            {
                group.HideGroup();
            }

            mainGroup.ShowGroup();
            alwaysActiveGroup.ShowGroup();
        }

        private void OnEnable()
        {
            EnableKeyListeners();
            deleteKey.onDeleteClicked += ProcessDeleteInput;
        }

        private void OnDisable()
        {
            DisableKeyListeners();
            deleteKey.onDeleteClicked -= ProcessDeleteInput;
        }

        private void EnableKeyListeners()
        {
            foreach (VRKeyInput key in keys)
            {
                key.onKeyPressed += ProcessKeyInput;
            }
        }

        private void DisableKeyListeners()
        {
            foreach (VRKeyInput key in keys)
            {
                key.onKeyPressed -= ProcessKeyInput;
            }
        }

        private void ProcessKeyInput(string value)
        {
            Debug.Log($"{value} was pressed");
            targetField.Append(value[0]);

        }

        private void ProcessDeleteInput()
        {
            Debug.Log("Delete pressed");
            targetField.RemovePrevious();
        }
    }
}
