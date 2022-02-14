using System;
using SemihCelek.SliceMerge.Slice;
using UnityEngine;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public class SliceContainer : SliceContainerStateMachine
    {
        [SerializeField]
        private SliceContainer _nextContainer;

        [SerializeField]
        private SliceContainer _previousContainer;

        [SerializeField]
        private SliceContainerSettings _sliceContainerSettings;

        private SliceScoreController _sliceScoreController;
        private SliceViewController _sliceViewController;
        private SliceEffects _sliceEffects;
        
        public SliceContainer NextContainer
        {
            get => _nextContainer;
        }

        public SliceContainer PreviousContainer
        {
            get => _previousContainer;
        }

        public SliceScoreController SliceScoreController
        {
            get => _sliceScoreController;
            set => _sliceScoreController = value;
        }

        public SliceViewController SliceViewController
        {
            get => _sliceViewController;
            set => _sliceViewController = value;
        }

        public SliceEffects SliceEffects
        {
            get => _sliceEffects;
            set => _sliceEffects = value;
        }

        private void Start()
        {
            ChangeState(new EmptyContainerState(this, _sliceContainerSettings));
        }

        private void OnTriggerEnter(Collider other)
        {
            SliceContainerState.HandleTrigger();
        }

        public void HandleMount(SliceMovementController sliceMovementController)
        {
            SliceContainerState.HandleMount(sliceMovementController);
        }
    }
}