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
            Debug.Log(SliceContainer.GetCurrentSliceContainerState().GetType());
            
            if (sliceMovementController == null) return;
            
            var currentContainerState = SliceContainer;
            var sliceScoreOnContainer = currentContainerState.GetComponentInChildren<SliceScoreView>();
            
            var sliceScoreOnNewSlice = sliceMovementController.GetComponent<SliceScoreView>();
            
            if (sliceScoreOnContainer.SliceScore != sliceScoreOnNewSlice.SliceScore) return;
            
            sliceMovementController.transform.SetParent(SliceContainer.transform);
            SliceContainer.StartCoroutine(MoveToSliceContainerCoroutine(sliceMovementController));
            sliceScoreOnContainer.UpdateScoreOnSliceObject(sliceScoreOnNewSlice.SliceScore*2,Color.cyan);
            CheckAvailableMerges();
            OnGenerateSlice?.Invoke();
        }

        private void CheckAvailableMerges()
        {
            var currentContainerState = SliceContainer;
            var previousContainerState = SliceContainer._previousContainer;
            var nextContainerState = SliceContainer._nextContainer;


            SliceContainer.StartCoroutine(MergeRecursivelyToLeft(currentContainerState, nextContainerState));

            SliceContainer.StartCoroutine(MergeRecursivelyToRight(currentContainerState, previousContainerState));
        }

        private IEnumerator MergeRecursivelyToLeft(SliceContainer previousContainer, SliceContainer nextContainer)
        {
            if (nextContainer.GetCurrentSliceContainerState().GetType() != typeof(FullContainerState)) yield break;

            var scoreValueOnPreviousSlide = previousContainer.GetComponentInChildren<SliceScoreView>();
            var scoreValueOnNextSlide = nextContainer.GetComponentInChildren<SliceScoreView>();

            var isScoreIsMatchingWithNextSlice =
                scoreValueOnNextSlide.SliceScore == scoreValueOnPreviousSlide.SliceScore ? true : false;

            if (!isScoreIsMatchingWithNextSlice) yield break;

            yield return SliceContainer.StartCoroutine(MergeContainersCoroutine(previousContainer, nextContainer));
            Debug.Log("Merge Left");

            // scoreValueOnPreviousSlide.UpdateScoreOnSliceObject(scoreValueOnPreviousSlide.SliceScore * 2, Color.blue);
            scoreValueOnNextSlide.UpdateScoreOnSliceObject(scoreValueOnPreviousSlide.SliceScore * 2, Color.blue);


            yield return SliceContainer.StartCoroutine(MergeRecursivelyToLeft(nextContainer,
                nextContainer._nextContainer));
        }

        private IEnumerator MergeRecursivelyToRight(SliceContainer previousContainer, SliceContainer nextContainer)
        {
            if (nextContainer.GetCurrentSliceContainerState().GetType() != typeof(FullContainerState)) yield break;

            var scoreValueOnPreviousSlide = previousContainer.GetComponentInChildren<SliceScoreView>();
            var scoreValueOnNextSlide = nextContainer.GetComponentInChildren<SliceScoreView>();

            var isScoreIsMatchingWithNextSlice =
                scoreValueOnNextSlide.SliceScore == scoreValueOnPreviousSlide.SliceScore ? true : false;

            if (!isScoreIsMatchingWithNextSlice) yield break;

            SliceContainer.StartCoroutine(MergeContainersCoroutine(previousContainer, nextContainer));
            Debug.Log("Merge Left");

            // scoreValueOnPreviousSlide.UpdateScoreOnSliceObject(scoreValueOnPreviousSlide.SliceScore * 2, Color.blue);
            scoreValueOnNextSlide.UpdateScoreOnSliceObject(scoreValueOnPreviousSlide.SliceScore * 2, Color.blue);


            yield return MergeRecursivelyToRight(nextContainer, nextContainer._previousContainer);
        }

        private IEnumerator MergeContainersCoroutine(SliceContainer previousContainer, SliceContainer nextContainer)
        {
            var time = 1f;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                var sliceTransform = previousContainer.transform.GetChild(0).transform;
                var targetContainerTransform = nextContainer.transform;

                sliceTransform.position =
                    Vector3.Lerp(sliceTransform.position, targetContainerTransform.position, elapsedTime / time);

                sliceTransform.rotation = Quaternion.Lerp(sliceTransform.rotation,
                    targetContainerTransform.rotation, elapsedTime / time);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            foreach (Transform child in previousContainer.transform)
            {
                SliceContainer.Destroy(child.gameObject);
            }

            // previousContainer.transform.GetChild(0).SetParent(nextContainer.transform);
            previousContainer.ChangeState(new EmptyContainerState(previousContainer));
        }

        private IEnumerator MoveToSliceContainerCoroutine(SliceMovementController sliceMovementController)
        {
            var time = 1f;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                var sliceTransform = sliceMovementController.transform;

                sliceTransform.position = Vector3.Lerp(sliceTransform.position,
                    SliceContainer.transform.position, elapsedTime / time);

                sliceTransform.rotation = Quaternion.Lerp(sliceTransform.rotation, SliceContainer.transform.rotation,
                    elapsedTime / time);

                elapsedTime += Time.deltaTime;

                yield return null;
            }
            
            SliceContainer.Destroy(sliceMovementController.gameObject);
        }
    }
}