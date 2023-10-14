using Game.Character.Signals;
using UnityEngine;
using Zenject;

internal class ActionPanel : MonoBehaviour
{
    [SerializeField] private ButtonWithCooldown _attackBtn;
    [SerializeField] private ButtonWithCooldown _blockBtn;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _blockCooldown;

    [Inject] private SignalBus _signalBus;

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
}