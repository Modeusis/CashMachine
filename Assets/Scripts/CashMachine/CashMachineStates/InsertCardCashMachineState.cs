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
            _eventBus.Subscribe<Card>(HandleCardInsert);
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
            if (buttonType == ButtonType.ScreenButton14)
            {
                _eventBus.Publish(ScreenType.Idle);
            }
        }
        
        private void HandleCardInsert(Card card)
        {
            card.InsertCard();
            
            _eventBus.Publish(ScreenType.InputPin);
        }
    }
}