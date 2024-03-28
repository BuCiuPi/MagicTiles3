using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _noteTop;
    [SerializeField] private RectTransform _noteBot;

    [Header("Event Raiser")]
    [SerializeField] private NoteInteractEventChannel _OnNoteInteractEvent;


    private RectTransform _rectTransform;

    private NoteInfo _currentNoteInfo;

    private float _lastClickTime;
    private bool _isClicked;

    private Action<NoteController> _resetCallback;

    void Awake()
    {
        _rectTransform = transform.GetComponent<RectTransform>();
    }

    public void Initialize(NoteInfo noteInfo, Action<NoteController> resetCallback)
    {
        this._currentNoteInfo = noteInfo;
        this._resetCallback = resetCallback;
    }

    void FixedUpdate()
    {
        DecreaseNoteHeight();
    }

    private void DecreaseNoteHeight()
    {
        if (_currentNoteInfo == null)
        {
            return;
        }

        _rectTransform.position += Vector3.up * (_currentNoteInfo.NoteSpeed * Time.deltaTime);

        if (_rectTransform.position.y < _currentNoteInfo.NoteResetPositionZ)
        {
            OnNoteTouchResetPosition();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isClicked)
        {
            return;
        }

        _isClicked = true;
        _lastClickTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isClicked)
        {
            return;
        }

        NoteInteractInfo noteInteractInfo = new();
        noteInteractInfo.NoteController = this;
        noteInteractInfo.NoteHoverTime = Time.time - _lastClickTime;

        _OnNoteInteractEvent.RaiseEvent(noteInteractInfo);
    }

    public NoteAccuracy GetNoteAccuracy(Vector3 position)
    {

        float currentDistance = MathF.Abs(position.y - _rectTransform.position.y);

        if (_rectTransform.position.y < position.y)
        {
            float maxDistanceTop = Mathf.Abs(_noteTop.position.y - _rectTransform.position.y);
            return GetNoteAccuracyByPercent(currentDistance / maxDistanceTop);
        }
        else
        {
            float maxDistanceBot = Mathf.Abs(_noteTop.position.y - _rectTransform.position.y);
            return GetNoteAccuracyByPercent(currentDistance / maxDistanceBot);
        }
    }

    private NoteAccuracy GetNoteAccuracyByPercent(float percent)
    {
        switch (percent)
        {
            case > 1:
                return NoteAccuracy.Miss;
            case > .7f:
                return NoteAccuracy.Bad;
            case > .3f:
                return NoteAccuracy.Good;
            default:
                return NoteAccuracy.Perfect;
        }
    }

    public void SetNotePosition(Vector3 position)
    {
        _rectTransform.position = position;
    }

    private void OnNoteTouchResetPosition()
    {
        Reset();
        _resetCallback?.Invoke(this);
    }

    private void Reset()
    {
        _isClicked = false;
    }

}

public class NoteInfo
{
    public NoteDataSO NoteData;
    public float NoteResetPositionZ;
    public float NoteSpeed;
}

public enum NoteAccuracy
{
    Miss,
    Bad,
    Good,
    Perfect
}




