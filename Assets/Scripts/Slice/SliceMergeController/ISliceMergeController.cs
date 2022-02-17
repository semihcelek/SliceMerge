
namespace SemihCelek.SliceMerge.Slice.SliceMergeController
{
    public interface ISliceMergeController
    {
        void ValidateAndMergeWithTargetSlice(SliceContainer.SliceContainer targetSliceContainer);
        void MoveToSliceContainer(SliceContainer.SliceContainer sliceContainer);
    }
}