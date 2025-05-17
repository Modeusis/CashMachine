namespace Utilities.FSM
{
    public enum StateType
    {
        Idle = 0,
        Active = 1,
        Locked = 2,
        InsertCard = 3,
        InputPin = 4,
        ChooseOperation = 5,
        GetBalance = 6,
        GetMoney = 7,
        Finish = 8,
        Any,
    }
}