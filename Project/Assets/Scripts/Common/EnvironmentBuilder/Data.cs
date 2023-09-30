using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.EnvironmentBuilder
{
    [CreateAssetMenu(fileName = "EnvironmentData", menuName = "Common/Environment/Data", order = 1)]
    public class Data : ScriptableObject
    {
        [Serializable]
        public class EnvironmentPositions
        {
            public List<Vector2> Positions;

            private readonly int _hashCode;

            public EnvironmentPositions(int hashCode)
            {
                _hashCode = hashCode;
                Positions = new List<Vector2>();
            }

            public bool IsEqual(int hashCode) => _hashCode == hashCode;
        }

        [SerializeField] private List<EnvironmentPositions> _positions = new();

        public EnvironmentPositions GetByIdOrCreateNew(int hashCode)
        {
            if (_positions.Any(p => p.IsEqual(hashCode)))
                return GetById(hashCode);

            var positions = new EnvironmentPositions(hashCode);
            _positions.Add(positions);
            return positions;
        }

        private EnvironmentPositions GetById(int hashCode) =>
            _positions.First(p => p.IsEqual(hashCode));
    }
}