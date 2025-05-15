using Unity.Cinemachine;
using UnityEngine;
using Utilities.FSM;

namespace Player.PlayerStates
{
    public class PlayerFirstPersonState : State
    {
        private readonly BaseInput _input;
        
        private readonly CinemachineCamera _firstPersonCamera;
        
        private readonly CharacterController _playerController;
        
        public PlayerFirstPersonState(StateType stateType, BaseInput input, CinemachineCamera playerCamera,
            CharacterController playerController)
        {
            StateType = stateType;
            
            _input = input;
            
            _firstPersonCamera = playerCamera;
            
            _playerController = playerController;
        }

        public override void Enter()
        {
            _firstPersonCamera.gameObject.SetActive(true);
        }

        public override void Update()
        {
            HandleMove(_input.main.Move.ReadValue<Vector2>());
        }

        public override void Exit()
        {
            _firstPersonCamera.gameObject.SetActive(false);
        }

        private void HandleMove(Vector2 move)
        {
            _playerController.SimpleMove(new Vector3(move.x, 0, move.y));
        }

        private void HandleInteract()
        {
            
        }
    }
}