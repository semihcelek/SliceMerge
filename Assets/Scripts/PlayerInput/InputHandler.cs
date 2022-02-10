using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SemihCelek.SliceMerge.PlayerInput
{
    public delegate void PlayerInputAction();

    public class InputHandler : MonoBehaviour
    {
        public static event PlayerInputAction OnClickFireButton;
        
        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                OnClickFireButton?.Invoke();
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                OnClickFireButton?.Invoke();
            }
        }
    }
}