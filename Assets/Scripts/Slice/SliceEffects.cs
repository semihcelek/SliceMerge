using UnityEngine;

namespace SemihCelek.SliceMerge.Slice
{
    public class SliceEffects
    {
        private SliceViewController _sliceViewController;
        // create a scriptable object then specify the colors from it
        
        public SliceEffects(SliceViewController sliceViewController)
        {
            _sliceViewController = sliceViewController;
        }

        public void RemoveTrailerEffect()
        {
            var trailerRenderer = _sliceViewController.SliceTrailRenderer;
            trailerRenderer.enabled = false;
        }

        public void UpdateSliceColor(int score)
        {
            var renderer = _sliceViewController.SliceRenderer;

            renderer.material.color = score switch
            {
                4 => Color.yellow,
                8 => Color.red,
                16 => Color.green,
                32 => Color.cyan,
                _ => renderer.material.color
            };
        }
    }
}