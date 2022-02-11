using System.Collections;
using SemihCelek.SliceMerge.Slice;
using SemihCelek.SliceMerge.Utilities;
using UnityEngine;

namespace SemihCelek.SliceMerge.SliceContainer
{
    public class FullContainerState : SliceContainerState
    {
        public FullContainerState(SliceContainer sliceContainer) : base(sliceContainer)
        {
        }

        public static event SliceGenerationAction OnGenerateSlice;

        public override void Start()
        {
            CheckAvailableMerges();
            OnGenerateSlice?.Invoke();
        }

        public override void HandleMount(SliceMovementController sliceMovementController)
        {
            base.HandleMount(sliceMovementController);
        }

        private void CheckAvailableMerges()
        {
            var previousContainerState = SliceContainer._previousContainer;
            var nextContainerState = SliceContainer._nextContainer;


            var scoreValueOnCurrentSlide = SliceContainer.GetComponentInChildren<SliceScoreView>();
            var scoreValueOnPreviousSlide = previousContainerState.GetComponentInChildren<SliceScoreView>();
            var scoreValueOnNextSlide = nextContainerState.GetComponentInChildren<SliceScoreView>();


            if (previousContainerState.GetCurrentSliceContainerState().GetType() == typeof(FullContainerState) &&
                nextContainerState.GetCurrentSliceContainerState().GetType() == typeof(FullContainerState))
            {
                SliceContainer.StartCoroutine(DoubleMergeCoroutine(previousContainerState, nextContainerState));
                Debug.Log("Double Merge");

                return;
            }

            if (nextContainerState.GetCurrentSliceContainerState().GetType() == typeof(FullContainerState))
            {
                var isScoreIsMatchingWithNextSlice =
                    scoreValueOnNextSlide.SliceScore == scoreValueOnCurrentSlide.SliceScore ? true : false;

                if (!isScoreIsMatchingWithNextSlice) return;

                SliceContainer.StartCoroutine(MergeContainersCoroutine(nextContainerState));
                Debug.Log("Merge Left");

                scoreValueOnCurrentSlide.UpdateScoreOnSliceObject(scoreValueOnCurrentSlide.SliceScore * 2, Color.blue);

                return;
            }

            if (previousContainerState.GetCurrentSliceContainerState().GetType() == typeof(FullContainerState))
            {
                var isScoreIsMatchingWithPreviousSlice =
                    scoreValueOnPreviousSlide.SliceScore == scoreValueOnCurrentSlide.SliceScore
                        ? true
                        : false;

                if (!isScoreIsMatchingWithPreviousSlice) return;


                SliceContainer.StartCoroutine(MergeContainersCoroutine(previousContainerState));
                Debug.Log("Merge Right");

                scoreValueOnCurrentSlide.UpdateScoreOnSliceObject(scoreValueOnCurrentSlide.SliceScore * 2, Color.blue);
                return;
            }
        }

        private bool MergeRecursively(SliceContainer previousContainer, SliceContainer nextContainer)
        {
            var scoreValueOnPreviousSlide = previousContainer.GetComponentInChildren<SliceScoreView>();
            var scoreValueOnNextSlide = nextContainer.GetComponentInChildren<SliceScoreView>();

            if (nextContainer.GetCurrentSliceContainerState().GetType() != typeof(FullContainerState)) return false;

            var isScoreIsMatchingWithNextSlice =
                scoreValueOnNextSlide.SliceScore == scoreValueOnPreviousSlide.SliceScore ? true : false;

            if (!isScoreIsMatchingWithNextSlice) return false;

            SliceContainer.StartCoroutine(MergeContainersCoroutine(nextContainer));
            Debug.Log("Merge Left");

            scoreValueOnNextSlide.UpdateScoreOnSliceObject(scoreValueOnPreviousSlide.SliceScore * 2, Color.blue);


            return MergeRecursively(nextContainer, nextContainer._nextContainer);
        }


        private IEnumerator DoubleMergeCoroutine(SliceContainer previousContainerState,
            SliceContainer nextContainerState)
        {
            var time = 1f;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                var previousContainersSliceTransform = previousContainerState.transform.GetChild(0).transform;
                var currentContainerTransform = SliceContainer.transform;

                previousContainersSliceTransform.position =
                    Vector3.Lerp(previousContainersSliceTransform.position, currentContainerTransform.position,
                        elapsedTime / time);

                previousContainersSliceTransform.rotation = Quaternion.Lerp(previousContainersSliceTransform.rotation,
                    currentContainerTransform.rotation, elapsedTime / time);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            foreach (Transform child in previousContainerState.transform)
            {
                SliceContainer.Destroy(child.gameObject);
            }

            previousContainerState.ChangeState(new EmptyContainerState(previousContainerState));

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
            SliceContainer.ChangeState(new EmptyContainerState(SliceContainer));
        }
    }
}