using Minigolf.Putting.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigolf.UI
{
    public class ScoreUIDisplay : MonoBehaviour
    {
        [SerializeField]
        private ScoreSlot[] scoreSlots;

        [SerializeField]
        private FinalScoreSlot finalScoreSlot;

        [SerializeField]
        private PuttingScore puttingScore;

        private void OnEnable()
        {
            puttingScore.onCreateCard += CreateScoreSlot;
            puttingScore.onUpdateScore += UpdateScoreSlot;
            puttingScore.onTallyFinalPar += RecordTotalPar;
            puttingScore.onUpdateFinalScore += UpdateTotalScore;
        }

        private void OnDisable()
        {
            puttingScore.onCreateCard -= CreateScoreSlot;
            puttingScore.onUpdateScore -= UpdateScoreSlot;
            puttingScore.onTallyFinalPar -= RecordTotalPar;
            puttingScore.onUpdateFinalScore -= UpdateTotalScore;
        }

        private void UpdateTotalScore(int totalScore)
        {
            finalScoreSlot.SetFinalScoreText(totalScore);
        }

        private void RecordTotalPar(int totalPar)
        {
            finalScoreSlot.SetTotalParText(totalPar);
        }

        private void CreateScoreSlot(int index, ScoreCard scoreCard)
        {
            if (index > scoreSlots.Length) return;

            scoreSlots[index].SetupSlot(index + 1, scoreCard.Par);
        }

        private void UpdateScoreSlot(int index, ScoreCard scoreCard)
        {
            if (index > scoreSlots.Length) return;

            scoreSlots[index].SetHitsText(scoreCard.Hits);
            scoreSlots[index].SetFinalText(scoreCard.FinalScore);
        }
    }
}
