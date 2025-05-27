using DG.Tweening;
using Sounds;
using UnityEngine;
using Zenject;

namespace Animations
{
    public class VerticalButtonAnimationHandler : MonoBehaviour
    {
        [Inject] private SoundService _soundService;
        
        [SerializeField] private string verticalButtonClickId = "ButtonPress";
        
        [SerializeField] private float clickDuration = 0.5f;
        [SerializeField] private float zOffset = 0.002f;
        
        private float _startZPosition;
        
        private Sequence _sequence;

        private void Awake()
        {
            _startZPosition = transform.localPosition.z;
        }
        
        public void Click()
        {
            _sequence?.Kill();
            
            _sequence = DOTween.Sequence();

            _soundService.Play(SoundType.Sound, verticalButtonClickId, transform, 4f);
            
            _sequence.Append(transform.DOLocalMoveZ(_startZPosition + zOffset, clickDuration /2 ));
            _sequence.Append(transform.DOLocalMoveZ(_startZPosition, clickDuration / 2));
        }
    }
}