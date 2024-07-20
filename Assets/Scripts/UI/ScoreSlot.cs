using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        private TextMeshProUGUI finalText;

        public void SetupSlot(int hole, int par)
        {
            holeText.text = hole.ToString();
            parText.text = par.ToString();
        }

        public void SetHitsText(int hits)
        {
            hitsText.text = hits.ToString();
        }

        public void SetFinalText(int final)
        {
            finalText.text = final.ToString();
        }
    }
}
