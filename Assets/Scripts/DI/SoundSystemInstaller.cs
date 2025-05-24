using Sounds;
using UnityEngine;
using Zenject;

namespace DI
{
    public class SoundSystemInstaller : MonoInstaller
    {
        private SoundService _soundService;
        
        [SerializeField] private SoundDataSetup sfxDataSetup;
        [SerializeField] private SoundDataSetup musicDataSetup;
        
        [SerializeField] private AudioPlayer soundPlayer;
        [SerializeField] private AudioPlayer musicPlayer;
        
        [SerializeField] private int minPoolSize = 1;
        [SerializeField] private int maxPoolSize = 30;

        public override void InstallBindings()
        {
            _soundService = new SoundService(soundPlayer, musicPlayer, sfxDataSetup, musicDataSetup,
                transform, minPoolSize, maxPoolSize);
            
            Container.Bind<SoundService>().FromInstance(_soundService).AsSingle().NonLazy();
        }
        
        private void OnDestroy()
        {
            _soundService.Dispose();
        }
    }
}