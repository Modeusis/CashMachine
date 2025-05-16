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
            _eventBus.Subscribe<ButtonType>(HandleScreenChange);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            _eventBus.Unsubscribe<ButtonType>(HandleScreenChange);
        }

        private void HandleScreenChange(ButtonType buttonType)
        {
            if (buttonType == ButtonType.ScreenButton14)
            {
                _eventBus.Publish(ScreenType.InsertCard);
            }
        }
    }
}