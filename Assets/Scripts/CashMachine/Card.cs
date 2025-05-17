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
        }

        public bool GetMoney(float value)
        {
            if (value > _currentBalance)
                return false;
            
            _currentBalance -= value;

            return true;
        }
        
        public void RemoveCard()
        {
            _isInserted = false;
        }
        
        public bool IsInserted() => _isInserted;
        public bool IsReady() => _isReadyToInsert;
        
        public float CurrentBalance() => _currentBalance;
        
        public string GetPin() => cardPin.ToString();
    }
}