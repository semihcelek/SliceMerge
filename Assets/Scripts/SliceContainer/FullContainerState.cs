using System.Collections;
using SemihCelek.SliceMerge.Slice;
using SemihCelek.SliceMerge.Utilities;
using UnityEngine;

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
            CheckAvailableMerges();
        }

        public override void HandleMount(SliceMovementController sliceMovementController)
        {
            base.HandleMount(sliceMovementController);
        }

        private void CheckAvailableMerges()
        {
            var previousContainerState = PreviousContainer;
            var nextContainerState = NextContainer;

            if (previousContainerState.GetCurrentSliceContainerState().GetType() == typeof(FullContainerState) &&
                nextContainerState.GetCurrentSliceContainerState().GetType() == typeof(FullContainerState))
            {
                SliceContainer.StartCoroutine(DoubleMergeCoroutine(previousContainerState, nextContainerState));
                Debug.Log("Double Merge");
                
                return;
            }
            
            if (nextContainerState.GetCurrentSliceContainerState().GetType() == typeof(FullContainerState))
            {
                SliceContainer.StartCoroutine(MergeContainersCoroutine(nextContainerState));
                Debug.Log("Merge Left");
                
                return;
            }
            
            if (previousContainerState.GetCurrentSliceContainerState().GetType() == typeof(FullContainerState))
            {
                SliceContainer.StartCoroutine(MergeContainersCoroutine(previousContainerState));
                Debug.Log("Merge Right");
                
                return;
            }


        }

        private IEnumerator DoubleMergeCoroutine(SliceContainer previousContainerState, SliceContainer nextContainerState)
        {
            
            var time = 1f;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                var previousContainersSliceTransform = previousContainerState.transform.GetChild(0).transform;
                var currentContainerTransform = SliceContainer.transform;

                previousContainersSliceTransform.position =
                    Vector3.Lerp(previousContainersSliceTransform.position, currentContainerTransform.position, elapsedTime / time);

                previousContainersSliceTransform.rotation = Quaternion.Lerp(previousContainersSliceTransform.rotation,
                    currentContainerTransform.rotation, elapsedTime / time);

                elapsedTime += Time.deltaTime;

                yield return null;
            }
            
            foreach (Transform child in previousContainerState.transform)
            {
                SliceContainer.Destroy(child.gameObject);
            }
            // previousContainerState.ChangeState(new EmptyContainerState(previousContainerState));
            
            yield return SliceContainer.StartCoroutine(MergeContainersCoroutine(nextContainerState));
        }

        private IEnumerator MergeContainersCoroutine(SliceContainer containerToMerge)
        {
            var time = 1f;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                var sliceTransform = SliceContainer.transform.GetChild(0).transform;
                var targetContainerTransform = containerToMerge.transform;

                sliceTransform.position =
                    Vector3.Lerp(sliceTransform.position, targetContainerTransform.position, elapsedTime / time);

                sliceTransform.rotation = Quaternion.Lerp(sliceTransform.rotation,
                    targetContainerTransform.rotation, elapsedTime / time);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            foreach (Transform child in containerToMerge.transform)
            {
                SliceContainer.Destroy(child.gameObject);
            }
            
            SliceContainer.transform.GetChild(0).SetParent(containerToMerge.transform);
            SliceContainer.ChangeState(new EmptyContainerState(SliceContainer,NextContainer, PreviousContainer));
        }
        
    }
}