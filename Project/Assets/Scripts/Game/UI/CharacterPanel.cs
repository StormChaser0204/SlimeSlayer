using Game.Character.Signals;
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

        [Inject] private SignalBus _signalBus;

        public void Init(int currentLevel, int nextLevelExpAmount)
        {
            _levelProgress.maxValue = nextLevelExpAmount;
            _currentLevelLabel.text = $"{currentLevel}lvl";
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

        public void SetNewLevelProgress(LevelUpSignal levelUpSignal)
        {
            _levelProgress.minValue = levelUpSignal.CurrentLevelExpAmount;
            _levelProgress.maxValue = levelUpSignal.NextLevelExpAmount;
            _currentLevelLabel.text = $"{levelUpSignal.CurrentLevel}lvl";
        }

        public void UpdateProgress(ExperienceChangedSignal experienceChangedSignal) =>
            _levelProgress.value = experienceChangedSignal.Current;
    }
}