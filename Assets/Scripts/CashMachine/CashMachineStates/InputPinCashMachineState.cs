using CashMachine.Screens;
using TMPro;
using UnityEngine;
using Utilities.EventBus;
using Utilities.FSM;

namespace CashMachine.CashMachineStates
{
    public class InputPinCashMachineState : State
    {
        private readonly EventBus _eventBus;

        private Card _card;
        
        private TMP_Text _currentPinView;
        
        private string _currentPin;

        private string CurrentPin
        {
            get => _currentPin;
            set
            {
                _currentPin = value;

                _currentPinView.text = GetPinView(_currentPin.Length);
            }
        }
        
        public InputPinCashMachineState(StateType stateType, EventBus eventBus, Card card)
        {
            StateType = stateType;

            _eventBus = eventBus;
            
            _card = card;
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
            
            CurrentPin = string.Empty;
        }

        private void HandleButtonInput(ButtonType buttonType)
        {
            if (buttonType == ButtonType.ScreenButton14)
            {
                _eventBus.Publish(ScreenType.Idle);
                
                return;
            }

            if (buttonType == ButtonType.NumClear)
            {
                CurrentPin = CurrentPin.Remove(CurrentPin.Length - 1);
                
                return;
            }

            if (buttonType == ButtonType.NumCancel)
            {
                CurrentPin = string.Empty;
                
                return;
            }
            
            if (buttonType == ButtonType.NumConfirm && ValidatePin())
            {
                _eventBus.Publish(ScreenType.ChooseOperation);
                
                return;
            }
            
            CurrentPin += GetNumFromButton(buttonType);
        }

        private bool ValidatePin()
        {
            if (_card.GetPin() != _currentPin)
                return false;
            
            return true;
        }

        private string GetPinView(int pinSize)
        {
            var pinView = "";

            if (pinSize <= 0)
                return pinView;
            
            for (int i = 0; i < pinSize; i++)
            {
                pinView += "*";
            }

            return pinView;
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