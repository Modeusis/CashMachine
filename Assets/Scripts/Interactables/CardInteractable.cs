using UI;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace Interactables
{
    [RequireComponent(typeof(BoxCollider))]
    public class CardInteractable : MonoBehaviour, IInteractable
    {
        [Inject] private Tooltip _tooltip;
        [Inject] private EventBus _eventBus;
        
        [SerializeField] private string toolTipMessage = "Base string";

        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }
        
        public void Activate()
        {
            _boxCollider.enabled = true;
        }

        public void Deactivate()
        {
            _boxCollider.enabled = false;
        }
        
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
            _eventBus.Publish(InteractionType.GetCard);
        }
    }
}