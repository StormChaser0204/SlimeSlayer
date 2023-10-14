using System;
using System.Linq;
using Game.Enemies.Data;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Enemies.Services
{
    [UsedImplicitly]
    internal class EnemiesMovementService : ITickable
    {
        private readonly ActiveEnemies _units;
        private readonly float _endPointPointPosition;

        public Action<Model> OnEndPointReached;

        public EnemiesMovementService(ActiveEnemies activeEnemies, Transform endPoint)
        {
            _units = activeEnemies;
            _endPointPointPosition = endPoint.position.x;
        }

        public void Tick()
        {
            foreach (var unit in _units.Where(u => !u.EndPointReached))
            {
                unit.View.CurrentPosition += Vector3.left * unit.Speed * Time.deltaTime;
                
                
                if (IsReached(unit.View.CurrentPosition))
                    FinishMovement(unit);
            }
        }

        private bool IsReached(Vector3 currentPosition) =>
            currentPosition.x <= _endPointPointPosition;

        private void FinishMovement(Model minion)
        {
            minion.EndPointReached = true;
            //OnEndPointReached.Invoke(minion);
        }
    }
}