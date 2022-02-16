using UnityEngine;

namespace SemihCelek.SliceMerge.Slice
{
    public interface ISliceController
    {
       int SliceScore { get; set; }
       Transform SliceTransform { get; }
       void MergeToTargetSlice(ISliceController targetSlice);
       void MergeToSliceContainer(SliceContainer.SliceContainer sliceContainer);
    }
}