using Player.Camera;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace Interactables
{
    public class CashMachineInteraction : MonoBehaviour, IInteractable
    {
        [Inject] private EventBus _eventBus;
        
        private CameraToggle _cameraToggle = new CameraToggle();
        
        public void Focus()
        {
            
        }

        public void Unfocus()
        {
            
        }

        public void Interact()
        {
            _eventBus.Publish(_cameraToggle);
        }
    }
}