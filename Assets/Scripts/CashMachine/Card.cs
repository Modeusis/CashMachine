using UnityEngine;

namespace CashMachine
{
    public class Card : MonoBehaviour
    {
        private bool _isInserted = false;
        
        public bool IsInserted => _isInserted;
        
        public void InsertCard()
        {
            _isInserted = true;
        }

        public void RemoveCard()
        {
            _isInserted = false;
        }

        public void TakeCard()
        {
            
        }
    }
}