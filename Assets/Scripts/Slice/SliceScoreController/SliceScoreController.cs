using SemihCelek.SliceMerge.Slice.SliceEffects;
using SemihCelek.SliceMerge.Utilities;
using TMPro;

namespace SemihCelek.SliceMerge.Slice.SliceScoreController
{
    public class SliceScoreController : ISliceScoreController
    {
        private TextMeshPro _scoreOnSliceObject;

        private ISliceEffects _sliceEffects;

        public static event GameScoreUpdateAction OnUpdateScore;
        public static event GameScoreGetRandomScoreAction OnGetRandomizedScore;

        private int _sliceScore;

        public int SliceScore
        {
            get => _sliceScore;
            set => _sliceScore = value;
        }

        public SliceScoreController(TextMeshPro scoreOnSliceObject, ISliceEffects sliceEffects)
        {
            _scoreOnSliceObject = scoreOnSliceObject;
            _sliceEffects = sliceEffects;

            SetInitialScore();
        }

        private void SetInitialScore()
        {
            _sliceScore = 2;
            
            if (OnGetRandomizedScore != null)
            {
                _sliceScore = OnGetRandomizedScore.Invoke();
            }

            _scoreOnSliceObject.text = _sliceScore.ToString();
            _sliceEffects.UpdateSliceColor(_sliceScore);
        }

        public void UpdateScore(int score)
        {
            _sliceScore = score;
            _scoreOnSliceObject.text = _sliceScore.ToString();
            _sliceEffects.UpdateSliceColor(score);
            OnUpdateScore?.Invoke(score);
        }
    }

    public delegate void SliceScoreActions(int score);
}