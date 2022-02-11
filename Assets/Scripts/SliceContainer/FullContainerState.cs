using System.Collections;
using SemihCelek.SliceMerge.Slice;
using SemihCelek.SliceMerge.Utilities;
using UnityEngine;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public class FullContainerState : SliceContainerState
    {
        private SliceMerger _sliceMerger;

        public FullContainerState(SliceContainer sliceContainer) : base(sliceContainer)
        {
        }

        public static event SliceGenerationAction OnGenerateSlice;

        public override void Start()
        {
            _sliceMerger = new SliceMerger(SliceContainer);

            _sliceMerger.CheckAvailableMerges(SliceContainer);

            OnGenerateSlice?.Invoke();
        }

        public override void HandleMount(SliceMovementController sliceMovementController)
        {
            _sliceMerger.CombineIncomingSlice(sliceMovementController);

            OnGenerateSlice?.Invoke();
        }
    }
}