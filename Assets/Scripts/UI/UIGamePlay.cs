using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] private Image _barScore;
    [SerializeField] private TextMeshProUGUI _txtScore;
    [SerializeField] private Animator _animScore;
    [SerializeField] private TextMeshProUGUI _txtCombo;
    [SerializeField] private Animator _animCombo;

    [Space]
    [SerializeField] private GameObject _txtBad;
    [SerializeField] private ParticleSystem _psBad;
    [SerializeField] private Animator _animBad;

    [Space]
    [SerializeField] private GameObject _txtGood;
    [SerializeField] private ParticleSystem _psGood;
    [SerializeField] private Animator _animGood;

    [Space]
    [SerializeField] private GameObject _txtPerfect;
    [SerializeField] private ParticleSystem _psPerfect;
    [SerializeField] private Animator _animPerfect;

    [Header("Event Listener")]
    [SerializeField] private NoteAccuracyEventChannel _onNoteAccuracyEvent;
    [SerializeField] private FloatEventChannel _onSetScorePercentEvent;
    [SerializeField] private IntEventChannel _onSetScoreEvent;
    [SerializeField] private IntEventChannel _onSetComboEvent;

    void OnEnable()
    {
        _onNoteAccuracyEvent.OnEventRaised += OnNoteAccuracyEventReceived;
        _onSetScorePercentEvent.OnEventRaised += OnSetScorePercentReceived;
        _onSetComboEvent.OnEventRaised += OnSetComboEventReceived;
        _onSetScoreEvent.OnEventRaised += OnSetScoreEventReceived;
    }

    void OnDisable()
    {
        _onNoteAccuracyEvent.OnEventRaised -= OnNoteAccuracyEventReceived;
        _onSetScorePercentEvent.OnEventRaised -= OnSetScorePercentReceived;
        _onSetComboEvent.OnEventRaised -= OnSetComboEventReceived;
        _onSetScoreEvent.OnEventRaised -= OnSetScoreEventReceived;
    }

    private void OnSetScoreEventReceived(int Value)
    {
        _txtScore.gameObject.SetActive(true);
        _txtScore.text = Value.ToString();
        _animScore.SetTrigger("Pop");
    }

    private void OnSetComboEventReceived(int value)
    {
        if (value <= 0)
        {
            _txtCombo.gameObject.SetActive(false);
            return;
        }

        _txtCombo.text = value + "x";
        _txtCombo.gameObject.SetActive(true);
        _animCombo.SetTrigger("Pop");
    }

    private void OnSetScorePercentReceived(float value)
    {
        _barScore.fillAmount = value;
    }

    private Coroutine _badCoroutine;
    private Coroutine _goodCoroutine;
    private Coroutine _perfectCoroutine;
    private void OnNoteAccuracyEventReceived(NoteAccuracy accuracy)
    {
        ResetAllAccuracy();
        switch (accuracy)
        {
            case NoteAccuracy.Miss:
                break;
            case NoteAccuracy.Bad:
                _badCoroutine = StartCoroutine(CoPlayAccuracyAnimation(_txtBad, _psBad,_animBad, 1));

                break;
            case NoteAccuracy.Good:
                _goodCoroutine = StartCoroutine(CoPlayAccuracyAnimation(_txtGood, _psGood,_animGood, 1));

                break;
            case NoteAccuracy.Perfect:
                _perfectCoroutine = StartCoroutine(CoPlayAccuracyAnimation(_txtPerfect, _psPerfect,_animPerfect, 1));

                break;
        }
    }

    IEnumerator CoPlayAccuracyAnimation(GameObject textObject, ParticleSystem particle, Animator animator, float DelayOffTime)
    {
        textObject.SetActive(true);
        particle.Play();
        animator.SetTrigger("Pop");
        yield return new WaitForSeconds(DelayOffTime);
        particle.Stop();
        textObject.SetActive(false);
    }

    public void ResetAllAccuracy()
    {
        _psBad.Stop();
        _txtBad.SetActive(false);
        _psGood.Stop();
        _txtGood.SetActive(false);
        _psPerfect.Stop();
        _txtPerfect.SetActive(false);

        if (_badCoroutine != null)
        {
            StopCoroutine(_badCoroutine);
        }
        if (_goodCoroutine != null)
        {
            StopCoroutine(_goodCoroutine);
        }
        if (_perfectCoroutine != null)
        {
            StopCoroutine(_perfectCoroutine);
        }
    }
}
