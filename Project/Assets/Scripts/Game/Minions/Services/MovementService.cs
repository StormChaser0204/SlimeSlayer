using System;
using System.Linq;
using Game.Minions.Data;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Minions.Services
{
    [UsedImplicitly]
    internal class MinionsMovementService : ITickable
    {
        private readonly SpawnedUnits _units;
        private readonly float _endPointPointPosition;

        public Action<Model> OnEndPointReached;

        public MinionsMovementService(SpawnedUnits spawnedUnits, Transform endPoint)
        {
            _units = spawnedUnits;
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