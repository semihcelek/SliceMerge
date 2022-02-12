using System;
using UnityEngine;

namespace SemihCelek.SliceMerge.Utilities
{
    public class UiScoreUpdater : MonoBehaviour
    {
        [SerializeField]
        private TextMesh _scoreText;
        
        private void Awake()
        {
            GameScoreController.OnUiScoreUpdate += UpdateUi;
        }

        private void OnDestroy()
        {
            GameScoreController.OnUiScoreUpdate -= UpdateUi;
        }

        private void UpdateUi(int score)
        {
            _scoreText.text = score.ToString();
            Debug.Log(score);
        }
    }
}