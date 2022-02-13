using UnityEngine;

namespace SemihCelek.SliceMerge.SliceContainer
{
    [CreateAssetMenu(fileName = "SliceContainerSettings", menuName = "ScriptableObjects/SliceContainerSettings", order = 0)]
    public class SliceContainerSettings : ScriptableObject
    {
        public float MoveSpeed;
    }
}