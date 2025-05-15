using Sounds;
using UnityEngine;
using Zenject;

namespace DI
{
    public class SoundSystemInstaller : MonoInstaller
    {
        private SoundService _soundService;
        
        [SerializeField] private SoundType startBackgroundSound;
        
        [SerializeField] private int _minPoolSize = 1;
        [SerializeField] private int _maxPoolSize = 10;
        
        [SerializeField] private AudioPlayer _audioPlayer;
        
        [SerializeField] private SoundDataSetup _musicSounds;
        [SerializeField] private SoundDataSetup _sfxSounds;
        
        public override void InstallBindings()
        {
            _soundService = new SoundService(_audioPlayer, _sfxSounds, _musicSounds, transform, _minPoolSize, _maxPoolSize);
            
            Container.Bind<SoundService>().FromInstance(_soundService).AsSingle().NonLazy();
        }
    }
}