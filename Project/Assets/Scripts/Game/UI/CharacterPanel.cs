using Common.SignalDispatching.Dispatcher;
using Game.Character.Data;
using Game.Character.Signals;
using Game.UI.Health;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    internal class CharacterPanel : MonoBehaviour
    {
        [SerializeField] private Slider _levelProgress;
        [SerializeField] private TMP_Text _currentLevelLabel;
        [SerializeField] private ButtonWithCooldown _attackBtn;
        [SerializeField] private ButtonWithCooldown _blockBtn;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private Transform _characterTransform;

        [Inject] private AbilitiesInfo _abilitiesInfo;
        [Inject] private ISignalDispatcher _dispatcher;

        public void Init(int currentLevel, int nextLevelExpAmount, Camera mainCamera)
        {
            _levelProgress.maxValue = nextLevelExpAmount;
            _currentLevelLabel.text = $"{currentLevel}lvl";
            _healthBar.Init(_characterTransform, mainCamera);
            _healthBar.UpdatePosition();
        }

        private void Start()
        {
            _attackBtn.OnClick += Attack;
            _blockBtn.OnClick += Block;
        }

        private void Attack()
        {
            _dispatcher.Raise(new AttackSignal());
            StartCoroutine(_attackBtn.Cooldown(_abilitiesInfo.AttackCooldown));
        }

        private void Block()
        {
            _dispatcher.Raise(new BlockSignal());
            StartCoroutine(_blockBtn.Cooldown(_abilitiesInfo.BlockCooldown));
        }

        public void SetNewLevelProgress(int currentLevelExpAmount, int nextLevelExpAmount,
            int currentLevel)
        {
            _levelProgress.minValue = currentLevelExpAmount;
            _levelProgress.maxValue = nextLevelExpAmount;
            _currentLevelLabel.text = $"{currentLevel}lvl";
        }

        public void UpdateLevelProgress(int expAmount) =>
            _levelProgress.value = expAmount;

        public void UpdateHealth(int newHealth, int totalHealth) =>
            _healthBar.UpdateValue((float) newHealth / totalHealth);
    }
}