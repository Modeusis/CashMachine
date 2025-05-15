using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerCharacteristics
    {
        [field: SerializeField] public float Speed { get; private set; }
        
        [field: SerializeField] public float CameraSpeed { get; private set; }
    }
}