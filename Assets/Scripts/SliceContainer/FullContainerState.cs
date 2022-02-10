using SemihCelek.SliceMerge.Utilities;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public class FullContainerState : SliceContainerState
    {
        public FullContainerState(SliceContainer sliceContainer, SliceContainer nextContainer,
            SliceContainer previousContainer) : base(sliceContainer, nextContainer, previousContainer)
        {
        }


        public static event SliceGenerationAction OnGenerateSlice;

        public override void Start()
        {
            OnGenerateSlice?.Invoke();
        }
    }
}