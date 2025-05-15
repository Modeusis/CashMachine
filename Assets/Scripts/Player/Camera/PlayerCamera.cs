using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace Player.Camera
{
    [Serializable]
    public class PlayerCamera
    {
        [field: SerializeField] public CameraType CameraType { get; private set; }
        
        [field: SerializeField] public CinemachineCamera Camera { get; private set; }
        
        [SerializeField] private List<AvailableCameras> availableCameras;
        
        public IReadOnlyList<AvailableCameras> AvailableCameras => availableCameras;
        
        public void Activate()
        {
            Camera.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            Camera.gameObject.SetActive(false);
        }
    }

    [Serializable]
    public class AvailableCameras
    {
        [field: SerializeField] public CameraType CameraType { get; private set; }
        
        [field: SerializeField] public Vector2 CameraInput { get; private set; }
    }
}