using System.Collections;
using SemihCelek.SliceMerge.Slice;
using UnityEngine;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public class EmptyContainerState : SliceContainerState
    {
        public EmptyContainerState(SliceContainer sliceContainer, SliceContainer nextContainer,
            SliceContainer previousContainer) : base(sliceContainer, nextContainer, previousContainer)
        {
        }
        
        public override void HandleMount(SliceMovementController sliceMovementController)
        {
            sliceMovementController.transform.SetParent(SliceContainer.transform);
            SliceContainer.StartCoroutine(MoveToSliceContainerCoroutine(sliceMovementController));

            SliceContainer.ChangeState(new FullContainerState(SliceContainer, NextContainer, PreviousContainer));
        }


        private IEnumerator MoveToSliceContainerCoroutine(SliceMovementController sliceMovementController)
        {
            var time = 1f;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                var sliceTransform = sliceMovementController.transform;

                sliceTransform.position = Vector3.MoveTowards(sliceTransform.position,
                    SliceContainer.transform.position, Time.deltaTime);

                sliceTransform.localEulerAngles = Vector3.zero;

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            // OnCheckMerge?.Invoke();
            // OnGenerateSlice?.Invoke();
        }
    }
}