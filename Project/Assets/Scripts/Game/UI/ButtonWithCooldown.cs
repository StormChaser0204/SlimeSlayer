using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    internal class ButtonWithCooldown : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _cooldownImage;

        public Action OnClick;

        public void Click() => OnClick.Invoke();

        public IEnumerator Cooldown(float duration)
        {
            var currentTime = duration;

            SetButtonInteractableState(false);
            var wff = new WaitForEndOfFrame();

            while (currentTime >= 0)
            {
                currentTime -= Time.deltaTime;
                _cooldownImage.fillAmount = currentTime / duration;
                yield return wff;
            }

            SetButtonInteractableState(true);
        }

        private void SetButtonInteractableState(bool isOn) => _button.interactable = isOn;
    }
}