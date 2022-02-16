namespace SemihCelek.SliceMerge.Slice.SliceScoreController
{
    public interface ISliceScoreController
    {
        int SliceScore { get; set; }
        void UpdateScore(int score);
        
    }
}