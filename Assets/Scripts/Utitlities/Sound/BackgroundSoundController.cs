using Interactables;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace Sounds
{
    public class BackgroundSoundController : MonoBehaviour
    {
        [Inject] private SoundService _soundService;
        [Inject] private EventBus _eventBus;

        [SerializeField] private string sadMusicId = "SadPiano";
        [SerializeField] private string moneyMusicId = "MoneyMusic";
        
        private bool _isMoneyMusicPlaying;
        
        private AudioPlayer _backgroundMusicPlayer;
        
        private void Awake()
        {
            _eventBus.Subscribe<InteractionType>(HandleInteraction);
            
            _backgroundMusicPlayer = _soundService.Play(SoundType.Music, sadMusicId, true);
        }

        private void HandleInteraction(InteractionType interaction)
        {
            if (interaction == InteractionType.Money)
            {
                if (_isMoneyMusicPlaying)
                    return;
                
                if (_backgroundMusicPlayer != null)
                {
                    _backgroundMusicPlayer.StopSound();
                }
                
                _backgroundMusicPlayer = _soundService.Play(SoundType.Music, moneyMusicId);

                _isMoneyMusicPlaying = true;
                
                _backgroundMusicPlayer.OnReleased += PlayStandardBackground;
            }
        }

        private void PlayStandardBackground(AudioPlayer audioPlayer)
        {
            _backgroundMusicPlayer.OnReleased -= PlayStandardBackground;
            
            _isMoneyMusicPlaying = false;
            
            _backgroundMusicPlayer = _soundService.Play(SoundType.Music, sadMusicId, true);
        }
    }
}