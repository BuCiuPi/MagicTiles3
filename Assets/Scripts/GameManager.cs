using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private NoteSpawnerManager _noteSpawnerManager;

    void Start()
    {
        Initialize();
    }
    
    public void Initialize()
    {
        _noteSpawnerManager.Initialize(_levelManager.GetAccuracyPositionY());
        _levelManager.Initialize(_noteSpawnerManager.GetSpawnPosition());
    }

}