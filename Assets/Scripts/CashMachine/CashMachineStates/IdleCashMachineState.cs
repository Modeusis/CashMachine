using CashMachine.Screens;
using Utilities.EventBus;
using Utilities.FSM;

namespace CashMachine.CashMachineStates
{
    public class IdleCashMachineState : State
    {
        private readonly EventBus _eventBus;
        
        private readonly ScreenSetup _screenSetup;
        
        public IdleCashMachineState(StateType stateType, EventBus eventBus)
        {
            StateType = stateType;
            
            _eventBus = eventBus;
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

        private void HandleScreenChange(ButtonType buttonType)
        {
            
        }
    }
}