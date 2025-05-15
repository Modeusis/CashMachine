using System;

namespace Utilities.FSM
{
    public class Transition
    {
        public StateType From { get;  private set; }
        
        public StateType To { get; private set; }
        
        public Func<bool> Condition { get; private set; }

        public Transition(StateType from, StateType to, Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}