using CashMachine.Screens;
using Utilities.EventBus;
using Utilities.FSM;

namespace CashMachine.CashMachineStates
{
    public class InsertCardCashMachineState : State
    {
        private readonly EventBus _eventBus;
        
        private Card _currentCard;
        
        public InsertCardCashMachineState(StateType stateType, EventBus eventBus)
        {
            StateType = stateType;

            _eventBus = eventBus;
        }
        
        public override void Enter()
        {
            _eventBus.Subscribe<ButtonType>(HandleButtonInput);
            _eventBus.Subscribe<Card>(HandleCardInsert);
        }

        public override void Update()
        {
            if (_currentCard == null)
                return;

            if (_currentCard.IsInserted())
            {
                _eventBus.Publish(ScreenType.InputPin);
            }
        }

        public override void Exit()
        {
            _eventBus.Unsubscribe<ButtonType>(HandleButtonInput);
        }

        private void HandleButtonInput(ButtonType buttonType)
        {
            if (buttonType == ButtonType.ScreenButton14)
            {
                _eventBus.Publish(new ToPrevious());
                
                return;
            }
        }
        
        private void HandleCardInsert(Card card)
        {
            _currentCard = card;
            
            card.InsertCard();
        }
    }
}