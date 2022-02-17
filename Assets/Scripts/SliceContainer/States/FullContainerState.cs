using SemihCelek.SliceMerge.Slice;
using SemihCelek.SliceMerge.Utilities;

namespace SemihCelek.SliceMerge.SliceContainer.States
{
    public class FullContainerState : SliceContainerState
    {
        // private ISliceController _sliceController;

        private ContainerMergeManager _containerMergeManager;

        public FullContainerState(SliceContainer sliceContainer, ISliceController sliceController) : base(sliceContainer)
        {
            sliceContainer.SliceInsideContainer = sliceController;
        }

        public static event SliceGenerationAction OnGenerateSlice;

        public override void Start()
        {
            _containerMergeManager = new ContainerMergeManager(SliceContainer, SliceContainer.NextContainer,
                SliceContainer.PreviousContainer, SliceContainer.SliceInsideContainer);
            
            _containerMergeManager.CheckAvailableMerges();

            OnGenerateSlice?.Invoke();
        }

        public override void HandleMount(ISliceController sliceController)
        {
            // _sliceMerger.CombineIncomingSlice(sliceMovementController);

            OnGenerateSlice?.Invoke();
        }
    }
}