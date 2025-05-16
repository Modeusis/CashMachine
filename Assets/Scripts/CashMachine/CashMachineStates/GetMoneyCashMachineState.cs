using Utilities.EventBus;
using Utilities.FSM;

namespace CashMachine.CashMachineStates
{
    public class GetMoneyCashMachineState : State
    {
        private readonly EventBus _eventBus;
        
        public GetMoneyCashMachineState(StateType stateType, EventBus eventBus)
        {
            StateType = stateType;

            _eventBus = eventBus;
        }
        
        public override void Enter()
        {
            _eventBus.Subscribe<ButtonType>(HandleButtonInput);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            _eventBus.Unsubscribe<ButtonType>(HandleButtonInput);
        }

        private void HandleButtonInput(ButtonType buttonType)
        {
            
        }
    }
}