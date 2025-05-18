using System;
using DG.Tweening;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities.EventBus;

namespace Interactables
{
    [RequireComponent(typeof(BoxCollider))]
    public class ChequeInteractable : MonoBehaviour, IInteractable
    {
        private EventBus _eventBus;
        private Tooltip _tooltip;

        [SerializeField] private TMP_Text operationDateTextField;
        [SerializeField] private TMP_Text operationResultTextField;
        
        [SerializeField] private string tooltipString = "Нажмите лкм, чтобы забрать чек";
        
        [SerializeField] private float targetPositionZ;
        [SerializeField] private float showDuration = 0.5f;
        
        private DateTime _operationDate;
        private string _operationResult;
        
        public void Initialize(EventBus eventBus, Tooltip tooltip, string operation)
        {
            _eventBus = eventBus;
                
            _tooltip = tooltip;
            
            _operationDate = DateTime.Now;
            _operationResult = operation;
            
            operationDateTextField.SetText(_operationDate.ToString("dd/MM/yyyy HH:mm:ss"));
            operationResultTextField.SetText(_operationResult);

            transform.DOLocalMoveZ(targetPositionZ, showDuration);
        }
        
        public void Focus()
        {
            _tooltip?.Show(tooltipString);
        }

        public void Unfocus()
        {
            _tooltip?.Hide();
        }

        public void Interact()
        {
            _eventBus.Publish(new ChequeData(_operationDate, _operationResult));
            
            _tooltip?.Hide();
            
            Destroy(gameObject);
        }
    }
}