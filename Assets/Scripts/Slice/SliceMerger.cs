using System.Collections;
using SemihCelek.SliceMerge.SliceContainer;
using UnityEngine;

namespace SemihCelek.SliceMerge.Slice
{
    public class SliceMerger
    {
        private SliceContainer.SliceContainer _currentSliceContainer;
        private SliceContainerSettings _sliceContainerSettings;
        
        private SliceScoreController _sliceScoreController;
        private SliceViewController _sliceViewController;
        private SliceEffects _sliceEffects;

        public SliceMerger(SliceContainer.SliceContainer currentSliceContainer,
            SliceScoreController sliceScoreController, SliceViewController sliceViewController,
            SliceContainerSettings sliceContainerSettings)
        {
            _currentSliceContainer = currentSliceContainer;
            _sliceViewController = sliceViewController;
            _sliceScoreController = sliceScoreController;
            _sliceContainerSettings = sliceContainerSettings;

            _sliceEffects = new SliceEffects(_sliceViewController);
            _sliceEffects.RemoveTrailerEffect();
        }

        public void CheckAvailableMerges(SliceContainer.SliceContainer currentSliceContainer)
        {
            var previousSliceContainer = currentSliceContainer.PreviousContainer;
            var nextSliceContainer = currentSliceContainer.NextContainer;

            var currentSliceScore = currentSliceContainer.SliceScoreController.SliceScore;

            if (nextSliceContainer.GetCurrentSliceContainerState().GetType() == typeof(EmptyContainerState) &&
                previousSliceContainer.GetCurrentSliceContainerState().GetType() == typeof(EmptyContainerState)) return;

            if (CheckNextContainers(currentSliceContainer, nextSliceContainer, currentSliceScore))
            {
                CheckPreviousContainers(currentSliceContainer, previousSliceContainer, currentSliceScore);
            }
        }

        private bool CheckNextContainers(SliceContainer.SliceContainer currentSliceContainer,
            SliceContainer.SliceContainer nextSliceContainer,
            int currentSliceScore)
        {
            if (nextSliceContainer.GetCurrentSliceContainerState().GetType() != typeof(FullContainerState)) return true;
            var nextSliceScore = nextSliceContainer.SliceScoreController;

            var isScoreIsMatchingWithNextSlice = nextSliceScore.SliceScore == currentSliceScore;

            if (!isScoreIsMatchingWithNextSlice) return true;
            nextSliceScore.UpdateScoreOnSliceObject(currentSliceScore * 2, Color.blue);
            _currentSliceContainer.StartCoroutine(MergeSliceCoroutine(currentSliceContainer, nextSliceContainer));

            return false;
        }

        private void CheckPreviousContainers(SliceContainer.SliceContainer currentSliceContainer,
            SliceContainer.SliceContainer previousSliceContainer,
            int currentSliceScore)
        {
            if (previousSliceContainer.GetCurrentSliceContainerState().GetType() == typeof(FullContainerState))
            {
                var previousSliceScore = previousSliceContainer.SliceScoreController;

                var isScoreIsMatchingWithPreviousSlice =
                    previousSliceScore.SliceScore == currentSliceScore;

                if (!isScoreIsMatchingWithPreviousSlice) return;
                previousSliceScore.UpdateScoreOnSliceObject(currentSliceScore * 2, Color.blue);
                _currentSliceContainer.StartCoroutine(
                    MergeSliceCoroutine(currentSliceContainer, previousSliceContainer));
            }
        }

        public void CombineIncomingSlice(SliceMovementController incomingSlice)
        {
            var sliceScoreOnCurrentContainer = _currentSliceContainer.SliceScoreController;
            var sliceScoreOnIncomingSlice = incomingSlice.gameObject.GetComponent<SliceScoreController>();

            if (sliceScoreOnCurrentContainer.SliceScore == sliceScoreOnIncomingSlice.SliceScore)
            {
                incomingSlice.transform.SetParent(_currentSliceContainer.transform);

                _currentSliceContainer.StartCoroutine(MoveToSliceContainerCoroutine(incomingSlice));
                sliceScoreOnCurrentContainer.UpdateScoreOnSliceObject(sliceScoreOnCurrentContainer.SliceScore * 2,
                    Color.cyan);
            }
            else
            {
                SliceContainer.SliceContainer.Destroy(incomingSlice.gameObject);
            }
        }

        private IEnumerator MergeSliceCoroutine(SliceContainer.SliceContainer currentSliceContainer,
            SliceContainer.SliceContainer targetSliceContainer)
        {
            var time = _sliceContainerSettings.MoveSpeed;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                var sliceTransform = currentSliceContainer.SliceViewController.transform;
                var targetContainerTransform = targetSliceContainer.transform;

                sliceTransform.position =
                    Vector3.Lerp(sliceTransform.position, targetContainerTransform.position, elapsedTime / time);

                sliceTransform.rotation = Quaternion.Lerp(sliceTransform.rotation,
                    targetContainerTransform.rotation, elapsedTime / time);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            SliceContainer.SliceContainer.Destroy(currentSliceContainer.SliceViewController.gameObject);

            currentSliceContainer.ChangeState(new EmptyContainerState(currentSliceContainer,_sliceContainerSettings));

            CheckAvailableMerges(targetSliceContainer);
        }

        private IEnumerator MoveToSliceContainerCoroutine(SliceMovementController sliceMovementController)
        {
            var time = _sliceContainerSettings.MoveSpeed;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                var sliceTransform = sliceMovementController.transform;

                sliceTransform.position = Vector3.Lerp(sliceTransform.position,
                    _currentSliceContainer.transform.position, elapsedTime / time);

                sliceTransform.rotation = Quaternion.Lerp(sliceTransform.rotation,
                    _currentSliceContainer.transform.rotation,
                    elapsedTime / time);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            SliceContainer.SliceContainer.Destroy(sliceMovementController.gameObject);

            CheckAvailableMerges(_currentSliceContainer);
        }
    }
}