using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UITapToPlay : MonoBehaviour
{
    [SerializeField] private GameObject GUI;
    [SerializeField] private bool _isHiddenOnAwake;
    [SerializeField] private Button _btnTapToPlay;

    [Header("Event Listener")]
    [SerializeField] private VoidEventChannel _onStartGameEvent;

    void Awake()
    {
        if (_isHiddenOnAwake)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    void OnEnable()
    {
        _btnTapToPlay.onClick.AddListener(OnButtonTapToPlayClick);
    }

    void OnDisable()
    {
        _btnTapToPlay.onClick.RemoveAllListeners();
    }
    private void OnButtonTapToPlayClick()
    {
        _onStartGameEvent.RaiseEvent();
        Hide();
    }

    public void Show()
    {
        GUI.SetActive(true);
    }

    public void Hide()
    {
        GUI.SetActive(false);
    }
}
