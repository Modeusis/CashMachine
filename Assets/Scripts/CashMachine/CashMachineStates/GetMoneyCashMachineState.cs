using Animations;
using CashMachine.Screens;
using TMPro;
using UnityEngine;
using Utilities.EventBus;
using Utilities.FSM;

namespace CashMachine.CashMachineStates
{
    public class GetMoneyCashMachineState : State
    {
        private readonly EventBus _eventBus;

        private readonly Card _card;
        
        private readonly TMP_Text _moneyTextField;
        private readonly TMP_Text _errorTextField;

        private readonly MoneyAnimationHandler _moneyAnimationHandler;
        
        private float _moneyInput;
        
        private string CurrentValue
        {
            get => _moneyInput.ToString();
            set
            {
                Debug.Log(value);
                
                if (string.IsNullOrEmpty(value))
                {
                    _moneyInput = 0;
                    
                    _moneyTextField.text = "Введите сумму";
                    
                    return;
                }
                
                _moneyInput = float.Parse(value);
                
                _moneyTextField.text = value + " BYN";
            }
        }
        
        public GetMoneyCashMachineState(StateType stateType, EventBus eventBus, TMP_Text moneyTextField, TMP_Text errorTextField,
            Card card, MoneyAnimationHandler moneyAnimationHandler)
        {
            StateType = stateType;

            _eventBus = eventBus;
            
            _moneyTextField = moneyTextField;
            _errorTextField = errorTextField;
            
            _moneyAnimationHandler = moneyAnimationHandler;
            
            _card = card;
        }
        
        public override void Enter()
        {
            CurrentValue = "";
            _errorTextField.text = "";
            
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
            if (buttonType == ButtonType.NumCancel)
            {
                CurrentValue = "";
                
                return;
            }
            
            if (buttonType == ButtonType.NumClear)
            {
                CurrentValue = CurrentValue.Remove(CurrentValue.Length - 1);
                
                return;
            }
            
            if (buttonType == ButtonType.ScreenButton14)
            {
                _eventBus.Publish(ScreenType.ChooseOperation);
                
                return;
            }
            
            if (buttonType == ButtonType.NumConfirm || buttonType == ButtonType.ScreenButton24)
            {
                if (!ValidateInput())
                {
                    _errorTextField.text = "Недостаточно средств";
                    
                    return;
                }

                _moneyAnimationHandler.SetMoney(_card.GetMoney(_moneyInput));
                _moneyAnimationHandler.HandleMoneyCall();
                
                _eventBus.Publish(ScreenType.Finish);
            }

            var tempValue = GetNumFromButton(buttonType);

            if (string.IsNullOrEmpty(tempValue))
            {
                return;
            }
                
            
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

        private bool ValidateInput()
        {
            if (_card.CurrentBalance() < _moneyInput)
            {
                return false;
            }
            
            return true;
        }
    }
}