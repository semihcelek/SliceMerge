using System;
using SemihCelek.SliceMerge.Slice;
using UnityEngine;

namespace SemihCelek.SliceMerge.Utilities
{
    public class SliceMounter : MonoBehaviour
    {

        private SliceContainer.SliceContainer currentContainer;

        private void Awake()
        {

            currentContainer = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            
            var isContainer = other.TryGetComponent(out SliceContainer.SliceContainer sliceContainer);

            if (isContainer)
            {
                currentContainer = sliceContainer;
            }

            var isSlice = other.TryGetComponent(out ISliceController sliceController);

            if(!isSlice) return;

            // if (sliceMovementController.ArrivedCondition == true) return;
            
            currentContainer.HandleMount(sliceController);
        }
    }
}