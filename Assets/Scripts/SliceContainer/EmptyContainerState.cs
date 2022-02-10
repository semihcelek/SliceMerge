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
        

        public override void Start()
        {
        }

        public override void HandleMount(SliceMovementController sliceMovementController)
        {
            sliceMovementController.transform.SetParent(SliceContainer.transform);
            SliceContainer.StartCoroutine(MoveToSliceContainerCoroutine(sliceMovementController));
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

                sliceTransform.rotation = Quaternion.Lerp(sliceTransform.rotation, SliceContainer.transform.rotation,
                    elapsedTime / time);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            SliceContainer.ChangeState(new FullContainerState(SliceContainer, NextContainer, PreviousContainer));
        }
    }
}