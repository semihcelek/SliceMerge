using System;
using SemihCelek.SliceMerge.Slice;
using UnityEngine;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public class SliceContainer : SliceContainerStateMachine
    {
        [SerializeField]
        public SliceContainer _nextContainer;

        [SerializeField]
        public SliceContainer _previousContainer;

        private void Start()
        {
            ChangeState(new EmptyContainerState(this));
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