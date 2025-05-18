using System;
using System.Collections.Generic;
using System.Linq;
using Animations;
using CashMachine.CashMachineStates;
using CashMachine.Screens;
using TMPro;
using UnityEngine;
using Utilities.EventBus;
using Utilities.FSM;
using Zenject;

namespace CashMachine
{
    public class CashMachineController : MonoBehaviour
    {
        [SerializeField] private List<ScreenSetup> screens;

        [Header("Card")]
        [SerializeField] private Card card;
        
        [Header("Input fields")]
        [SerializeField] private TMP_Text pinView;
        [SerializeField] private TMP_Text moneyView;
        [SerializeField] private TMP_Text balanceView;
        
        [Header("Error messages")]
        [SerializeField] private TMP_Text idleErrorField;
        [SerializeField] private TMP_Text getMoneyErrorField;
        [SerializeField] private TMP_Text pinErrorField;
        
        [Header("Settings")]
        [SerializeField] private int pinAttemptsAmount = 3;
        
        [Header("Animation handlers")]
        [SerializeField] private MoneyAnimationHandler moneyAnimationHandler;
        [SerializeField] private ChequeAnimationHandle chequeAnimationHandler;
        
        private EventBus _eventBus;
        
        private FSM _cashMachineFSM;

        private ScreenSetup _currentScreen;
        
        [Inject]
        private void Initialize(EventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<ScreenType>(HandleScreenChange);
            
            HandleScreenChange(ScreenType.Idle);
            
            var states = new Dictionary<StateType, State>()
            {
                { StateType.Idle, new IdleCashMachineState(StateType.Idle, _eventBus, idleErrorField, card, moneyAnimationHandler, chequeAnimationHandler)},
                { StateType.InsertCard, new InsertCardCashMachineState(StateType.InsertCard, _eventBus, card)},
                { StateType.InputPin, new InputPinCashMachineState(StateType.InputPin, _eventBus, pinView, pinErrorField, card, pinAttemptsAmount)},
                { StateType.ChooseOperation, new ChooseOperationCashMachineState(StateType.ChooseOperation, _eventBus)},
                { StateType.GetBalance, new GetBalanceCashMachineState(StateType.GetBalance, _eventBus, balanceView, card, chequeAnimationHandler)},
                { StateType.GetMoney, new GetMoneyCashMachineState(StateType.GetMoney, _eventBus, moneyView, getMoneyErrorField, card, moneyAnimationHandler)},
                { StateType.Finish, new FinishCashMachineState(StateType.Finish, _eventBus)},
            };

            var transitions = new List<Transition>()
            {
                new(StateType.Idle, StateType.InsertCard, () => _currentScreen.ScreenType == ScreenType.InsertCard),
                
                new(StateType.InsertCard, StateType.InputPin, () => _currentScreen.ScreenType == ScreenType.InputPin),
                new(StateType.InsertCard, StateType.Idle, () => _currentScreen.ScreenType == ScreenType.Idle),
                
                new(StateType.InputPin, StateType.ChooseOperation, () => _currentScreen.ScreenType == ScreenType.ChooseOperation),
                new(StateType.InputPin, StateType.Idle, () => _currentScreen.ScreenType == ScreenType.Idle),
                
                new(StateType.ChooseOperation, StateType.GetBalance, () => _currentScreen.ScreenType == ScreenType.CheckBalance),
                new(StateType.ChooseOperation, StateType.GetMoney, () => _currentScreen.ScreenType == ScreenType.GetMoney),
                new(StateType.ChooseOperation, StateType.Idle, () => _currentScreen.ScreenType == ScreenType.Idle),
                
                new(StateType.GetMoney, StateType.Finish, () => _currentScreen.ScreenType == ScreenType.Finish),
                new(StateType.GetMoney, StateType.ChooseOperation, () => _currentScreen.ScreenType == ScreenType.ChooseOperation),
                
                new(StateType.GetBalance, StateType.Idle, () => _currentScreen.ScreenType == ScreenType.Idle),
                new(StateType.GetBalance, StateType.ChooseOperation, () => _currentScreen.ScreenType == ScreenType.ChooseOperation),
                
                new(StateType.Finish, StateType.Idle, () => _currentScreen.ScreenType == ScreenType.Idle),
                new(StateType.Finish, StateType.ChooseOperation, () => _currentScreen.ScreenType == ScreenType.ChooseOperation),
            };

            _cashMachineFSM = new FSM(states, transitions, StateType.Idle);
        }

        private void Update()
        {
            _cashMachineFSM?.Update();
        }

        private void LateUpdate()
        {
            _cashMachineFSM?.LateUpdate();
        }

        private void HandleScreenChange(ScreenType screenType)
        {
            var newScreen = screens.First(screen => screen.ScreenType == screenType);
            
            _currentScreen?.Deactivate();
            
            _currentScreen = newScreen;
            
            _currentScreen?.Activate();
        }
    }
}