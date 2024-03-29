using System;
using UnityEngine;

[CreateAssetMenu(menuName = "MagicTiles3/FloatEventChannel")]
public class FloatEventChannel : ScriptableObject
{
    public Action<float> OnEventRaised;

    public void RaiseEvent(float value)
    {
        OnEventRaised?.Invoke(value);
    }
}