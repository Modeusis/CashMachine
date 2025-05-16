using CashMachine.Screens;
using Utilities.EventBus;
using Utilities.FSM;

namespace CashMachine.CashMachineStates
{
    public class ChooseOperationCashMachineState : State
    {
        private readonly EventBus _eventBus;
        
        public ChooseOperationCashMachineState(StateType stateType, EventBus eventBus)
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
            if (buttonType == ButtonType.ScreenButton13)
            {
                _eventBus.Publish(new ToPrevious());
                
                return;
            }
            
            CallOperation(buttonType);
        }
        
        private void CallOperation(ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.ScreenButton14:
                {
                    _eventBus.Publish(ScreenType.GetMoney);
                    
                    break;
                }
                case ButtonType.ScreenButton24:
                {
                    _eventBus.Publish(ScreenType.CheckBalance);
                    
                    break;
                }
            }
        }
    }
}