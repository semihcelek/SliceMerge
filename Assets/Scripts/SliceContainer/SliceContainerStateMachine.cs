using UnityEngine;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public abstract class SliceContainerStateMachine : MonoBehaviour
    {
        protected SliceContainerState SliceContainerState;


        public void ChangeState(SliceContainerState state)
        {
            SliceContainerState = state;
            state.Start();
        }

        public SliceContainerState GetCurrentSliceContainerState()
        {
            return SliceContainerState;
        }
    }
}