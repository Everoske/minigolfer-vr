using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigolf.Putting.Interactable
{
    public class GolfPutter : MonoBehaviour
    {
        private bool isHeld = false;

        public bool IsHeld
        {
            get => isHeld;
            set => isHeld = value;
        }
    }
}
