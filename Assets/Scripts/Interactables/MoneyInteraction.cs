using UI;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace Interactables
{
    [RequireComponent(typeof(BoxCollider))]
    public class MoneyInteraction : MonoBehaviour, IInteractable
    {
        private EventBus _eventBus;
        private Tooltip _tooltip;
        
        [SerializeField] private string tooltipString = "Нажмите лкм, чтобы забрать деньги";

        private float _moneyValue;
        
        public void Initialize(EventBus eventBus, Tooltip tooltip, float moneyValue)
        {
            _eventBus = eventBus;
                
            _tooltip = tooltip;
            
            _moneyValue = moneyValue;
        }
        
        public void Focus()
        {
            _tooltip.Show(tooltipString);
        }

        public void Unfocus()
        {
            _tooltip.Hide();
        }

        public void Interact()
        {
            _eventBus.Publish(_moneyValue);
            
            _eventBus.Publish(InteractionType.Money);
        }
    }
}