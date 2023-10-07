using UnityEngine;

namespace Game.Enemies
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