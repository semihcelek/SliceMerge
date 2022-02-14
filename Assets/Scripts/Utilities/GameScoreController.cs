using System;
using SemihCelek.SliceMerge.Slice;
using UnityEngine;

namespace SemihCelek.SliceMerge.Utilities
{
    public class GameScoreController : MonoBehaviour
    {
        private int _score;
        
        public int Score { get; set; }
        
        public static event GameScoreActions OnUiScoreUpdate;
        public static event SliceScoreActions OnUnlockSpawn;
        
        private void Awake()
        {
            SliceScoreController.OnUpdateScore += HandleScoreUpdate;
        }

        private void OnDestroy()
        {
            SliceScoreController.OnUpdateScore -= HandleScoreUpdate;
        }

        private void HandleScoreUpdate(int score)
        {
            _score += score;

            // Validate players achievement on merging an modify the spawning behaviour accordingly.
            ValidatePlayersMergingAchievement(score);
            OnUiScoreUpdate?.Invoke(_score);
        }

        private void ValidatePlayersMergingAchievement(int score)
        {
            switch (score)
            {
                case 16:
                    Debug.Log("Unlock 16");
                    break;
                case 32:
                    Debug.Log("Unlock 32");
                    break;
                case 64:
                    Debug.Log("Unlock 64");
                    break;
            }
        }
    }

    public delegate void GameScoreActions(int score);
}