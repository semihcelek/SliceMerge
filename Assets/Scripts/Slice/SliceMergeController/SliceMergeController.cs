using System.Collections;
using SemihCelek.SliceMerge.Slice.SliceMovementController;
using SemihCelek.SliceMerge.Slice.SliceScoreController;
using SemihCelek.SliceMerge.SliceContainer.States;

namespace SemihCelek.SliceMerge.Slice.SliceMergeController
{
    public class SliceMergeController : ISliceMergeController
    {
        private ISliceController _targetSlice; // does we have this dep on start?
        private ISliceScoreController _currentSliceScoreController;
        private ISliceMovementController _currentSliceMovementController;
        private SliceController _currentSlice;

        public SliceMergeController(ISliceScoreController currentSliceScoreController,
            ISliceMovementController currentSliceMovementController, SliceController currentSlice)
        {
            _currentSliceScoreController = currentSliceScoreController;
            _currentSliceMovementController = currentSliceMovementController;
            _currentSlice = currentSlice;
        }

        public void ValidateAndMergeWithTargetSlice(SliceContainer.SliceContainer targetSliceContainer)
        {
            _targetSlice = targetSliceContainer.SliceInsideContainer;
            if (!ScoreMatches(_currentSlice, _targetSlice)) return;

            _currentSlice.StartCoroutine(MergeSlicesCoroutine(_targetSlice));
            targetSliceContainer.ChangeState(new EmptyContainerState(targetSliceContainer));
        }

        private IEnumerator MergeSlicesCoroutine(ISliceController targetSlice)
        {
            yield return _currentSlice.StartCoroutine(
                _currentSliceMovementController.MergeWithTargetSliceCoroutine(targetSlice));

            _currentSliceScoreController.UpdateScore(_currentSlice.SliceScore * 2);
            SliceController.Destroy(targetSlice.SliceGameObject);
        }

        private bool ScoreMatches(ISliceController currentSlice, ISliceController targetSlice)
        {
            var currentSliceScore = currentSlice.SliceScore;
            var targetSliceScore = targetSlice.SliceScore;

            var isMatch = currentSliceScore == targetSliceScore;
            return isMatch;
        }

        public void MoveToSliceContainer(SliceContainer.SliceContainer sliceContainer)
        {
            _currentSlice.StartCoroutine(_currentSliceMovementController.MoveToSliceContainerCoroutine(sliceContainer));
            _currentSlice.SliceHasEnteredContainer = true;

            sliceContainer.ChangeState(new FullContainerState(sliceContainer, _currentSlice));
        }
    }
}