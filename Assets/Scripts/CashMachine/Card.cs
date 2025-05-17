using UnityEngine;

namespace CashMachine
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private int cardPin;
        
        [SerializeField] private float startBalance;
        
        
        private float _currentBalance;
        
        private bool _isReadyToInsert = false;
        private bool _isInserted = false;
        private bool _isTaken = true;

        private void Awake()
        {
            _currentBalance = startBalance;
        }
        
        public void SetReady(bool isReadyToInsert)
        {
            _isReadyToInsert = isReadyToInsert;
        } 
        
        public void InsertCard()
        {
            _isInserted = true;
            
            _isTaken = false;
        }

        public float GetMoney(float value)
        {
            _currentBalance -= value;
            
            return value;
        }
        
        public void RemoveCard()
        {
            _isInserted = false;
        }
        
        public void TakeCard()
        {
            _isTaken = true;
        }
        
        public bool IsInserted() => _isInserted;
        public bool IsReady() => _isReadyToInsert;
        public bool IsTaken() => _isTaken;
        
        public float CurrentBalance() => _currentBalance;
        
        public string GetPin() => cardPin.ToString();
    }
}