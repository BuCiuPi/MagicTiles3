using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnerManager : MonoBehaviour
{
    [SerializeField] private LevelInfoSO _levelInfoSO;
    [SerializeField] private NoteController _notePrefab;
    [SerializeField] private NoteDataSO _noteData;

    [Header("Pooling Setting")]
    [SerializeField] private RectTransform _poolTransform;
    [SerializeField] private List<RectTransform> _linePositions;
    [SerializeField] private List<NoteController> _noteControllerPools;
    [SerializeField] private int _poolInitItemCount = 10;

    [Space]
    [SerializeField] private RectTransform _resetNoteRectTransform;

    [Header("Event Listener")]
    [SerializeField] private VoidEventChannel _onNoteSpawnEvent;

    private float _currentAccuracyPositionY;

    void OnEnable()
    {
        _onNoteSpawnEvent.OnEventRaised += SpawnNoteAtRandomLine;
    }

    void OnDisable()
    {
        _onNoteSpawnEvent.OnEventRaised -= SpawnNoteAtRandomLine;
    }

    public void Initialize(float accuracyPositionY)
    {
        _currentAccuracyPositionY = accuracyPositionY;
        SetupPool();
    }

    private void SpawnNoteAtRandomLine()
    {
        int randomIndex = UnityEngine.Random.Range(0, _linePositions.Count - 1);
        RectTransform randomLineTransform = _linePositions[randomIndex];
        CreatedNoteAt(randomLineTransform);
    }

    public void SetupPool()
    {
        for (int i = 0; i < _poolInitItemCount; i++)
        {
            _noteControllerPools.Add(CreateItem());
        }
    }

    public void OnPoolItemResetCallback(NoteController noteController)
    {
        noteController.gameObject.SetActive(false);
        noteController.transform.SetParent(_poolTransform);
        _noteControllerPools.Add(noteController);
    }

    public void CreatedNoteAt(RectTransform linePosition)
    {
        NoteController newNote = GetItemInPool();

        NoteInfo noteInfo = new();
        noteInfo.NoteData = _noteData;
        noteInfo.NoteSpeed = _levelInfoSO.LevelNoteSpeed;
        noteInfo.NoteResetPositionY = _resetNoteRectTransform.position.y;
        noteInfo.NoteAccuracyPositionY = _currentAccuracyPositionY;

        newNote.Initialize(noteInfo, OnPoolItemResetCallback);
        newNote.transform.SetParent(linePosition);
        newNote.SetNotePosition(linePosition.position);
        newNote.gameObject.SetActive(true);
    }

    private NoteController GetItemInPool()
    {
        NoteController returnedNote = null;

        if (_noteControllerPools.Count > 0)
        {
            returnedNote = _noteControllerPools[0];
            _noteControllerPools.RemoveAt(0);
        }

        if (returnedNote == null)
        {
            returnedNote = CreateItem();
        }

        return returnedNote;
    }

    private NoteController CreateItem()
    {
        NoteController newNote = Instantiate(_notePrefab, _poolTransform);
        newNote.gameObject.SetActive(false);
        return newNote;
    }

    public Vector3 GetSpawnPosition()
    {
        return _linePositions[0].position;
    }
}
