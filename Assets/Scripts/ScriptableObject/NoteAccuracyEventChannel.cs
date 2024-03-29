using System;
using UnityEngine;

[CreateAssetMenu(menuName = "MagicTiles3/NoteAccuracyEventChannel")]
public class NoteAccuracyEventChannel : ScriptableObject
{
    public Action<NoteAccuracy> OnEventRaised;

    public void RaiseEvent(NoteAccuracy noteAccuracy)
    {
        OnEventRaised?.Invoke(noteAccuracy);
    }
}