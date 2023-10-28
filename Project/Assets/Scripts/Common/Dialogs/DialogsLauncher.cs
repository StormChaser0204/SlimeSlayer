using System;
using System.Linq;
using UnityEngine;

public class DialogsLauncher : MonoBehaviour, IDialogsLauncher
{
    [Serializable]
    private class Dialog
    {
        public DialogType Type;
        public GameObject Prefab;
    }

    [SerializeField] private Dialog[] _dialogs;


    public T Show<T>(DialogType dialogType) => Show(dialogType).GetComponent<T>();

    public GameObject Show(DialogType dialogType) => Instantiate(GetPrefab(dialogType), transform);

    private GameObject GetPrefab(DialogType type) => _dialogs.First(d => d.Type == type).Prefab;
}