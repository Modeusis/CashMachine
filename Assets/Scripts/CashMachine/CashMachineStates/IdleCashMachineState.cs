using CashMachine.Screens;
using Interactables;
using Utilities.EventBus;
using Utilities.FSM;

namespace CashMachine.CashMachineStates
{
    public class IdleCashMachineState : State
    {
        private readonly EventBus _eventBus;
        
        private readonly ScreenSetup _screenSetup;

        private readonly Card _card;
        
        public IdleCashMachineState(StateType stateType, EventBus eventBus, Card card)
        {
            StateType = stateType;
            
            _eventBus = eventBus;
        }
        
        public override void Enter()
        {
            _eventBus.Subscribe<ButtonType>(HandleScreenChange);

            if (_card.IsInserted())
            {
                _eventBus.Publish(InteractionType.GetCard);
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
            if (buttonType == ButtonType.ScreenButton14)
            {
                _eventBus.Publish(ScreenType.InsertCard);
            }
        }
    }
}