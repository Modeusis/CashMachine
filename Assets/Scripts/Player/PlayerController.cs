using System.Collections.Generic;
using NUnit.Framework;
using Player.Camera;
using Player.PlayerStates;
using Unity.Cinemachine;
using UnityEngine;
using Utilities.EventBus;
using Utilities.FSM;
using Zenject;
using CameraType = Player.Camera.CameraType;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private BaseInput _input;
        
        private EventBus _eventBus;
        
        [Header("Dependencies")]
        [SerializeField] private CharacterController playerController;

        [SerializeField] private CinemachineCamera playerCamera;
        
        [SerializeField] private List<PlayerCamera> cashMachineCameras;
        private IReadOnlyList<PlayerCamera> Cameras => cashMachineCameras;
        
        [Header("Settings")]
        [SerializeField] private PlayerCharacteristics characteristics;

        private FSM _fsm;
        
        private CameraType _currentCameraType;
        
        [Inject]
        private void Initialize(BaseInput input, EventBus eventBus)
        {
            _input = input;
            
            _eventBus = eventBus;
            _eventBus.Subscribe<CameraType>(HandleCameraChange);

            
            
            var states = new Dictionary<StateType, State>()
            {
                { StateType.Idle, new PlayerFirstPersonState(StateType.Idle, _input, playerCamera, playerController) },
                { StateType.Active, new PlayerToggledState(StateType.Active, _input, Cameras) },
                { StateType.Locked, new PlayerLockedState(StateType.Locked) },
            };

            var transitions = new List<Transition>()
            {
                
            };
            
            _fsm = new FSM(states, transitions, StateType.Idle);
        }

        private void Update()
        {
            _fsm?.Update();
        }

        private void LateUpdate()
        {
            _fsm?.LateUpdate();
        }

        private void HandleCameraChange(CameraType cameraType)
        {
            if (cameraType == _currentCameraType)
                return;
            
            _currentCameraType = cameraType;
        }
    }
}