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
        
        private void Start()
        {
            ChangeState(new EmptyContainerState(this,_nextContainer, _previousContainer));
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