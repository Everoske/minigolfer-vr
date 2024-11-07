using Minigolf.Putting.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Minigolf.UI
{
    public class ScoreUIDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI totalParText;
        [SerializeField]
        private TextMeshProUGUI finalScoreText;

        // Consider making this fully private and using FindByType since only one puttingScore should exist per minigolf course
        [SerializeField]
        private PuttingScore puttingScore;

        [SerializeField]
        private ScoreSlot slotPrefab;

        [SerializeField]
        private GameObject slotContainer;

        private ScoreSlot[] scoreSlots;

        private void OnEnable()
        {
            puttingScore.onCreateCards += CreateScoreSlots;
            puttingScore.onActivateCard += ActivateSlot;
            puttingScore.onDeactivateCard += DeactivateSlot;
            puttingScore.onUpdateScore += UpdateScoreSlot;
            puttingScore.onTallyFinalPar += RecordTotalPar;
            puttingScore.onUpdateFinalScore += UpdateTotalScore;
        }

        private void OnDisable()
        {
            puttingScore.onCreateCards -= CreateScoreSlots;
            puttingScore.onActivateCard -= ActivateSlot;
            puttingScore.onDeactivateCard -= DeactivateSlot;
            puttingScore.onUpdateScore -= UpdateScoreSlot;
            puttingScore.onTallyFinalPar -= RecordTotalPar;
            puttingScore.onUpdateFinalScore -= UpdateTotalScore;
        }

        private void UpdateTotalScore(int totalScore)
        {
            finalScoreText.text = totalScore.ToString();
        }

        private void RecordTotalPar(int totalPar)
        {
            totalParText.text = totalPar.ToString();
        }

        private void UpdateScoreSlot(int index, ScoreCard scoreCard)
        {
            if (index > scoreSlots.Length) return;

            scoreSlots[index].SetHitsText(scoreCard.Hits);
        }

        private void CreateScoreSlots(ScoreCard[] cards)
        {
            scoreSlots = new ScoreSlot[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                scoreSlots[i] = Instantiate(slotPrefab, slotContainer.transform);
                scoreSlots[i].SetupSlot(i + 1, cards[i].Par);
            }
        }

        private void DeactivateSlot(int active)
        {
            scoreSlots[active].SetActiveSlot(false);
        }

        private void ActivateSlot(int next)
        {
            scoreSlots[next].SetActiveSlot(true);
        }
    }
}
