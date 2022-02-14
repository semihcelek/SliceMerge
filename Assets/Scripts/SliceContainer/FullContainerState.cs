using System.Collections;
using SemihCelek.SliceMerge.Slice;
using SemihCelek.SliceMerge.Utilities;
using UnityEngine;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public class FullContainerState : SliceContainerState
    {
        private SliceMerger _sliceMerger;
        private SliceContainerSettings _sliceContainerSettings;
        
        private SliceViewController _sliceViewController;
        private SliceScoreController _sliceScoreController;
        private SliceEffects _sliceEffects;

        public FullContainerState(SliceContainer sliceContainer, SliceMovementController sliceMovementController,
            SliceContainerSettings sliceContainerSettings) : base(sliceContainer)
        {
            _sliceContainerSettings = sliceContainerSettings;
            
            _sliceScoreController = sliceMovementController.gameObject.GetComponent<SliceScoreController>();
            _sliceViewController = sliceMovementController.gameObject.GetComponent<SliceViewController>();
            _sliceEffects = new SliceEffects(_sliceViewController);
            
            SliceContainer.SliceScoreController = _sliceScoreController;
            SliceContainer.SliceViewController = _sliceViewController;
            SliceContainer.SliceEffects = _sliceEffects;
        }

        public static event SliceGenerationAction OnGenerateSlice;

        public override void Start()
        {
            _sliceMerger = new SliceMerger(SliceContainer, _sliceScoreController, _sliceContainerSettings, _sliceEffects);

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