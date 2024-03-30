using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelInfoSO _levelInfoSO;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private NoteSpawnerManager _noteSpawnerManager;

    [Header("EventRaiser")]
    [SerializeField] private LevelInfoEventChannel _onLevelSetupEvent;

    void Start()
    {
        Initialize();
    }
    
    public void Initialize()
    {
        _noteSpawnerManager.Initialize(_levelManager.GetAccuracyPositionY());
        _levelManager.Initialize(_noteSpawnerManager.GetSpawnPosition(), _levelInfoSO);

        _onLevelSetupEvent.RaiseEvent(_levelInfoSO);
    }

}
