using System.Collections.Generic;
using Player.Camera;
using Utilities.FSM;

namespace Player.PlayerStates
{
    public class PlayerToggledState : State
    {
        private readonly BaseInput _input;

        private readonly IReadOnlyList<PlayerCamera> _cameras;
        
        public PlayerToggledState(StateType stateType, BaseInput input, IReadOnlyList<PlayerCamera> cameras)
        {
            StateType = stateType;
            
            _input = input;
            
            _cameras = cameras;
        }  
        
        public override void Enter()
        {
            
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}