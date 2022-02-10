using UnityEngine;

namespace SemihCelek.SliceMerge.Utilities
{
    public class CircleController : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed;

        private Transform _cachedTransform;
        
        private void Awake()
        {
            _cachedTransform = transform;
        }

        private void Update()
        {
            _cachedTransform.Rotate(0f,_rotationSpeed * Time.deltaTime,0f);
        }
    }
}