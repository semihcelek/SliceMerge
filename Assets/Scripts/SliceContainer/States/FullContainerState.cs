using SemihCelek.SliceMerge.Slice;
using SemihCelek.SliceMerge.Utilities;

namespace SemihCelek.SliceMerge.SliceContainer.States
{
    public class FullContainerState : SliceContainerState
    {
        private SliceController _sliceController;

        private SliceMergeManager _sliceMergeManager;

        public FullContainerState(SliceContainer sliceContainer, SliceController sliceController) : base(sliceContainer)
        {
            _sliceController = sliceController;
        }

        public static event SliceGenerationAction OnGenerateSlice;

        public override void Start()
        {
            _sliceMergeManager = new SliceMergeManager(SliceContainer, SliceContainer.NextContainer,
                SliceContainer.PreviousContainer, _sliceController);
            
            _sliceMergeManager.CheckAvailableMerges();

            OnGenerateSlice?.Invoke();
        }

        public override void HandleMount(ISliceController sliceController)
        {
            // _sliceMerger.CombineIncomingSlice(sliceMovementController);

            OnGenerateSlice?.Invoke();
        }
    }
}