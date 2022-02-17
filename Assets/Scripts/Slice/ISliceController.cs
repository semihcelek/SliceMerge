using UnityEngine;

namespace SemihCelek.SliceMerge.Slice
{
    public interface ISliceController
    {
       int SliceScore { get; set; }
       bool SliceHasEnteredContainer { get; set; }
       Transform SliceTransform { get; }
       GameObject SliceGameObject { get; }
       void MergeToTargetSlice(SliceContainer.SliceContainer targetSliceContainer);
       void MoveSliceToContainer(SliceContainer.SliceContainer sliceContainer);
    }
}