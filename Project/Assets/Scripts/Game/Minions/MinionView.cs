using UnityEngine;

namespace Game.Minions
{
    internal class MinionView : MonoBehaviour
    {
        public Vector3 CurrentPosition
        {
            get => transform.position;
            set => transform.position = value;
        }
    }
}