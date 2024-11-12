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

        /// <summary>
        /// Creates Score Cards to track the scores of each putting hole
        /// </summary>
        /// <param name="puttingAreas">Array of putting holes to make cards for</param>
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

        /// <summary>
        /// Resets score for a new putting game
        /// </summary>
        public void StartNewPuttingGame()
        {
            ClearScores();
            currentCardIndex = 0;
            onActivateCard?.Invoke(currentCardIndex);
        }

        /// <summary>
        /// Start tracking the score of the next hole
        /// </summary>
        public void NextCard()
        {
            currentCardIndex++;
            onActivateCard?.Invoke(currentCardIndex);
        }

        /// <summary>
        /// Complete the score record of the current hole
        /// </summary>
        public void CompleteCurrentCard()
        {
            onDeactivateCard?.Invoke(currentCardIndex);
        }


        /// <summary>
        /// Increment the number of hits for the current hole
        /// </summary>
        public void IncrementCardHits()
        {
            cards[currentCardIndex].Hits++;
            finalScore++;
            onUpdateScore?.Invoke(currentCardIndex, cards[currentCardIndex]);
            onUpdateFinalScore?.Invoke(finalScore);
        }

        /// <summary>
        /// Clear all previous game scores
        /// </summary>
        public void ClearScores()
        {
            CompleteCurrentCard();

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
