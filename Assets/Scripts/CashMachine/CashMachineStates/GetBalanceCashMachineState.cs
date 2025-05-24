using Animations;
using CashMachine.Screens;
using Interactables;
using Sounds;
using TMPro;
using Utilities.EventBus;
using Utilities.FSM;

namespace CashMachine.CashMachineStates
{
    public class GetBalanceCashMachineState : State
    {
        private readonly EventBus _eventBus;
        
        private readonly SoundService _soundService;

        private readonly string _errorSoundId;
        
        private readonly Card _card;
        
        private readonly TMP_Text _cardBalanceText;

        private readonly ChequeAnimationHandle _cheque;
        
        private string CardBalance
        {
            get => _cardBalanceText.text;
            set
            {
                _cardBalanceText.text = value + " BYN";
            }
        }
        
        public GetBalanceCashMachineState(StateType stateType, EventBus eventBus, TMP_Text balanceView, Card card,
            ChequeAnimationHandle cheque, SoundService soundService, string errorSoundId)
        {
            StateType = stateType;

            _eventBus = eventBus;
            
            _soundService = soundService;
            _errorSoundId = errorSoundId;
            
            _card = card;
            
            _cardBalanceText = balanceView;
            
            _cheque = cheque;
        }
        
        public override void Enter()
        {
            _eventBus.Subscribe<ButtonType>(HandleButtonInput);
            
            CardBalance = _card.CurrentBalance().ToString();
            
            _eventBus.Publish(InteractionType.CheckBalance);
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
            if (buttonType == ButtonType.ScreenButton14 && !_cheque.IsReady())
            {
                _soundService.Play(SoundType.Sound, _errorSoundId);
                
                return;
            }
            
            if (buttonType == ButtonType.ScreenButton14 && _cheque.IsReady())
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