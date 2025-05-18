using DG.Tweening;
using Sounds;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Animations
{
    public class NumButtonsAnimationHandler : MonoBehaviour
    {
        [Inject] private SoundService _soundService;
        
        [SerializeField] private float clickDuration = 0.3f;
        [SerializeField] private float yOffset = 0.000325f;

        private float _startYPosition;
        
        private Sequence _sequence;

        private void Awake()
        {
            _startYPosition = transform.localPosition.y;
        }
        
        public void Click()
        {
            _sequence?.Kill();
            
            _sequence = DOTween.Sequence();
            
            _soundService.Play3DSfx(SoundType.NumInput, transform, 2f, 1f);
            
            _sequence.Append(transform.DOLocalMoveY(_startYPosition - yOffset, clickDuration /2 ));
            _sequence.Append(transform.DOLocalMoveY(_startYPosition, clickDuration / 2));
        }
    }
}