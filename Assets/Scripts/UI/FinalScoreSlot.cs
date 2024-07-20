using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Minigolf.UI
{
    public class FinalScoreSlot : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI totalParText;
        [SerializeField]
        private TextMeshProUGUI finalScoreText;

        public void SetTotalParText(int totalPar)
        {
            totalParText.text = totalPar.ToString();
        }

        public void SetFinalScoreText(int finalScore)
        {
            finalScoreText.text = finalScore.ToString();
        }
    }
}
