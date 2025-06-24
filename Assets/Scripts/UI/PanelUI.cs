using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelUI : MonoBehaviour
{
    [SerializeField] private Button _play;
    [SerializeField] private Button _choiceLevel;
    [SerializeField] private Button _close;

    private void Start()
    {
        _play.onClick.AddListener(()=>Play());
        _choiceLevel.onClick.AddListener(()=>Level());
        _close.onClick.AddListener(()=>Close());
    }

    private void Play()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        GameManager.Instance.livePlayer = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Level()
    {
        
    }

    private void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
