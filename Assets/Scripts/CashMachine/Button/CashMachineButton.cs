using UI;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace CashMachine
{
    public class CashMachineButton : MonoBehaviour
    {
        private EventBus _eventBus;
        
        private Tooltip _tooltip;
        
        [SerializeField] private ButtonType buttonType;

        [SerializeField] private string tooltipText;

        [Inject]
        private void Initialize(EventBus eventBus, Tooltip tooltip)
        {
            _eventBus = eventBus;
            
            _tooltip = tooltip;
        }
        
        public void Focus()
        {
            _tooltip?.Show(tooltipText);
        }

        public void Unfocus()
        {
            _tooltip?.Hide();
        }

        public void Interact()
        {
            _eventBus.Publish(buttonType);
        }
    }
}