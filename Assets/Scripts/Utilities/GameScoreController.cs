using System;
using System.Collections.Generic;
using SemihCelek.SliceMerge.Slice;
using SemihCelek.SliceMerge.Slice.SliceScoreController;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SemihCelek.SliceMerge.Utilities
{
    public class GameScoreController : MonoBehaviour
    {
        private int _score;
        
        private List<int> _initialSliceScorePool;
        
        public static event GameScoreUpdateAction OnUiScoreUpdate;

        private void Awake()
        {
            _initialSliceScorePool = new List<int>();
            SetSliceStartingScore();
            
            SliceScoreController.OnUpdateScore += HandleScoreUpdate;
            SliceScoreController.OnGetRandomizedScore += GetRandomizedScore;
        }

        private void OnDestroy()
        {
            SliceScoreController.OnUpdateScore -= HandleScoreUpdate;
            SliceScoreController.OnGetRandomizedScore -= GetRandomizedScore;
        }

        private void HandleScoreUpdate(int score)
        {
            _score += score;
            ValidatePlayersMergingAchievement(score);
            OnUiScoreUpdate?.Invoke(_score);
        }

        private void ValidatePlayersMergingAchievement(int score)
        {
            switch (score)
            {
                case 16:
                    UnlockScoreInPool(4);
                    break;
                case 32:
                    UnlockScoreInPool(8);
                    break;
                case 64:
                    UnlockScoreInPool(16);
                    break;
                case 128:
                    UnlockScoreInPool(32);
                    break;
            }
        }

        private void SetSliceStartingScore()
        {
            var initialScore = 2;
            _initialSliceScorePool.Add(initialScore);
        }

        private void UnlockScoreInPool(int scoreToUnlock)
        {
            var isScoreIsUnlocked = _initialSliceScorePool.Contains(scoreToUnlock);
            if (isScoreIsUnlocked) return;

            _initialSliceScorePool.Add(scoreToUnlock);
        }

        private int GetRandomizedScore()
        {
            var randomScoreIndex = Random.Range(0, _initialSliceScorePool.Count);
            return _initialSliceScorePool[randomScoreIndex];
        }
    }

    public delegate void GameScoreUpdateAction(int score);

    public delegate int GameScoreGetRandomScoreAction();
}