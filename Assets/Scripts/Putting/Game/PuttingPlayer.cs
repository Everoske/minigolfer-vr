using Minigolf.Putting.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigolf.Putting.Game
{
    public class PuttingPlayer : MonoBehaviour
    {
        [SerializeField]
        private GolfBall playerGolfBall;
        [SerializeField]
        private GolfPutter playerPutter;

        public GolfBall PlayerGolfBall => playerGolfBall;
    }
}
