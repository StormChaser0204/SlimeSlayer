using UnityEngine;

namespace Common.Dialogs
{
    public interface IDialogsLauncher
    {
        T Show<T>(DialogType dialogType);
        GameObject Show(DialogType dialogType);
    }
}