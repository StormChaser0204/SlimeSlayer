using UnityEngine;

namespace Game.Minions
{
    internal class View : MonoBehaviour
    {
        public Vector3 CurrentPosition
        {
            get => transform.position;
            set => transform.position = value;
        }
    }
}