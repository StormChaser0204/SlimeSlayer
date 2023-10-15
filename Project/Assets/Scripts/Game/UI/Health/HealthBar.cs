using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Health
{
    internal class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _progres;
        [SerializeField] private RectTransform _transform;
        [SerializeField] private Vector3 _offset;

        private Transform _chasedTarget;

        public void SetChasedTarget(Transform target) => _chasedTarget = target;

        public void UpdatePosition()
        {
            var viewportPoint = Camera.main.WorldToScreenPoint(_chasedTarget.position);
            _transform.anchoredPosition = viewportPoint + _offset;
        }

        public void UpdateValue(float newValue) => _progres.value = newValue;
    }
}