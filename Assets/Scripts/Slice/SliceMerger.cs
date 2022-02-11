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

            var currentSliceScore = currentSliceContainer.GetComponentInChildren<SliceScoreView>().SliceScore;

            if (nextSliceContainer.GetCurrentSliceContainerState().GetType() == typeof(EmptyContainerState) &&
                previousSliceContainer.GetCurrentSliceContainerState().GetType() == typeof(EmptyContainerState)) return;

            if (nextSliceContainer.GetCurrentSliceContainerState().GetType() == typeof(FullContainerState))
            {
                var nextSliceScore = nextSliceContainer.GetComponentInChildren<SliceScoreView>();

                var isScoreIsMatchingWithNextSlice = nextSliceScore.SliceScore == currentSliceScore;

                if (!isScoreIsMatchingWithNextSlice) return;
                nextSliceScore.UpdateScoreOnSliceObject(currentSliceScore * 2, Color.blue);
                _currentSliceContainer.StartCoroutine(MergeSliceCoroutine(currentSliceContainer, nextSliceContainer));
            }

            if (previousSliceContainer.GetCurrentSliceContainerState().GetType() == typeof(FullContainerState))
            {
                var previousSliceScore = previousSliceContainer.GetComponentInChildren<SliceScoreView>();

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
            var sliceScoreOnCurrentContainer = _currentSliceContainer.GetComponentInChildren<SliceScoreView>();
            var sliceScoreOnIncomingSlice = incomingSlice.GetComponent<SliceScoreView>();

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
            var time = 1f;

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

            foreach (Transform child in currentSliceContainer.transform)
            {
                SliceContainer.SliceContainer.Destroy(child.gameObject);
            }

            currentSliceContainer.ChangeState(new EmptyContainerState(currentSliceContainer));

            CheckAvailableMerges(targetSliceContainer);
        }

        private IEnumerator MoveToSliceContainerCoroutine(SliceMovementController sliceMovementController)
        {
            var time = 1f;

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