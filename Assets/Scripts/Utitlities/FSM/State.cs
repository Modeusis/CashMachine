namespace Utilities.FSM
{
    public abstract class State
    {
        public StateType StateType { get; protected set; }
        
        public abstract void Enter();
        
        public abstract void Update();
        
        public abstract void Exit();
    }
}