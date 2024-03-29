using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private LevelInfoSO _levelInfoSO;
    [SerializeField] private RectTransform _accuracyLine;

    [Header("Event Listener")]
    [SerializeField] private NoteInteractEventChannel _onNoteInteractEventChannel;

    [Header("Event Raiser")]
    [SerializeField] private VoidEventChannel _onNoteSpawnEvent;
    [SerializeField] private NoteAccuracyEventChannel _onNoteAccuracyEvent;
    [SerializeField] private FloatEventChannel _onNoteScoreEvent;
    [SerializeField] private IntEventChannel _onNoteComboEvent;

    private float _spawnOffset;
    private float _songDuration;
    private float _currentSongDuration;
    private float _startSongTime;
    private float _spawnTimeOffsetPerBeat;

    private bool _isStartSpawnNote;
    private float _validSpawnTime;

    private float _currentLevelScore;
    private int _currentCombo;
    private float _currentPlayOffset;

    void OnEnable()
    {
        _onNoteInteractEventChannel.OnEventRaised += OnNoteInteractEventChannel;
    }

    void OnDisable()
    {

        _onNoteInteractEventChannel.OnEventRaised -= OnNoteInteractEventChannel;
    }

    public void Initialize(Vector3 SpawnPosition)
    {
        _songDuration = _levelInfoSO.LevelAudioClip.length;

        float distanceY = Mathf.Abs(SpawnPosition.y - _accuracyLine.position.y);
        _spawnOffset = distanceY / Mathf.Abs(_levelInfoSO.LevelNoteSpeed);
        _spawnTimeOffsetPerBeat = 60 / _levelInfoSO.LevelAudioBPM;

        // fix audio awake make game lagging
        _audioSource.clip = _levelInfoSO.LevelAudioClip;
        _audioSource.Play();
        _audioSource.Stop();

        OnStarLevel();
    }

    private void OnStarLevel()
    {
        _validSpawnTime = _levelInfoSO.OffsetTime + (_levelInfoSO.OffsetBeat * _spawnTimeOffsetPerBeat);

        if (_validSpawnTime - _spawnOffset < 0)
        {
            _currentPlayOffset = Mathf.Abs(_validSpawnTime);
        }
        StartCoroutine(CoOnStartLevel(_currentPlayOffset));
    }

    IEnumerator CoOnStartLevel(float DelayMusicTime)
    {
        _startSongTime = Time.time;
        _isStartSpawnNote = true;
        yield return new WaitForSeconds(DelayMusicTime);

        _audioSource.clip = _levelInfoSO.LevelAudioClip;
        _audioSource.Play();
    }

    void FixedUpdate()
    {
        UpdateSpawnTime();
    }

    private void UpdateSpawnTime()
    {
        if (_isStartSpawnNote)
        {
            _currentSongDuration = Time.time - _startSongTime;

            if (_currentSongDuration > _validSpawnTime - _spawnOffset + _currentPlayOffset)
            {
                _onNoteSpawnEvent.RaiseEvent();
                _validSpawnTime += (_spawnTimeOffsetPerBeat / _levelInfoSO.NotePerBeat);
            }
        }
    }

    private void OnNoteInteractEventChannel(NoteInteractInfo info)
    {
        NoteAccuracy currentNoteAccuracy = info.NoteController.GetNoteAccuracy(_accuracyLine.position);
        if (currentNoteAccuracy != NoteAccuracy.Miss)
        {
            float additionalScore = _levelInfoSO.GetScoreByAccuracy(currentNoteAccuracy);
            _currentLevelScore += additionalScore;

            float scorePercent = Mathf.Clamp01(_currentLevelScore / _levelInfoSO.MaxScore);
            _onNoteScoreEvent.RaiseEvent(scorePercent);

            _currentCombo++;
        }
        else
        {
            _currentCombo = 0;
        }

        _onNoteComboEvent.RaiseEvent(_currentCombo);
        _onNoteAccuracyEvent.RaiseEvent(currentNoteAccuracy);
    }

    public float GetAccuracyPositionY()
    {
        return _accuracyLine.position.y;
    }
}
