using System.Collections;
using SemihCelek.SliceMerge.PlayerInput;
using UnityEngine;

namespace SemihCelek.SliceMerge.Slice.SliceMovementController
{
    public class SliceMovementController : ISliceMovementController
    {
        // In here, plan is to handle Movement.

        private Transform _cachedTransform;
        private Transform _targetMountPosition;
        private SliceController _sliceController;
        private SliceSettings _sliceSettings;


        public bool ArrivedCondition;

        public SliceMovementController(Transform cachedTransform, Transform targetMountPosition,
            SliceController sliceController, SliceSettings sliceSettings)
        {
            _cachedTransform = cachedTransform;
            _targetMountPosition = targetMountPosition;
            _sliceController = sliceController;
            _sliceSettings = sliceSettings;

            ArrivedCondition = false;
            InputHandler.OnClickFireButton += OnFireButton;
        }

        private void OnFireButton()
        {
            _sliceController.StartCoroutine(MoveSliceToCircleCoroutine());
            InputHandler.OnClickFireButton -= OnFireButton;
        }

        private IEnumerator MoveSliceToCircleCoroutine()
        {
            var time = 0.4f;

            var startPosition = _cachedTransform.position;
            var destinationPosition = _targetMountPosition.position;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                _cachedTransform.position =
                    Vector3.Lerp(startPosition, destinationPosition, (elapsedTime / time));

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            ArrivedCondition = true;
        }

        public IEnumerator MergeWithTargetSliceCoroutine(ISliceController targetSlice)
        {
            var time = _sliceSettings.MoveSpeed;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                var sliceTransform = _cachedTransform;
                var targetSliceTransform = targetSlice.SliceTransform;

                sliceTransform.position =
                    Vector3.Lerp(sliceTransform.position, targetSliceTransform.position, elapsedTime / time);

                sliceTransform.rotation = Quaternion.Lerp(sliceTransform.rotation,
                    targetSliceTransform.rotation, elapsedTime / time);

                elapsedTime += Time.deltaTime;

                yield return null;
            }
        }
        
        public IEnumerator MoveToSliceContainerCoroutine(SliceContainer.SliceContainer targetSliceContainer)
        {
            _cachedTransform.SetParent(targetSliceContainer.transform);
            
            var time = _sliceSettings.MoveSpeed;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                var sliceTransform = _cachedTransform;

                sliceTransform.position = Vector3.Lerp(sliceTransform.position,
                    targetSliceContainer.transform.position, elapsedTime / time);

                sliceTransform.rotation = Quaternion.Lerp(sliceTransform.rotation, targetSliceContainer.transform.rotation,
                    elapsedTime / time);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

        }
    }
}