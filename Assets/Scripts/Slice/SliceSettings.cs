using UnityEngine;

namespace SemihCelek.SliceMerge.Slice
{
    [CreateAssetMenu(fileName = "SliceSettings", menuName = "ScriptableObjects/SliceSettings", order = 0)]
    public class SliceSettings : ScriptableObject
    {
        public float MoveSpeed;
    }
}