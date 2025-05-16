using CashMachine.Screens;
using Utilities.EventBus;
using Utilities.FSM;

namespace CashMachine.CashMachineStates
{
    public class InsertCardCashMachineState : State
    {
        private readonly EventBus _eventBus;
        
        private Card _currentCard;
        
        public InsertCardCashMachineState(StateType stateType, EventBus eventBus, Card currentCard)
        {
            StateType = stateType;

            _eventBus = eventBus;
            
            _currentCard = currentCard;
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
            if (buttonType == ButtonType.ScreenButton11)
            {
                _eventBus.Publish(new ToPrevious());
                
                return;
            }
            
            CallOperation(buttonType);
        }
        
        private void HandleCardInsert(Card card)
        {
            _currentCard = card;
            
            card.InsertCard();
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