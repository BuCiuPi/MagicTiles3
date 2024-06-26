using System;
using UnityEngine;

[CreateAssetMenu(menuName = "MagicTiles3/IntEventChannel")]
public class IntEventChannel : ScriptableObject
{
    public Action<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}