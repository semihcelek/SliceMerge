using System;
using SemihCelek.SliceMerge.SliceContainer;
using UnityEngine;

namespace SemihCelek.SliceMerge.Utilities
{
    public class SliceGeneratorController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _slicePrefab;

        private void GenerateRandomSlice()
        {
            var slice = Instantiate(_slicePrefab);
        }

        private void Awake()
        {
            FullContainerState.OnGenerateSlice += GenerateRandomSlice;
        }

        private void OnDestroy()
        {
            FullContainerState.OnGenerateSlice -= GenerateRandomSlice;
        }
    }


    public delegate void SliceGenerationAction();
}