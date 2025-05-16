using System.Collections.Generic;
using UnityEngine;

namespace Utilities.FSM
{
    public class FSM
    {
        private Dictionary<StateType, State> _states;
        
        private List<Transition> _transitions;
        
        private State _previousState;
        private State _currentState;

        public FSM(Dictionary<StateType, State> states, List<Transition> transitions, StateType startingState)
        {
            _states = states;
            
            _transitions = transitions;

            ChangeState(startingState);
        }
        
        public void Update()
        {
            _currentState?.Update();
        }

        public void LateUpdate()
        {
            for (int i = 0; i < _transitions.Count; i++)
            {
                if ((_transitions[i].From == _currentState.StateType || 
                     (_transitions[i].From == StateType.Any && _currentState.StateType != _transitions[i].To)) && _transitions[i].Condition())
                {
                    ChangeState(_transitions[i].To);
                    
                    return;
                }
            }
        }
        
        private void ChangeState(StateType newState)
        {
            if (newState == StateType.Previous)
            {
                if (_previousState == null)
                    return;
                
                var tempPreviousState = _currentState;
                
                _currentState?.Exit();
                
                _currentState = _previousState;
                _previousState = tempPreviousState;
                
                _currentState?.Enter();
                
                return;
            }
            
            if (!_states.TryGetValue(newState, out State state))
            {
                Debug.LogWarning("Cannot change FSM state to null!");
                
                return;
            }
            
            _currentState?.Exit();

            _previousState = _currentState;
            _currentState = state;
                
            _currentState?.Enter();
        }
    }
}