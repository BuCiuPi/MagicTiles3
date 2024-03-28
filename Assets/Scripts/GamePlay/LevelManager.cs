using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelInfoSO _levelInfoSO;
    [SerializeField] private RectTransform _accuracyLine;

    [SerializeField] private NoteInteractEventChannel _onNoteInteractEventChannel;

    void OnEnable()
    {
        _onNoteInteractEventChannel.OnEventRaised += OnNoteInteractEventChannel;
    }
    
    void OnDisable()
    {
        
        _onNoteInteractEventChannel.OnEventRaised -= OnNoteInteractEventChannel;
    }

    private void OnNoteInteractEventChannel(NoteInteractInfo info)
    {
        Debug.Log(info.NoteController.GetNoteAccuracy(_accuracyLine.position));
    }
}
