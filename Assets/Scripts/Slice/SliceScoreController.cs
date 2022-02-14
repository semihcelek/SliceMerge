using System;
using SemihCelek.SliceMerge.Utilities;
using TMPro;
using UnityEngine;

namespace SemihCelek.SliceMerge.Slice
{
    public class SliceScoreController : MonoBehaviour
    {
        // [SerializeField]
        // private TextMesh _scoreOnSliceObject;

        [SerializeField]
        private TextMeshPro _scoreOnSliceObject;

        [SerializeField]
        private SliceViewController _sliceViewController;

        private SliceEffects _sliceEffects;
        
        private int _sliceScore;

        public static event GameScoreUpdateAction OnUpdateScore;
        public static event GameScoreGetRandomScoreAction OnGetRandomizedScore;


        private void Start()
        {
            SetInitialScore();
            _sliceEffects = new SliceEffects(_sliceViewController);
            _sliceEffects.UpdateSliceColor(_sliceScore);
        }

        private void SetInitialScore()
        {
            if (OnGetRandomizedScore != null) _sliceScore = OnGetRandomizedScore.Invoke();
            _scoreOnSliceObject.text = _sliceScore.ToString();
        }

        public void UpdateScoreOnSliceObject(int score)
        {
            _sliceScore = score;
            _scoreOnSliceObject.text = _sliceScore.ToString();

            OnUpdateScore?.Invoke(score);
        }

        public int SliceScore
        {
            get => _sliceScore;
            set => _sliceScore = value;
        }
    }

    public delegate void SliceScoreActions(int score);
}