using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIResult : MonoBehaviour
{
    [SerializeField] private GameObject GUI;
    [SerializeField] private bool _isHiddenOnAwake;
    [SerializeField] private Button _btnRestart;

    [Header("Event Listener")]
    [SerializeField] private VoidEventChannel _onPanelInspect;

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
        _onPanelInspect.OnEventRaised += Show;
        _btnRestart.onClick.AddListener(OnRestartButtonClick);
    }

    void OnDisable()
    {

        _onPanelInspect.OnEventRaised -= Show;
        _btnRestart.onClick.RemoveAllListeners();
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

    private void Show()
    {
        this.GUI.SetActive(true);
    }

    private void Hide()
    {
        this.GUI.SetActive(false);
    }
}
