using Game.Character.Signals;
using Game.Enemies.Signals;
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
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _blockCooldown;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private Transform _characterTransform;

        [Inject] private SignalBus _signalBus;

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
            _signalBus.Fire(new AttackSignal());
            StartCoroutine(_attackBtn.Cooldown(_attackCooldown));
        }

        private void Block()
        {
            _signalBus.Fire(new BlockSignal());
            StartCoroutine(_blockBtn.Cooldown(_blockCooldown));
        }

        public void SetNewLevelProgress(LevelUpSignal signal)
        {
            _levelProgress.minValue = signal.CurrentLevelExpAmount;
            _levelProgress.maxValue = signal.NextLevelExpAmount;
            _currentLevelLabel.text = $"{signal.CurrentLevel}lvl";
        }

        public void UpdateProgress(ExperienceChangedSignal signal) =>
            _levelProgress.value = signal.Current;

        public void UpdateHealth(CharacterHealthChangedSignal signal) =>
            _healthBar.UpdateValue((float) signal.NewHealth / signal.TotalHealth);
    }
}