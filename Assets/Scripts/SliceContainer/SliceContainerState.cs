using SemihCelek.SliceMerge.Slice;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public abstract class SliceContainerState
    {
        protected SliceContainer SliceContainer;


        protected SliceContainerState(SliceContainer sliceContainer)
        {
            SliceContainer = sliceContainer;
        }

        public virtual void Start()
        {
        }

        public virtual void HandleTrigger()
        {
        }

        public virtual void HandleMount(ISliceController sliceController)
        {
        }
    }
}