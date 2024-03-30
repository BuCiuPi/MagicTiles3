using System;
using UnityEngine;

[CreateAssetMenu(menuName = "MagicTiles3/LevelInfoEventChannel")]
public class LevelInfoEventChannel : ScriptableObject
{
    public Action<LevelInfoSO> OnEventRaised;

    public void RaiseEvent(LevelInfoSO value)
    {
        OnEventRaised?.Invoke(value);
    }
}