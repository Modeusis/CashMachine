using Animations;
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
        
        private readonly MoneyAnimationHandler _money;
        
        private readonly ChequeAnimationHandle _cheque;
        
        public IdleCashMachineState(StateType stateType, EventBus eventBus, TMP_Text errorText, Card card,
            MoneyAnimationHandler moneyAnimationHandler, ChequeAnimationHandle chequeAnimationHandle)
        {
            StateType = stateType;
            
            _eventBus = eventBus;
            
            _card = card;
            
            _errorText = errorText;
            
            _money = moneyAnimationHandler;
            
            _cheque = chequeAnimationHandle;
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
            if (buttonType == ButtonType.ScreenButton14 && _card.IsTaken() && _cheque.IsReady() && _money.IsReady())
            {
                _eventBus.Publish(ScreenType.InsertCard);
                
                return;
            }

            _errorText.text = "Заберите чек, деньги и карту с прошлой операции, перед тем как продолжить";
        }
    }
}