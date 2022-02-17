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

        private ISliceController _sliceInsideContainer;
        
        public SliceContainer NextContainer
        {
            get => _nextContainer;
        }

        public SliceContainer PreviousContainer
        {
            get => _previousContainer;
        }

        public ISliceController SliceInsideContainer
        {
            get => _sliceInsideContainer;
            set => _sliceInsideContainer = value;
        }

        public Transform SliceContainerTransform { get; set; }
        
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