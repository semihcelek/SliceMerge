using System;
using UnityEngine;

namespace SemihCelek.SliceMerge.Slice
{
    public class SliceScoreView : MonoBehaviour
    {
        [SerializeField]
        private TextMesh _scoreOnSliceObject;

        [SerializeField]
        private Renderer _sliceRenderer;

        private SliceScoreController _sliceScoreController;

        private void Start()
        {
            _sliceScoreController = new SliceScoreController(0);
            UpdateScoreOnSliceObject(2,Color.red);
        }

        public void UpdateScoreOnSliceObject(int score, Color color)
        {
            _sliceScoreController.SliceScore = score;
            _scoreOnSliceObject.text = score.ToString();
            _sliceRenderer.material.color = color;
            // Ui.Updater.OnUpdateEvent(score)
        }
        // Color of the slice can be decided throughout switch statements
        
        public int SliceScore
        {
            get => _sliceScoreController.SliceScore;
            set => _sliceScoreController.SliceScore = value;
        }
        
    }
        
}