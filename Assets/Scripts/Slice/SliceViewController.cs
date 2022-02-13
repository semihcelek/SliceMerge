using UnityEngine;

namespace SemihCelek.SliceMerge.Slice
{
    public class SliceViewController : MonoBehaviour
    {
        [SerializeField]
        private Renderer _renderer;

        [SerializeField]
        private TrailRenderer _trailRenderer;

        [SerializeField]
        private ParticleSystem _particleSystem;
        // slice Effect Settings ScriptableObject
        
        public Renderer SliceRenderer
        {
            get => _renderer;
        }

        public TrailRenderer SliceTrailRenderer
        {
            get => _trailRenderer;
        }

        public ParticleSystem SliceParticleSystem
        {
            get => _particleSystem;
        }
    }
}