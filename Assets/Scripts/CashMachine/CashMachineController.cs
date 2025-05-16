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

        private BaseInput _input;
        
        private EventBus _eventBus;
        
        private FSM _cashMachineFSM;
        
        private ScreenSetup _currentScreen;
        
        [Inject]
        private void Initialize(BaseInput input, EventBus eventBus)
        {
            _input = input;
            
            _eventBus = eventBus;
            _eventBus.Subscribe<ScreenType>(HandleScreenChange);
            
            var states = new Dictionary<StateType, State>()
            {
                { StateType.Idle , new IdleCashMachineState(StateType.Idle, _eventBus)},
                { StateType.InsertCard , new InsertCardCashMachineState(StateType.InsertCard)},
                { StateType.InputPin , new InputPinCashMachineState(StateType.InputPin)},
                { StateType.ChooseOperation , new ChooseOperationCashMachineState(StateType.ChooseOperation)},
                { StateType.GetBalance , new GetBalanceCashMachineState(StateType.GetBalance)},
                { StateType.GetMoney , new GetMoneyCashMachineState(StateType.GetMoney)},
                { StateType.Finish , new FinishCashMachineState(StateType.Finish)},
            };

            var transitions = new List<Transition>()
            {
                new(StateType.Idle, StateType.InsertCard, () => _currentScreen.ScreenType == ScreenType.InsertCard),
                new(StateType.InsertCard, StateType.InputPin, () => _currentScreen.ScreenType == ScreenType.InputPin),
                new(StateType.InputPin, StateType.ChooseOperation, () => _currentScreen.ScreenType == ScreenType.ChooseOperation),
                new(StateType.ChooseOperation, StateType.GetBalance, () => _currentScreen.ScreenType == ScreenType.CheckBalance),
                new(StateType.ChooseOperation, StateType.GetMoney, () => _currentScreen.ScreenType == ScreenType.GetMoney),
                new(StateType.GetMoney, StateType.Finish, () => _currentScreen.ScreenType == ScreenType.Finish),
                new(StateType.Any, StateType.Idle, () => _currentScreen.ScreenType == ScreenType.Idle),
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
            
            _currentScreen.Deactivate();
            
            _currentScreen = newScreen;
            
            _currentScreen.Activate();
        }
    }
}