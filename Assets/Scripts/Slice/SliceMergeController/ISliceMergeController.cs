namespace SemihCelek.SliceMerge.Slice.SliceMergeController
{
    public interface ISliceMergeController
    {
        void ValidateAndMergeWithTargetSlice(ISliceController targetSlice);
    }
}