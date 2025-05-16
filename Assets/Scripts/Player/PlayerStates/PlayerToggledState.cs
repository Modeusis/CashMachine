using System.Collections.Generic;
using System.Linq;
using Interactables;
using Player.Camera;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.FSM;
using CameraType = Player.Camera.CameraType;

namespace Player.PlayerStates
{
    public class PlayerToggledState : State
    {
        private readonly BaseInput _input;

        private readonly List<PlayerCamera> _cameras;
        
        private readonly float _raycastDistance;
        private readonly LayerMask _toggleLayerMask;
        
        private PlayerCamera _currentCamera;
        
        private UnityEngine.Camera _mainCamera;

        private IInteractable _lastCalledInteractable;
        
        public PlayerToggledState(StateType stateType, BaseInput input, List<PlayerCamera> cameras, float raycastDistance,
            LayerMask toggleLayerMask)
        {
            StateType = stateType;
            
            _input = input;
            
            _cameras = cameras;
            
            _mainCamera = UnityEngine.Camera.main;
            
            _raycastDistance = raycastDistance;
            _toggleLayerMask = toggleLayerMask;
        }  
        
        public override void Enter()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            HandleCameraChange(CameraType.Screen);
        }

        public override void Update()
        {
            if (_input.main.Move.WasPressedThisFrame())
            {
                HandleMove(_input.main.Move.ReadValue<Vector2>());
            }
            
            HandleRaycast();
            HandleInteraction();
        }

        public override void Exit()
        {
            
        }
        
        private void HandleMove(Vector2 input)
        {
            for (int i = 0; i < _currentCamera.AvailableCameras.Count; i++)
            {
                if (input == _currentCamera.AvailableCameras[i].CameraInput)
                {
                    HandleCameraChange(_currentCamera.AvailableCameras[i].CameraType);
                    
                    return;
                }
            }
        }
        
        private void HandleCameraChange(CameraType cameraType)
        {
            var newCamera = _cameras.Find(camera => camera.CameraType == cameraType);
            
            if (newCamera == null)
            {
                return;   
            }
            
            _currentCamera = newCamera;
            
            _currentCamera.Activate();

            var inactiveCameras = _cameras
                .Where(cam => cam.CameraType != _currentCamera.CameraType);
            
            foreach (var camera in inactiveCameras)
            {
                camera.Deactivate();
            }
        }
        
        private void HandleRaycast()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out RaycastHit hit, _raycastDistance, _toggleLayerMask))
            {
                _lastCalledInteractable?.Unfocus();
                
                _lastCalledInteractable = null;
                
                return;
            }
                

            if (!hit.collider.TryGetComponent(out IInteractable interactable))
            {
                _lastCalledInteractable?.Unfocus();
                
                _lastCalledInteractable = null;
                
                return;
            }

            if (_lastCalledInteractable != interactable)
            {
                _lastCalledInteractable?.Unfocus();

                _lastCalledInteractable = null;
            }
            
            if (_lastCalledInteractable != null)
                return;
            
            _lastCalledInteractable = interactable;
                
            _lastCalledInteractable?.Focus();

        }
        
        private void HandleInteraction()
        {
            if (!_input.main.Click.WasPressedThisFrame())
                return;
            
            _lastCalledInteractable?.Interact();
        }
    }
}