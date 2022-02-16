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

        public void ValidateAndMergeWithTargetSlice(ISliceController targetSlice)
        {
            _targetSlice = targetSlice;
            if (ScoreMatches(_currentSlice, _targetSlice))
            {
                _currentSlice.StartCoroutine(MergeSlicesCoroutine(targetSlice));
            }
        }

        private IEnumerator MergeSlicesCoroutine(ISliceController targetSlice)
        {
            yield return _currentSlice.StartCoroutine(
                _currentSliceMovementController.MergeWithTargetSliceCoroutine(targetSlice));
            
            _currentSliceScoreController.UpdateScore(_currentSlice.SliceScore*2);
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
            sliceContainer.ChangeState(new FullContainerState(sliceContainer,_currentSlice));

        }
    }
}