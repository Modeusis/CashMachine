using System;
using System.Collections.Generic;
using System.Linq;
using CashMachine.CashMachineStates;
using CashMachine.Screens;
using UnityEngine;
using Utilities.EventBus;
using Utilities.FSM;
using Zenject;

namespace CashMachine
{
    public class CashMachineController : MonoBehaviour
    {
        [SerializeField] private List<ScreenSetup> screens;

        [SerializeField] private Card card;
        
        private EventBus _eventBus;
        
        private FSM _cashMachineFSM;
        
        private ScreenSetup _previousScreen;
        private ScreenSetup _currentScreen;
        
        [Inject]
        private void Initialize(EventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<ScreenType>(HandleScreenChange);
            _eventBus.Subscribe<ToPrevious>(HandleToPreviousScreen);
            
            var states = new Dictionary<StateType, State>()
            {
                { StateType.Idle, new IdleCashMachineState(StateType.Idle, _eventBus)},
                { StateType.InsertCard, new InsertCardCashMachineState(StateType.InsertCard, _eventBus, card)},
                { StateType.InputPin, new InputPinCashMachineState(StateType.InputPin, _eventBus)},
                { StateType.ChooseOperation, new ChooseOperationCashMachineState(StateType.ChooseOperation, _eventBus)},
                { StateType.GetBalance, new GetBalanceCashMachineState(StateType.GetBalance, _eventBus)},
                { StateType.GetMoney, new GetMoneyCashMachineState(StateType.GetMoney, _eventBus)},
                { StateType.Finish, new FinishCashMachineState(StateType.Finish, _eventBus)},
            };

            var transitions = new List<Transition>()
            {
                new(StateType.Idle, StateType.InsertCard, () => _currentScreen.ScreenType == ScreenType.InsertCard),
                new(StateType.InsertCard, StateType.InputPin, () => _currentScreen.ScreenType == ScreenType.InputPin),
                new(StateType.InputPin, StateType.ChooseOperation, () => _eventBus.WasCalledThisFrame<Card>()),
                new(StateType.ChooseOperation, StateType.GetBalance, () => _currentScreen.ScreenType == ScreenType.CheckBalance),
                new(StateType.ChooseOperation, StateType.GetMoney, () => _currentScreen.ScreenType == ScreenType.GetMoney),
                new(StateType.GetMoney, StateType.Finish, () => _currentScreen.ScreenType == ScreenType.Finish),
                new(StateType.GetBalance, StateType.Idle, () => _currentScreen.ScreenType == ScreenType.Idle),
                new(StateType.Any, StateType.Previous, () => _eventBus.WasCalledThisFrame<ToPrevious>())
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
            if (screenType == _currentScreen.ScreenType)
                return;
            
            var newScreen = screens.First(screen => screen.ScreenType == screenType);
            
            _currentScreen.Deactivate();

            _previousScreen = _currentScreen;
            _currentScreen = newScreen;
            
            _currentScreen.Activate();
        }

        private void HandleToPreviousScreen(ToPrevious toPrevious)
        {
            if (_previousScreen == null)
                return;
            
            _currentScreen.Deactivate();
            
            _currentScreen = _previousScreen;
            
            _currentScreen.Activate();
        }
    }
}