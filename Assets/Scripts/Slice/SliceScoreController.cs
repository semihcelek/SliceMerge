using System;
using SemihCelek.SliceMerge.Utilities;
using UnityEngine;

namespace SemihCelek.SliceMerge.Slice
{
    public class SliceScoreController : MonoBehaviour
    {
        [SerializeField]
        private TextMesh _scoreOnSliceObject;

        private int _sliceScore;

        public static event GameScoreActions OnUpdateScore;


        private void Start()
        {
            UpdateScoreOnSliceObject(2);
        }

        public void UpdateScoreOnSliceObject(int score)
        {
            _sliceScore = score;
            _scoreOnSliceObject.text = score.ToString();

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