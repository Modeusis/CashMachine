using CashMachine.Screens;
using Utilities.EventBus;
using Utilities.FSM;

namespace CashMachine.CashMachineStates
{
    public class InsertCardCashMachineState : State
    {
        private readonly EventBus _eventBus;
        
        private readonly Card _card;
        
        public InsertCardCashMachineState(StateType stateType, EventBus eventBus, Card card)
        {
            StateType = stateType;

            _eventBus = eventBus;
            
            _card = card;
        }
        
        public override void Enter()
        {
            _eventBus.Subscribe<ButtonType>(HandleButtonInput);

            _card?.SetReady(true);
        }

        public override void Update()
        {
            if (_card.IsInserted())
            {
                _eventBus.Publish(ScreenType.InputPin);
            }
        }

        public override void Exit()
        {
            _eventBus.Unsubscribe<ButtonType>(HandleButtonInput);
            
            _card?.SetReady(false);
        }

        private void HandleButtonInput(ButtonType buttonType)
        {
            if (buttonType == ButtonType.ScreenButton14)
            {
                _eventBus.Publish(ScreenType.Idle);
            }
        }
    }
}