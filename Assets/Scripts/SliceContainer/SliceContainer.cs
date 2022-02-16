using System;
using SemihCelek.SliceMerge.Slice;
using SemihCelek.SliceMerge.SliceContainer.States;
using UnityEngine;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public class SliceContainer : SliceContainerStateMachine
    {
        [SerializeField]
        private SliceContainer _nextContainer;

        [SerializeField]
        private SliceContainer _previousContainer;

        private SliceController _sliceInsideContainer;
        
        public SliceContainer NextContainer
        {
            get => _nextContainer;
        }

        public SliceContainer PreviousContainer
        {
            get => _previousContainer;
        }

        public SliceController SliceInsideContainer
        {
            get => _sliceInsideContainer;
            set => _sliceInsideContainer = value;
        }

        private void Start()
        {
            ChangeState(new EmptyContainerState(this));
        }

        private void OnTriggerEnter(Collider other)
        {
            SliceContainerState.HandleTrigger();
        }

        public void HandleMount(ISliceController sliceController)
        {
            SliceContainerState.HandleMount(sliceController);
        }
    }
}