using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelInfoSO _levelInfoSO;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private NoteSpawnerManager _noteSpawnerManager;

    [Header("EventRaiser")]
    [SerializeField] private LevelInfoEventChannel _onLevelSetupEvent;
    [SerializeField] private VoidEventChannel _onGameStartEvent;

    void OnEnable()
    {
        _onGameStartEvent.OnEventRaised += Initialize;
    }

    void OnDisable()
    {
        _onGameStartEvent.OnEventRaised -= Initialize;
    }

    public void Initialize()
    {
        _noteSpawnerManager.Initialize(_levelManager.GetAccuracyPositionY());
        _levelManager.Initialize(_noteSpawnerManager.GetSpawnPosition(), _levelInfoSO);

        _onLevelSetupEvent.RaiseEvent(_levelInfoSO);
    }

}
