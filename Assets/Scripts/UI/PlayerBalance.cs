using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class PlayerBalance : MonoBehaviour
    {
        [Inject] private EventBus _eventBus;
        
        [Header("Balance")]
        [SerializeField] private float playerStartBalance = 0f;
        
        [Header("Scale")]
        [SerializeField] private float scaleAmount = 1.1f;
        [SerializeField] private float scaleDuration = 0.2f;
        
        private TMP_Text _playerBalanceView;
        
        private float _playerBalance;

        private float PlayerBalanceValue
        {
            get => _playerBalance;
            set
            {
                _playerBalance = value;

                _playerBalanceView.text = _playerBalance + " BYN";
                
                _playerBalanceView.transform.DOKill();

                _playerBalanceView.transform.DOScale(scaleAmount, scaleDuration);
                _playerBalanceView.transform.DOScale(1f, scaleDuration);
            }
        }
        
        private void Awake()
        {
            _playerBalanceView = GetComponent<TMP_Text>();
            
            _eventBus.Subscribe<float>(AddToPlayerBalance);
            
            PlayerBalanceValue = playerStartBalance;
        }

        private void AddToPlayerBalance(float amount)
        {
            PlayerBalanceValue += amount;
        }
    }
}