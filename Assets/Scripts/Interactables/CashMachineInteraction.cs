using Player.Camera;
using UI;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace Interactables
{
    public class CashMachineInteraction : MonoBehaviour, IInteractable
    {
        [Inject] private EventBus _eventBus;
        [Inject] private Tooltip _tooltip;

        [SerializeField] private string tooltipString;
        
        private CameraToggle _cameraToggle = new CameraToggle();
        
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
            _eventBus.Publish(_cameraToggle);
        }
    }
}