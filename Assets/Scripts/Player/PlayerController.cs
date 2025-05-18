using System.Collections.Generic;
using Player.Camera;
using Player.PlayerStates;
using TMPro;
using UI;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
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
        
        [Header("RaycastLayers")]
        [SerializeField] private LayerMask firstPersonLayerMask;
        [SerializeField] private LayerMask toggleLayerMask;
        
        [Header("Player UI")]
        [SerializeField] private ChequeTable chequeTable;
        [SerializeField] private Button openButton;
        [SerializeField] private TMP_Text tooltipTextField; 
        
        [Header("Dependencies")]
        [SerializeField] private CharacterController playerController;

        [SerializeField] private CinemachineCamera playerCamera;
        [SerializeField] private CinemachineCamera overiewCamera;
        
        [SerializeField] private List<PlayerCamera> cashMachineCameras;
        
        [Header("Settings")]
        [SerializeField] private PlayerCharacteristics playerCharacteristics;

        private FSM _fsm;
        
        private CameraType _currentCameraType;
        
        [Inject]
        private void Initialize(BaseInput input, EventBus eventBus)
        {
            _input = input;
            
            _eventBus = eventBus;
            
            var states = new Dictionary<StateType, State>()
            {
                { StateType.Idle, new PlayerFirstPersonState(StateType.Idle, _input, playerCamera, playerController, playerCharacteristics, firstPersonLayerMask) },
                { StateType.Active, new PlayerToggledState(StateType.Active, _input, cashMachineCameras, playerCharacteristics.RaycastDistance, toggleLayerMask) },
                { StateType.Locked, new PlayerLockedState(StateType.Locked, overiewCamera, chequeTable, openButton, tooltipTextField) },
            };

            var transitions = new List<Transition>()
            {
                new Transition(StateType.Idle, StateType.Active, () => _eventBus.WasCalledThisFrame<CameraToggle>()),
                new Transition(StateType.Active, StateType.Idle, () => _input.main.Break.WasPressedThisFrame()),
                new Transition(StateType.Any, StateType.Locked, () => _input.main.RightClick.WasPressedThisFrame()),
                new Transition(StateType.Locked, StateType.Idle,() => _input.main.RightClick.WasPressedThisFrame())
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
    }
}