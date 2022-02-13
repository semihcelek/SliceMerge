using System.Collections;
using SemihCelek.SliceMerge.Slice;
using UnityEngine;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public class EmptyContainerState : SliceContainerState
    {
        private SliceContainerSettings _sliceContainerSettings;
        
        public EmptyContainerState(SliceContainer sliceContainer, SliceContainerSettings sliceContainerSettings) : base(sliceContainer)
        {
            _sliceContainerSettings = sliceContainerSettings;
        }

        public override void HandleMount(SliceMovementController sliceMovementController)
        {
            sliceMovementController.transform.SetParent(SliceContainer.transform);
            SliceContainer.StartCoroutine(MoveToSliceContainerCoroutine(sliceMovementController));
        }

        private IEnumerator MoveToSliceContainerCoroutine(SliceMovementController sliceMovementController)
        {
            var time = _sliceContainerSettings.MoveSpeed;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                var sliceTransform = sliceMovementController.transform;

                sliceTransform.position = Vector3.Lerp(sliceTransform.position,
                    SliceContainer.transform.position, elapsedTime / time);

                sliceTransform.rotation = Quaternion.Lerp(sliceTransform.rotation, SliceContainer.transform.rotation,
                    elapsedTime / time);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            SliceContainer.ChangeState(new FullContainerState(SliceContainer, sliceMovementController,_sliceContainerSettings));
        }
    }
}