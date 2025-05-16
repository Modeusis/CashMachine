using Interactables;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.FSM;

namespace Player.PlayerStates
{
    public class PlayerFirstPersonState : State
    {
        private readonly BaseInput _input;
        
        private readonly CinemachineCamera _firstPersonCamera;
        
        private readonly CharacterController _playerController;

        private readonly PlayerCharacteristics _stats;

        private readonly LayerMask _firstPersonLayerMask;
        
        private float _currentVerticalAngle;
        
        private IInteractable _lastCalledInteractable;
        
        private UnityEngine.Camera _mainCamera;
        
        public PlayerFirstPersonState(StateType stateType, BaseInput input, CinemachineCamera playerCamera,
            CharacterController playerController, PlayerCharacteristics stats, LayerMask playerLayerMask)
        {
            StateType = stateType;
            
            _input = input;
            
            _firstPersonCamera = playerCamera;
            
            _playerController = playerController;
            
            _stats = stats;
            
            _mainCamera = UnityEngine.Camera.main;
            
            _firstPersonLayerMask = playerLayerMask;
        }

        public override void Enter()
        {
            _firstPersonCamera.gameObject.SetActive(true);
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override void Update()
        {
            HandleMove(_input.main.Move.ReadValue<Vector2>());
            
            HandleCamera(_input.main.CameraMovement.ReadValue<Vector2>());
            
            HandleRaycast();
            HandleInteraction();
        }

        public override void Exit()
        {
            _firstPersonCamera.gameObject.SetActive(false);
        }

        private void HandleMove(Vector2 move)
        {
            Vector3 direction = (_playerController.transform.forward * move.y + 
                                 _playerController.transform.right * move.x).normalized;
    
            Vector3 movement = direction * _stats.Speed;
            
            _playerController.SimpleMove(movement);
        }

        private void HandleCamera(Vector2 cameraInput)
        {
            cameraInput = cameraInput.normalized;
            
            var playerRotate = Vector3.up * (cameraInput.x * _stats.CameraSpeed);
            
            _playerController.transform.Rotate(playerRotate);
            
            float verticalRotation = -cameraInput.y * _stats.CameraSpeed;
            _currentVerticalAngle += verticalRotation;
            _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, -45f, 45f);
            
            _firstPersonCamera.transform.localEulerAngles = new Vector3(
                _currentVerticalAngle,
                0f,
                0f
            );
        }
        
        private void HandleRaycast()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out RaycastHit hit, _stats.RaycastDistance, _firstPersonLayerMask))
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