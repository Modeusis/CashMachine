﻿using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.Events;
using Utilities.EventBus;
using Zenject;

namespace Interactables
{
    public class MoneyLockInteraction : MonoBehaviour, IInteractable
    {
        [Inject] private EventBus _eventBus;
        [Inject] private Tooltip _tooltip;
        
        [SerializeField] private float clickTooltipDuration = 1f;
        
        [Header("Tooltips messages")]
        [SerializeField] private string tooltipString = "Затворка из которой выдаются деньги";
        [SerializeField] private string clickTooltipString = "Выполните шаги для получения средств";
        
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
            _tooltip.ShowAndHide(clickTooltipString, clickTooltipDuration);
        }
    }
}