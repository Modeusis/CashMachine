using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Tooltip
    {
        private readonly float _toggleDuration;
        
        private readonly TMP_Text _tooltipField;
        
        private readonly MonoBehaviour _coroutinePlayer;        
        
        private Coroutine _showCoroutine;
        
        public Tooltip(TMP_Text tooltipField, MonoBehaviour coroutinePlayer, float toggleDuration)
        {
            _tooltipField = tooltipField;
            
            _toggleDuration = toggleDuration;
            
            _coroutinePlayer = coroutinePlayer;
            
            Hide();
        }

        public void Show(string tooltip)
        {
            if (_showCoroutine != null)
            {
                _coroutinePlayer.StopCoroutine(_showCoroutine);
                
                _showCoroutine = null;
            }
            
            _tooltipField.text = tooltip;
            
            ToggleAnimation(true);
        }

        public void Hide()
        {
            if (_showCoroutine != null)
            {
                _coroutinePlayer.StopCoroutine(_showCoroutine);
                
                _showCoroutine = null;
            }
            
            ToggleAnimation(false);
        }

        public void ShowAndHide(string tooltip, float duration)
        {
            if (_showCoroutine != null)
            {
                _coroutinePlayer.StopCoroutine(_showCoroutine);
                
                _showCoroutine = null;
            }
            
            _showCoroutine = _coroutinePlayer.StartCoroutine(TempShowCoroutine(tooltip, duration));
        }
        
        private void ToggleAnimation(bool isActive)
        {
            _tooltipField.DOKill();
            
            _tooltipField.DOFade(isActive ? 1 : 0, _toggleDuration);
        }

        private IEnumerator TempShowCoroutine(string text, float duration)
        {
            Show(text);
            
            yield return new WaitForSeconds(duration);
            
            Hide();
        }
    }
}