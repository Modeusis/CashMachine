using DG.Tweening;
using Sounds;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class UiButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] private float scaleFactorOnHover = 1.1f;
        [SerializeField] private float scaleFactorOnClick = 0.9f;
        [SerializeField] private float duration = 0.4f;
        
        private Button _button;
        
        private Sequence _sequence;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_button.interactable)
                return;
            
            transform.DOKill();
            _sequence?.Kill();
            
            transform.DOScale(scaleFactorOnHover, duration)
                .SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_button.interactable)
                return;
            
            transform.DOKill();
            _sequence?.Kill();
            
            transform.DOScale(1f, duration / 2)
                .SetUpdate(true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_button.interactable)
                return;
            
            transform.DOKill();
            _sequence?.Kill();
            
            var sequence = DOTween.Sequence();

            sequence.Append(transform.DOScale(scaleFactorOnClick, 0.3f));
            sequence.Append(transform.DOScale(1f, 0.2f));
            
            sequence.SetUpdate(true);
        }
    }
}