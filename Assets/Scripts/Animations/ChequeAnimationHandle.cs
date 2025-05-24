using Interactables;
using Sounds;
using UI;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace Animations
{
    public class ChequeAnimationHandle : MonoBehaviour
    { 
        [Inject] private EventBus _eventBus;
        [Inject] private Tooltip _tooltip;
        [Inject] private SoundService _soundService;
        
        [SerializeField] private string printChequeSoundId = "ChequePrint";
        
        [SerializeField] private ChequeInteractable chequePrefab;
        
        [SerializeField] private MoneyAnimationHandler moneyAnimationHandler;

        private GameObject _chequeInstance;

        private void Awake()
        {
            _eventBus.Subscribe<InteractionType>(GenerateCheque);
        }
        
        public bool IsReady() => _chequeInstance == null;

        private void GenerateCheque(InteractionType interactionType)
        {
            var operation = string.Empty;
            
            if (interactionType == InteractionType.SpawnMoney)
            {
                operation = "Снятие средств: " + moneyAnimationHandler.GetMoneyValue() + " BYN";
            }
            else if (interactionType == InteractionType.CheckBalance)
            {
                operation = "Проверка баланса";
            }
            else
            {
                return;
            }
            
            var chequeInteractable = Instantiate(chequePrefab, transform);
            
            _chequeInstance = chequeInteractable.gameObject;
            
            _soundService.Play(SoundType.Sound, printChequeSoundId, _chequeInstance.transform, 2f);
            
            chequeInteractable.Initialize(_eventBus, _tooltip, operation);
        }
    }
}