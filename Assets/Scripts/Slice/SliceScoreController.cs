using System;
using SemihCelek.SliceMerge.Utilities;
using UnityEngine;

namespace SemihCelek.SliceMerge.Slice
{
    public class SliceScoreController : MonoBehaviour
    {
        [SerializeField]
        private TextMesh _scoreOnSliceObject;

        [SerializeField]
        private Renderer _sliceRenderer;

        private int _sliceScore;
        
        public static event GameScoreActions OnUpdateScore;


        private void Start()
        {
            // _sliceScore = 2;
            UpdateScoreOnSliceObject(2,Color.red);
        }

        public void UpdateScoreOnSliceObject(int score, Color color)
        {
            _sliceScore = score;
            _scoreOnSliceObject.text = score.ToString();
            _sliceRenderer.material.color = color;
            
            OnUpdateScore?.Invoke(score);
            // Ui.Updater.OnUpdateEvent(score)
        }
        // Color of the slice can be decided throughout switch statements
        
        public int SliceScore
        {
            get => _sliceScore;
            set => _sliceScore = value;
        }
        
    }
        
}