namespace SemihCelek.SliceMerge.Slice
{
    public class SliceScoreController
    {
        private int _sliceScore;

        public SliceScoreController(int sliceScore)
        {
            _sliceScore = sliceScore;
        }

        public int SliceScore
        {
            get => _sliceScore;
            set => _sliceScore = value;
        }
    }
}