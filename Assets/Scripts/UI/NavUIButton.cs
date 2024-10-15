using UnityEngine;
using UnityEngine.UI;

namespace Minigolf.UI
{
    [RequireComponent(typeof(Button))]
    public class NavUIButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject selectionGroup;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(DeactivateGroup);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(DeactivateGroup);
        }

        private void Start()
        {
            DeactivateGroup();
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