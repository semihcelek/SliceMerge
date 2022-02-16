using System;
using SemihCelek.SliceMerge.SliceContainer;
using TMPro;
using UnityEngine;

namespace SemihCelek.SliceMerge.Slice
{
    public class SliceController : MonoBehaviour, ISliceController
    {
        [SerializeField]
        private Renderer _renderer;

        [SerializeField]
        private TrailRenderer _trailRenderer;

        [SerializeField]
        private ParticleSystem _particleSystem;
        
        [SerializeField]
        private TextMeshPro _scoreOnSliceObject;

        [SerializeField]
        private Transform _mountTargetPosition;

        [SerializeField]
        private SliceSettings _sliceSettings;


        private SliceEffects.SliceEffects _sliceEffects;
        private SliceScoreController.SliceScoreController _sliceScoreController;
        private SliceMovementController.SliceMovementController _sliceMovementController;
        private SliceMergeController.SliceMergeController _sliceMergeController;

        private void Awake()
        {
            _sliceEffects = new SliceEffects.SliceEffects(_trailRenderer, _renderer, _particleSystem);
            _sliceScoreController = new SliceScoreController.SliceScoreController(_scoreOnSliceObject, _sliceEffects);
            
            _sliceMovementController = new SliceMovementController.SliceMovementController(transform, _mountTargetPosition, this,_sliceSettings);
            _sliceMergeController = new SliceMergeController.SliceMergeController(_sliceScoreController, _sliceMovementController, this);
        }

        public int SliceScore { get; set; }

        public Transform SliceTransform { get; set; }

        public void MergeToTargetSlice(ISliceController targetSlice)
        {
           _sliceMergeController.ValidateAndMergeWithTargetSlice(targetSlice);
        }

        public void MergeToSliceContainer(SliceContainer.SliceContainer sliceContainer)
        {
            _sliceMergeController.MoveToSliceContainer(sliceContainer);

        }
    }
}