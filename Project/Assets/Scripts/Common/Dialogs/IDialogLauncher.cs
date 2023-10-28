using UnityEngine;

public interface IDialogsLauncher
{
    T Show<T>(DialogType dialogType);
    GameObject Show(DialogType dialogType);
}