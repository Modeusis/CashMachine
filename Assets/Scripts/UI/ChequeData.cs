using System;

namespace UI
{
    public struct ChequeData
    {
        public DateTime Date { get; private set; }
        
        public string Operation { get; private set; }

        public ChequeData(DateTime date, string operation)
        {
            Date = date;
            
            Operation = operation;
        }
    }
}