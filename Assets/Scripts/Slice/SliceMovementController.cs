using System;
using System.Collections;
using System.Collections.Generic;
using SemihCelek.SliceMerge.PlayerInput;
using UnityEngine;

namespace SemihCelek.SliceMerge.Slice
{
    public class SliceMovementController : MonoBehaviour
    {
        private Transform _cachedTransform;

        [SerializeField]
        private Transform _targetPosition;

        public bool ArrivedCondition;

        private void Awake()
        {
            ArrivedCondition = false;
            _cachedTransform = transform;
            InputHandler.OnClickFireButton += OnFireButton;
        }

        private void Start()
        {
        }

        private void OnFireButton()
        {
            StartCoroutine(MoveSliceToCircleCoroutine());
            InputHandler.OnClickFireButton -= OnFireButton;
        }


        private IEnumerator MoveSliceToCircleCoroutine()
        {
            var time = 0.6f;

            var startPosition = _cachedTransform.position;
            var destinationPosition = _targetPosition.position;

            var elapsedTime = 0f;

            while (elapsedTime < time)
            {
                _cachedTransform.position =
                    Vector3.Lerp(startPosition, destinationPosition, (elapsedTime / time));
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            ArrivedCondition = true;
        }
    }
}