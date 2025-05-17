using CashMachine.Screens;
using TMPro;
using Utilities.EventBus;
using Utilities.FSM;

namespace CashMachine.CashMachineStates
{
    public class GetBalanceCashMachineState : State
    {
        private readonly EventBus _eventBus;
        
        private readonly Card _card;
        
        private readonly TMP_Text _cardBalanceText;

        private string CardBalance
        {
            get => _cardBalanceText.text;
            set
            {
                _cardBalanceText.text = value + " BYN";
            }
        }
        
        public GetBalanceCashMachineState(StateType stateType, EventBus eventBus, TMP_Text balanceView, Card card)
        {
            StateType = stateType;

            _eventBus = eventBus;
            
            _card = card;
            
            _cardBalanceText = balanceView;
        }
        
        public override void Enter()
        {
            _eventBus.Subscribe<ButtonType>(HandleButtonInput);
            
            CardBalance = _card.CurrentBalance().ToString();
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
                _eventBus.Publish(ScreenType.ChooseOperation);
                
                return;
            }
            
            if (buttonType == ButtonType.ScreenButton24)
            {
                _eventBus.Publish(ScreenType.Idle);
            }
        }
    }
}