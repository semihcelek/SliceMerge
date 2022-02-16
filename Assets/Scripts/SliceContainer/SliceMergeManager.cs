using SemihCelek.SliceMerge.Slice;
using SemihCelek.SliceMerge.SliceContainer.States;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public class SliceMergeManager
    {
        private SliceContainer _currentContainer;
        private SliceContainer _nextContainer;
        private SliceContainer _previousContainer;

        private SliceController _sliceInsideContainer;

        public SliceMergeManager(SliceContainer currentContainer, SliceContainer nextContainer,
            SliceContainer previousContainer, SliceController sliceInsideContainer)
        {
            _currentContainer = currentContainer;
            _nextContainer = nextContainer;
            _previousContainer = previousContainer;
            _sliceInsideContainer = sliceInsideContainer;
        }

        public void CheckAvailableMerges()
        {
            if (_nextContainer.GetCurrentSliceContainerState().GetType() == typeof(EmptyContainerState) &&
                _previousContainer.GetCurrentSliceContainerState().GetType() == typeof(EmptyContainerState)) return;

            if (CheckNextContainer())
            {
                CheckPreviousContainer();
            }
        }

        private bool CheckNextContainer()
        {
            if (_nextContainer.GetCurrentSliceContainerState().GetType() != typeof(FullContainerState)) return true;

            _sliceInsideContainer.MergeToTargetSlice(_nextContainer.SliceInsideContainer);
            _currentContainer.ChangeState(new EmptyContainerState(_currentContainer));

            return false;
        }

        private void CheckPreviousContainer()
        {
            if (_previousContainer.GetCurrentSliceContainerState().GetType() != typeof(FullContainerState)) return;

            _sliceInsideContainer.MergeToTargetSlice(_previousContainer.SliceInsideContainer);
            _currentContainer.ChangeState(new EmptyContainerState(_currentContainer));
        }
    }
}