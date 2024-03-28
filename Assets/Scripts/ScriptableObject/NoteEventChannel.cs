using System;
using UnityEngine;

[CreateAssetMenu(menuName = "MagicTiles3/NoteEventChannel")]
public class NoteInteractEventChannel : ScriptableObject
{
    public Action<NoteInteractInfo> OnEventRaised;

    public void RaiseEvent(NoteInteractInfo noteInteractInfo)
    {
        OnEventRaised?.Invoke(noteInteractInfo);
    }
}

public class NoteInteractInfo
{
    public NoteController NoteController;
    public float NoteHoverTime;
}