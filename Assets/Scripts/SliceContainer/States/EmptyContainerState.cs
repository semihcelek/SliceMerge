using SemihCelek.SliceMerge.Slice;

namespace SemihCelek.SliceMerge.SliceContainer.States
{
    public class EmptyContainerState : SliceContainerState
    {

        public EmptyContainerState(SliceContainer sliceContainer) : base(sliceContainer)
        {
        }

        public override void HandleMount(ISliceController sliceController)
        {
            sliceController.MergeToSliceContainer(SliceContainer); // after change to new state.
        }
    }
}