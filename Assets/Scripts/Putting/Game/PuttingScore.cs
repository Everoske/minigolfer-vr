using Minigolf.Putting.Hole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Minigolf.Putting.Game
{
    public class PuttingScore : MonoBehaviour
    {
        private ScoreCard[] cards;
        private int currentCardIndex = 0;

        public UnityAction<ScoreCard[]> onCreateCards;
        public UnityAction<int, ScoreCard> onUpdateScore;
        public UnityAction<int> onActivateCard;
        public UnityAction<int> onDeactivateCard;
        public UnityAction<int> onTallyFinalPar;
        public UnityAction<int> onUpdateFinalScore;

        private int totalPar = 0;
        private int finalScore = 0;
        
        public ScoreCard[] Cards => cards;

        public void CreateCards(PuttingArea[] puttingAreas)
        {
            cards = new ScoreCard[puttingAreas.Length];
            for (int i = 0; i < puttingAreas.Length; i++)
            {
                cards[i] = new ScoreCard();
                cards[i].Par = puttingAreas[i].Par;
                totalPar += cards[i].Par;
            }

            onCreateCards?.Invoke(cards);
            onTallyFinalPar?.Invoke(totalPar);
        }

        public void StartNewPuttingGame()
        {
            ClearScores();
            currentCardIndex = 0;
            onActivateCard?.Invoke(currentCardIndex);
        }

        public void NextCard()
        {
            currentCardIndex++;
            onActivateCard?.Invoke(currentCardIndex);
        }

        public void CompleteCurrentCard()
        {
            onDeactivateCard?.Invoke(currentCardIndex);
        }

        public void IncrementCardHits()
        {
            cards[currentCardIndex].Hits++;
            finalScore++;
            onUpdateScore?.Invoke(currentCardIndex, cards[currentCardIndex]);
            onUpdateFinalScore?.Invoke(finalScore);
        }

        public void ClearScores()
        {
            for (int i = 0;i < cards.Length;i++)
            {
                cards[i].Hits = 0;
                finalScore = 0;
                onUpdateScore?.Invoke(i, cards[i]);
                onUpdateFinalScore?.Invoke(finalScore);
            }
        }
    }

    public class ScoreCard
    {
        public int Par;
        public int Hits;

    }
}
