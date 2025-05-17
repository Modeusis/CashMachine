using System;
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
        
        private readonly int _attemptsAmount;
        
        private readonly Card _card;
        
        private readonly TMP_Text _currentPinView;
        private readonly TMP_Text _errorPinView;
        
        private int _currentAttempts;
        
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
        
        public InputPinCashMachineState(StateType stateType, EventBus eventBus, TMP_Text pinView, TMP_Text errorPinView, Card card, int attemptsAmount)
        {
            StateType = stateType;
            
            _eventBus = eventBus;
            
            _attemptsAmount = attemptsAmount;
            
            _currentPinView = pinView;
            _errorPinView = errorPinView;
            
            _card = card;
        }
        
        public override void Enter()
        {
            _currentAttempts = _attemptsAmount;
            
            _errorPinView.text = string.Empty;
            
            _eventBus.Subscribe<ButtonType>(HandleButtonInput);
            
            CurrentPin = string.Empty;
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
            
            if (buttonType == ButtonType.NumConfirm)
            {
                if (!ValidatePin())
                {
                    if (_currentAttempts <= 0)
                    {
                        _eventBus.Publish(ScreenType.Idle);
                        
                        return;
                    }
                
                    _errorPinView.text = "Неверный пин код, кол-во попыток " + _currentAttempts;
                    
                    _currentAttempts--;
                
                    CurrentPin = String.Empty;
                    
                    return;
                }
                
                _eventBus.Publish(ScreenType.ChooseOperation);
                
                return;
            }

            var tempNum = GetNumFromButton(buttonType);

            if (string.IsNullOrEmpty(tempNum))
            {
                return;
            }
            
            CurrentPin += tempNum;
        }

        private bool ValidatePin()
        {
            if (_card.GetPin() != CurrentPin)
            {
                return false;
            }
            
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