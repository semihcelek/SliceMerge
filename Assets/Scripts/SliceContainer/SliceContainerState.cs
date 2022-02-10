using SemihCelek.SliceMerge.Slice;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public abstract class SliceContainerState
    {
        protected SliceContainer SliceContainer;
        
        protected SliceContainer NextContainer;

        protected SliceContainer PreviousContainer;

        protected SliceContainerState(SliceContainer sliceContainer, SliceContainer nextContainer, SliceContainer previousContainer)
        {
            SliceContainer = sliceContainer;
            NextContainer = nextContainer;
            PreviousContainer = previousContainer;
        }
        
        public virtual void Start()
        {
        }

        public virtual void HandleTrigger()
        {
        }

        public virtual void HandleMount(SliceMovementController sliceMovementController)
        {
        }
    }
}