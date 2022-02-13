namespace SemihCelek.SliceMerge.Slice
{
    public class SliceEffects
    {
        private SliceViewController _sliceViewController;
        
        public SliceEffects(SliceViewController sliceViewController)
        {
            _sliceViewController = sliceViewController;
        }

        public void RemoveTrailerEffect()
        {
            var trailerRenderer = _sliceViewController.SliceTrailRenderer;
            trailerRenderer.enabled = false;
        }
    }
}