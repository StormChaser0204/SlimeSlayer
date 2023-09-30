using System;
using System.Linq;
using Game.Minions.Data;
using Game.Shared.Services;
using UnityEngine;

namespace Game.Minions.Services
{
    internal class MovementService : ITickable
    {
        private readonly SpawnedUnits _units;
        private readonly float _endPointPointPosition;

        public Action<MinionModel> OnEndPointReached;

        public MovementService(SpawnedUnits spawnedUnits, Transform endPoint)
        {
            _units = spawnedUnits;
            _endPointPointPosition = endPoint.position.x;
        }

        public void Tick()
        {
            foreach (var unit in _units.Where(u=>!u.EndPointReached))
            {
                unit.View.CurrentPosition += Vector3.left * unit.Speed * Time.deltaTime;

                if (IsReached(unit.View.CurrentPosition))
                    FinishMovement(unit);
            }
        }

        private bool IsReached(Vector3 currentPosition) =>
            currentPosition.x <= _endPointPointPosition;

        private void FinishMovement(MinionModel minion)
        {
            minion.EndPointReached = true;
            //OnEndPointReached.Invoke(minion);
        }
    }
}