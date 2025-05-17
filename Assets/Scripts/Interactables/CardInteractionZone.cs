using CashMachine;
using UI;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace Interactables
{
    public class CardInteractionZone : MonoBehaviour, IInteractable
    {
        [Inject] private Tooltip _tooltip;
        [Inject] private EventBus _eventBus;
        
        [SerializeField] private float tooltipDuration;
        
        [SerializeField] private string toolTipMessage = "Слот для вставки вашей карты";
        [SerializeField] private string clickToolTipMessage = "Вы не на этапе вставки карты";
        
        [SerializeField] private Card card;
        
        public void Focus()
        {
            _tooltip.Show(toolTipMessage);
        }

        public void Unfocus()
        {
            _tooltip.Hide();
        }

        public void Interact()
        {
            if (!card.IsReady())
            {
                _tooltip.ShowAndHide(clickToolTipMessage, tooltipDuration);
                
                return;
            }
                
            
            _eventBus.Publish(InteractionType.CardSlot);
        }
    }
}