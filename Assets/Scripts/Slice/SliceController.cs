using TMPro;
using UnityEngine;
using SemihCelek.SliceMerge.Slice.SliceEffects;
using SemihCelek.SliceMerge.SliceContainer;

namespace SemihCelek.SliceMerge.Slice
{
    public class SliceController : MonoBehaviour, ISliceController
    {
        [SerializeField]
        private Renderer _renderer;

        [SerializeField]
        private TrailRenderer _trailRenderer;

        [SerializeField]
        private ParticleSystem _particleSystem;

        [SerializeField]
        private TextMeshPro _scoreOnSliceObject;

        [SerializeField]
        private Transform _mountTargetPosition;

        [SerializeField]
        private SliceSettings _sliceSettings;


        private SliceEffects.SliceEffects _sliceEffects;
        private SliceScoreController.SliceScoreController _sliceScoreController;
        private SliceMovementController.SliceMovementController _sliceMovementController;
        private SliceMergeController.SliceMergeController _sliceMergeController;

        private void Awake()
        {
            _sliceEffects = new SliceEffects.SliceEffects(_trailRenderer, _renderer, _particleSystem);
            _sliceScoreController = new SliceScoreController.SliceScoreController(_scoreOnSliceObject, _sliceEffects);

            _sliceMovementController =
                new SliceMovementController.SliceMovementController(transform, _mountTargetPosition, this,
                    _sliceSettings);
            _sliceMergeController =
                new SliceMergeController.SliceMergeController(_sliceScoreController, _sliceMovementController, this);

            SliceHasEnteredContainer = false;
            SliceScore = _sliceScoreController.SliceScore;
        }

        public int SliceScore
        {
            get => _sliceScoreController.SliceScore;
            set => _sliceScoreController.SliceScore = value;
        }

        public bool SliceHasEnteredContainer { get; set; }

        public Transform SliceTransform
        {
            get => transform;
        }

        public GameObject SliceGameObject
        {
            get => gameObject;
        }

        public void MergeToTargetSlice(SliceContainer.SliceContainer targetSliceContainer)
        {
            _sliceMergeController.ValidateAndMergeWithTargetSlice(targetSliceContainer);
        }

        public void MoveSliceToContainer(SliceContainer.SliceContainer sliceContainer)
        {
            _sliceMergeController.MoveToSliceContainer(sliceContainer);
        }
    }
}