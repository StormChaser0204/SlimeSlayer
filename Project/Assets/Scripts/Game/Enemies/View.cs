using UnityEngine;

namespace Game.Enemies
{
    internal class View : MonoBehaviour
    {
        [field: SerializeField] public Transform HealthBarTransform { get; private set; }

        public Vector3 CurrentPosition
        {
            get => transform.position;
            set => transform.position = value;
        }
    }
}