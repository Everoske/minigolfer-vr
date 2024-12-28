using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigolf.Scriptable
{
    /// <summary>
    /// Represents different types and colors of golf balls
    /// </summary>
    [CreateAssetMenu(fileName = "Golf Ball Template", menuName = "Golf Ball")]
    public class GolfBallTemplate : ScriptableObject
    {
        public string colorName;
        public Material material;
        public Sprite icon;
    }

}