using CashMachine.Screens;
using Interactables;
using TMPro;
using Utilities.EventBus;
using Utilities.FSM;

namespace CashMachine.CashMachineStates
{
    public class IdleCashMachineState : State
    {
        private readonly EventBus _eventBus;
        
        private readonly ScreenSetup _screenSetup;

        private readonly TMP_Text _errorText;
        
        private readonly Card _card;
        
        public IdleCashMachineState(StateType stateType, EventBus eventBus, TMP_Text errorText, Card card)
        {
            StateType = stateType;
            
            _eventBus = eventBus;
            
            _card = card;
            
            _errorText = errorText;
        }
        
        public override void Enter()
        {
            _errorText.text = string.Empty;
            
            _eventBus.Subscribe<ButtonType>(HandleScreenChange);
            
            if (_card.IsInserted())
            {
                _eventBus.Publish(InteractionType.RemoveCard);
            }
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
            if (buttonType == ButtonType.ScreenButton14 && _card.IsTaken())
            {
                _eventBus.Publish(ScreenType.InsertCard);
                
                return;
            }

            _errorText.text = "Заберите карту перед тем как продолжить";
        }
    }
}