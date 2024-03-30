using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UIBackground : MonoBehaviour
{
    [SerializeField] private Material _backgroundBPM;

    [Header("EventListener")]
    [SerializeField] private LevelInfoEventChannel _onLevelSetUpEvent;


    void OnEnable()
    {
        _onLevelSetUpEvent.OnEventRaised += Initialize;
    }

    void OnDisable()
    {

        _onLevelSetUpEvent.OnEventRaised -= Initialize;
    }
    
    public void Initialize(LevelInfoSO _levelInfoSO)
    {
        _backgroundBPM.SetFloat("_BPM", _levelInfoSO.LevelAudioBPM);
        _backgroundBPM.SetFloat("_NotePerBeat", _levelInfoSO.NotePerBeat);
        _backgroundBPM.SetFloat("_BeatOffset", _levelInfoSO.OffsetTime);
    }
}
