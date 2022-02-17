using SemihCelek.SliceMerge.Slice;
using SemihCelek.SliceMerge.SliceContainer.States;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public class ContainerMergeManager
    {
        private SliceContainer _currentContainer;
        private SliceContainer _nextContainer;
        private SliceContainer _previousContainer;

        private ISliceController _sliceInsideContainer;

        public ContainerMergeManager(SliceContainer currentContainer, SliceContainer nextContainer,
            SliceContainer previousContainer, ISliceController sliceInsideContainer)
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

            _sliceInsideContainer.MergeToTargetSlice(_nextContainer);

            return false;
        }

        private void CheckPreviousContainer()
        {
            if (_previousContainer.GetCurrentSliceContainerState().GetType() != typeof(FullContainerState)) return;

            _sliceInsideContainer.MergeToTargetSlice(_previousContainer);
        }
    }
}