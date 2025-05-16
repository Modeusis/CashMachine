using System;
using UnityEngine;
using UnityEngine.UI;

namespace CashMachine.Screens
{
    [Serializable]
    public class ScreenSetup
    {
        [field: SerializeField] public ScreenType ScreenType { get; private set; }
        
        [field: SerializeField] public Image Screen { get; private set; }

        public void Activate()
        {
            Screen.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            Screen.gameObject.SetActive(false);
        }
    }
}