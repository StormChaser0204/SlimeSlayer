using UnityEngine;
using UnityEngine.UI;

// ReSharper disable PossibleLossOfFraction

namespace Game.UI.Health
{
    internal class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _progress;
        [SerializeField] private RectTransform _transform;

        private Transform _chasedTarget;
        private Camera _camera;

        public void Init(Transform target, Camera mainCamera)
        {
            _chasedTarget = target;
            _camera = mainCamera;
        }

        public void UpdatePosition()
        {
            var resolution = Screen.currentResolution;
            var offset = new Vector3(resolution.width / 2, resolution.height / 2, 0);
            _transform.anchoredPosition = _camera.WorldToScreenPoint(_chasedTarget.position) - offset;
        }

        public void UpdateValue(float newValue) => _progress.value = newValue;
    }
}