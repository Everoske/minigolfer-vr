using Minigolf.XR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Minigolf.UI
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class SwitchHandDropMenu : MonoBehaviour
    {
        [SerializeField]
        private HandMenuXR handMenuOrigin;

        private TMP_Dropdown dropdown;

        private void Awake()
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }

        private void OnEnable()
        {
            dropdown.onValueChanged.AddListener(OnSwapHands);
        }

        private void OnDisable()
        {
            dropdown.onValueChanged.RemoveAllListeners();
        }

        public void OnSwapHands(int val)
        {
            Handedness handedness;

            if (val == 0) handedness = Handedness.Left;
            else handedness = Handedness.Right;

            handMenuOrigin.SwitchActiveReference(handedness);
        }
    }
}
