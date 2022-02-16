using UnityEngine;

namespace SemihCelek.SliceMerge.Slice.SliceEffects
{
    public class SliceEffects : ISliceEffects
    {
        private SliceController _sliceController;

        private TrailRenderer _trailRenderer;
        private Renderer _renderer;
        private ParticleSystem _particleSystem;

        public SliceEffects(TrailRenderer trailRenderer, Renderer renderer, ParticleSystem particleSystem)
        {
            _trailRenderer = trailRenderer;
            _renderer = renderer;
            _particleSystem = particleSystem;
        }
        // create a scriptable object then specify the colors from it


        public void RemoveTrailerEffect()
        {
            _trailRenderer.enabled = false;
        }

        public void UpdateSliceColor(int score)
        {
            var material = _renderer.material;
            material.color = score switch
            {
                4 => Color.yellow,
                8 => Color.red,
                16 => Color.green,
                32 => Color.cyan,
                _ => material.color
            };
        }
    }
}