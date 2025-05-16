using UnityEngine;

namespace CashMachine
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private int cardPin;
        
        [SerializeField] private float startBalance;
        
        private float _currentBalance;
        
        private bool _isInserted = false;
        
        public string GetPin() => cardPin.ToString();
        public float CurrentBalance() => _currentBalance;
        public bool IsInserted() => _isInserted;

        private void Awake()
        {
            _currentBalance = startBalance;
        }
        
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