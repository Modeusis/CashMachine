using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace Player.Camera
{
    [Serializable]
    public class PlayerCamera
    {
        [field: SerializeField] private CameraType CameraType { get; set; }
        
        [field: SerializeField] private CinemachineCamera Camera { get; set; }
        
        [SerializeField] private List<AvailableCameras> availableCameras;
        
        public IReadOnlyList<AvailableCameras> AvailableCameras => availableCameras;
    }

    [Serializable]
    public class AvailableCameras
    {
        [field: SerializeField] public CameraType CameraType { get; private set; }
        
        [field: SerializeField] public Vector2 CameraInput { get; private set; }
    }
}