using System;
using DG.Tweening;
using Interactables;
using Sounds;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities.EventBus;
using Zenject;
using Object = UnityEngine.Object;

namespace Animations
{
    public class MoneyAnimationHandler : MonoBehaviour
    {
        [Inject] private SoundService _soundService;
        [Inject] private EventBus _eventBus;
        [Inject] private Tooltip _tooltip;
        
        [Header("Prefab")] 
        [SerializeField] private GameObject moneyPrefab;

        [Header("Transforms")] 
        [SerializeField] public Transform moneyLock;
        
        [Header("Settings")]
        [SerializeField] private float lockToggleDuration = 0.8f;
        [SerializeField] private float moneyShowDuration = 0.6f;

        [Header("Money lock parameters")]
        [SerializeField] private Vector3 moneyLockStartPosition = new Vector3(-0.0699999854f, 1.43004167f, -0.246480346f);
        [SerializeField] private Vector3 moneyLockStartRotation = Vector3.zero;
        [SerializeField] private Vector3 moneyLockTargetPosition = new Vector3(-0.07f, 1.47358513f, -0.229360431f);
        [SerializeField] private Vector3 moneyLockTargetRotation = new Vector3(31.4094048f, 0f, 0f);
        
        [Header("Money lock parameters")]
        [SerializeField] private Vector3 moneyTargetPosition = Vector3.zero;
        
        private bool _isSpawned;
        
        private GameObject _moneyInstance;

        private float _moneyValue;

        public bool IsReady() => _moneyInstance == null;
        
        public void SetMoney(float moneyValue)
        {
            if (moneyValue <= 0)
                return;
            
            _moneyValue = moneyValue;
            
            _eventBus.Publish(InteractionType.SpawnMoney);
        }

        public float GetMoneyValue() => _moneyValue; 
        
        public void HandleTakeMoney(InteractionType interactionType)
        {
            if (interactionType != InteractionType.Money)
                return;
            
            moneyLock?.DOKill();
            _moneyInstance?.transform.DOKill();
            
            Destroy(_moneyInstance);

            moneyLock.DOLocalMove(moneyLockStartPosition, lockToggleDuration);
            moneyLock.DOLocalRotate(moneyLockStartRotation, lockToggleDuration);
            
            _isSpawned = false;
        }
        
        public void HandleMoneyCall()
        {
            if (_isSpawned)
                return;
            
            moneyLock?.DOKill();
            
            SpawnMoney();
            
            if (_moneyInstance == null)
                return;
            
            _eventBus.Subscribe<InteractionType>(HandleTakeMoney);
            
            moneyLock.DOLocalMove(moneyLockTargetPosition, lockToggleDuration);
            moneyLock.DOLocalRotate(moneyLockTargetRotation, lockToggleDuration);
            
            _moneyInstance.transform.DOLocalMove(moneyTargetPosition, moneyShowDuration)
                .SetDelay(lockToggleDuration);
        }

        private void SpawnMoney()
        {
            if (_moneyValue <= 0)
                return;
            
            _eventBus.Unsubscribe<InteractionType>(HandleTakeMoney);
            
            _moneyInstance = Instantiate(moneyPrefab, transform);
            
            _moneyInstance.layer = LayerMask.NameToLayer("Toggled");
            
            _moneyInstance.transform.localPosition = Vector3.zero;
            _moneyInstance.transform.localRotation = Quaternion.identity;

            _moneyInstance.AddComponent<BoxCollider>();
            
            var moneyInteraction = _moneyInstance.AddComponent<MoneyInteraction>();
            moneyInteraction.Initialize(_eventBus, _tooltip, _moneyValue);

            _moneyValue = 0f;
        }
    }
}