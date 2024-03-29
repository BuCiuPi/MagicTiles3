using System;
using UnityEngine;

[CreateAssetMenu(menuName = "MagicTiles3/VoidEventChannel")]
public class VoidEventChannel : ScriptableObject
{
    public Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}