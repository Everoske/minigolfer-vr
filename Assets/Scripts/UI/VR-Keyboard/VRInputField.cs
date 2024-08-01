using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Minigolf.UI.Keyboard
{
    public class VRInputField : MonoBehaviour
    {
        [Min(5)]
        [SerializeField]
        private int MAX_CHARACTERS = 25;

        private TextMeshProUGUI textField;

        [SerializeField]
        private int positionalIndex = 0;
        private char[] characters;

        [SerializeField]
        private int tail = 1;

        private void Awake()
        {
            characters = new char[MAX_CHARACTERS];
            textField = GetComponentInChildren<TextMeshProUGUI>();
            textField.text = "";
        }

        public void Append(char c)
        {
            if (positionalIndex < MAX_CHARACTERS && tail < MAX_CHARACTERS)
            {
                characters[positionalIndex] = c;
                positionalIndex++;
                tail++;
                textField.text = GetValue();
            }
        }

        public void RemovePrevious()
        {
            if (positionalIndex - 1 >= 0)
            {
                int newPosition = positionalIndex - 1;

                for (int i = newPosition; i < tail - 1; i++)
                {
                    characters[i] = characters[i + 1];
                }

                positionalIndex = newPosition;
                tail--;
                textField.text = GetValue();
            }
        }

        public string GetValue()
        {
            string value = "";

            for (int i = 0; i < tail; i++)
            {
                value += characters[i];
            }

            return value;
        }
    }
}
