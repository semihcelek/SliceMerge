using System.Collections;

namespace SemihCelek.SliceMerge.Slice.SliceMovementController
{
    public interface ISliceMovementController
    {
        // Moving Methods
        IEnumerator MergeWithTargetSliceCoroutine(ISliceController targetSlice);
        IEnumerator MoveToSliceContainerCoroutine(SliceContainer.SliceContainer targetSliceContainer);
    }
}