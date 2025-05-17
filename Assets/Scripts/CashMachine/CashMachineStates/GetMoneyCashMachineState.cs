using Animations;
using TMPro;
using Utilities.EventBus;
using Utilities.FSM;

namespace CashMachine.CashMachineStates
{
    public class GetMoneyCashMachineState : State
    {
        private readonly EventBus _eventBus;

        private readonly Card _card;

        private readonly MoneyAnimationHandler _moneyHandler;
        
        private readonly TMP_Text _moneyTextField;
        
        private string CurrentValue
        {
            get => _moneyTextField.text;
            set
            {
                _moneyTextField.text = value;
            }
        }
        
        public GetMoneyCashMachineState(StateType stateType, EventBus eventBus, TMP_Text moneyTextField)
        {
            StateType = stateType;

            _eventBus = eventBus;
            
            _moneyTextField = moneyTextField;
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
            // if (buttonType == )

            CurrentValue += GetNumFromButton(buttonType);
        }
        
        private string GetNumFromButton(ButtonType buttonType)
        {
            var key = "";

            switch (buttonType)
            {
                case ButtonType.Num0:
                {
                    key = "0";
                    
                    break;
                }
                case ButtonType.Num000:
                {
                    key = "000";
                    
                    break;
                }
                case ButtonType.Num1:
                {
                    key = "1";
                    
                    break;
                }
                case ButtonType.Num2:
                {
                    key = "2";
                    
                    break;
                }
                case ButtonType.Num3:
                {
                    key = "3";
                    
                    break;
                }
                case ButtonType.Num4:
                {
                    key = "4";
                    
                    break;
                }
                case ButtonType.Num5:
                {
                    key = "5";
                    
                    break;
                }
                case ButtonType.Num6:
                {
                    key = "6";
                    
                    break;
                }
                case ButtonType.Num7:
                {
                    key = "7";
                    
                    break;
                }
                case ButtonType.Num8:
                {
                    key = "8";
                    
                    break;
                }
                case ButtonType.Num9:
                {
                    key = "9";
                    
                    break;
                }
            }
            
            return key;
        }
    }
}