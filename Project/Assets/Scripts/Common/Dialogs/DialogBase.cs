using UnityEngine;

namespace Common.Dialogs
{
    public class DialogBase : MonoBehaviour
    {
        public void Close() => Destroy(gameObject);
    }
}