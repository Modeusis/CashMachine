using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerCharacteristics
    {
        [SerializeField] private float speed = 10f;
        public float Speed => speed * Time.deltaTime;
        
        [SerializeField] public float cameraSpeed = 10f;
        public float CameraSpeed => cameraSpeed * Time.deltaTime;

        [field: SerializeField] public float RaycastDistance { get; private set; } = 3f;
    }
}