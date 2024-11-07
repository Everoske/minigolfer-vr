using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigolf.UI
{
    public class ScoreSlot : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI holeText;
        [SerializeField]
        private TextMeshProUGUI parText;
        [SerializeField]
        private TextMeshProUGUI hitsText;

        [SerializeField]
        private Image backgroundImage;

        [SerializeField]
        private Color backColorNormal = Color.clear;
        [SerializeField]
        private Color backColorActive;

        public void SetupSlot(int hole, int par)
        {
            holeText.text = hole.ToString();
            parText.text = par.ToString();
            hitsText.text = "0";
        }

        public void SetHitsText(int hits)
        {
            hitsText.text = hits.ToString();
        }

        public void SetActiveSlot(bool active)
        {
            backgroundImage.color = active ? backColorActive : backColorNormal;
        }
    }
}
