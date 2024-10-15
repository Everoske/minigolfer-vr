using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Minigolf.UI
{
    [RequireComponent(typeof(Button))]
    public class NavUIButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject selectionGroup;

        public UnityAction<int> onSelectionActivated;

        private Button button;

        public int Index
        {
            get => index;
            set => index = value;
        }

        private int index = -1;

        private void Awake()
        {
            button = GetComponent<Button>();
            DeactivateGroup();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(HandleOnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(HandleOnClick);
        }

        private void HandleOnClick()
        {
            onSelectionActivated?.Invoke(index);
        }

        public void DeactivateGroup()
        {
            selectionGroup.SetActive(false);
            button.interactable = true;
        }

        public void ActivateGroup()
        {
            selectionGroup.SetActive(true);
            button.interactable = false;
        }
    }

}