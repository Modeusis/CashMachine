using DG.Tweening;
using TMPro;

namespace UI
{
    public class Tooltip
    {
        private readonly float _toggleDuration;
        
        private readonly TMP_Text _tooltipField;
        
        public Tooltip(TMP_Text tooltipField, float toggleDuration)
        {
            _tooltipField = tooltipField;
            
            _toggleDuration = toggleDuration;
            
            Hide();
        }

        public void Show(string tooltip)
        {
            _tooltipField.text = tooltip;
            
            ToggleAnimation(true);
        }

        public void Hide()
        {
            ToggleAnimation(false);
        }

        private void ToggleAnimation(bool isActive)
        {
            _tooltipField.DOKill();
            
            _tooltipField.DOFade(isActive ? 1 : 0, _toggleDuration);
        }
    }
}