using System.Collections;
using SemihCelek.SliceMerge.SliceContainer;
using UnityEngine;

namespace SemihCelek.SliceMerge.Slice
{
    public class SliceMerger
    {
        private SliceContainer.SliceContainer _currentSliceContainer;

        public SliceMerger(SliceContainer.SliceContainer currentSliceContainer)
        {
            _currentSliceContainer = currentSliceContainer;
        }

        public void CheckAvailableMerges(SliceContainer.SliceContainer currentSliceContainer)
        {
            var previousSliceContainer = currentSliceContainer._previousContainer;
            var nextSliceContainer = currentSliceContainer._nextContainer;

            var currentSliceScore = currentSliceContainer.GetComponentInChildren<SliceScoreController>().SliceScore;

            if (nextSliceContainer.GetCurrentSliceContainerState().GetType() == typeof(EmptyContainerState) &&
                previousSliceContainer.GetCurrentSliceContainerState().GetType() == typeof(EmptyContainerState)) return;

            if (CheckNextContainers(currentSliceContainer, nextSliceContainer, currentSliceScore))
            {
                CheckPreviousContainers(currentSliceContainer, previousSliceContainer, currentSliceScore);
            }
        }

        private bool CheckNextContainers(SliceContainer.SliceContainer currentSliceContainer, SliceContainer.SliceContainer nextSliceContainer,
            int currentSliceScore)
        {
            if (nextSliceContainer.GetCurrentSliceContainerState().GetType() != typeof(FullContainerState)) return true;
            var nextSliceScore = nextSliceContainer.GetComponentInChildren<SliceScoreController>();

            var isScoreIsMatchingWithNextSlice = nextSliceScore.SliceScore == currentSliceScore;

            if (!isScoreIsMatchingWithNextSlice) return true;
            nextSliceScore.UpdateScoreOnSliceObject(currentSliceScore * 2, Color.blue);
            _currentSliceContainer.StartCoroutine(MergeSliceCoroutine(currentSliceContainer, nextSliceContainer));

            return false;
        }

        private void CheckPreviousContainers(SliceContainer.SliceContainer currentSliceContainer, SliceContainer.SliceContainer previousSliceContainer,
            int currentSliceScore)
        {
            if (previousSliceContainer.GetCurrentSliceContainerState().GetType() == typeof(FullContainerState))
            {
                var previousSliceScore = previousSliceContainer.GetComponentInChildren<SliceScoreController>();

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
            var sliceScoreOnCurrentContainer = _currentSliceContainer.GetComponentInChildren<SliceScoreController>();
            var sliceScoreOnIncomingSlice = incomingSlice.GetComponent<SliceScoreController>();

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
            var time = 0.6f;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                var sliceTransform = currentSliceContainer.transform.GetChild(0).transform;
                var targetContainerTransform = targetSliceContainer.transform;

                sliceTransform.position =
                    Vector3.Lerp(sliceTransform.position, targetContainerTransform.position, elapsedTime / time);

                sliceTransform.rotation = Quaternion.Lerp(sliceTransform.rotation,
                    targetContainerTransform.rotation, elapsedTime / time);

                elapsedTime += Time.deltaTime;

                yield return null;
            }
            
            // SliceContainer.SliceContainer.Destroy(currentSliceContainer.transform.GetChild(0));

            foreach (Transform child in currentSliceContainer.transform)
            {
                SliceContainer.SliceContainer.Destroy(child.gameObject);
            }

            currentSliceContainer.ChangeState(new EmptyContainerState(currentSliceContainer));

            CheckAvailableMerges(targetSliceContainer);
        }

        private IEnumerator MoveToSliceContainerCoroutine(SliceMovementController sliceMovementController)
        {
            var time = 0.4f;

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