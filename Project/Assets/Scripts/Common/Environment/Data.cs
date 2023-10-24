using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Environment
{
    [CreateAssetMenu(fileName = "EnvironmentData", menuName = "Common/Environment/Data", order = 1)]
    public class Data : ScriptableObject
    {
        [Serializable]
        public class Info
        {
            public Texture2D Texture;
            public List<Vector2> Positions;

            public Info(Texture2D tex)
            {
                Texture = tex;
                Positions = new List<Vector2>();
            }

            public bool IsEqual(Texture2D tex) => Texture == tex;
        }

        [FormerlySerializedAs("_infos")] [SerializeField] private List<Info> _info = new();
        [SerializeField] private Sprite[] _trunks;
        [SerializeField] private Sprite[] _crowns;

        public Sprite GetRandomTrunkSprite() => _trunks[new System.Random().Next(0, _trunks.Length)];

        public Sprite GetRandomCrownSprite() => _crowns[new System.Random().Next(0, _crowns.Length)];

        public Info GetInfoByTexture(Texture2D tex)
        {
            if (_info.Any(p => p.IsEqual(tex)))
                return GetByTexture(tex);

            var info = new Info(tex);
            _info.Add(info);
            return info;
        }

        private Info GetByTexture(Texture2D tex) =>
            _info.First(p => p.IsEqual(tex));
    }
}