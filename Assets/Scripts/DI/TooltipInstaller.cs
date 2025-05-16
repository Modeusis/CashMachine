using TMPro;
using UI;
using UnityEngine;
using Zenject;

namespace DI
{
    public class TooltipInstaller : MonoInstaller
    {
        private Tooltip _tooltip;

        [SerializeField] private float fadeDuration;
        
        [SerializeField] private TMP_Text tooltipTextField;

        public override void InstallBindings()
        {
            _tooltip = new Tooltip(tooltipTextField, fadeDuration);
            
            Container.Bind<Tooltip>().FromInstance(_tooltip).AsSingle().NonLazy();
        }
    }
}