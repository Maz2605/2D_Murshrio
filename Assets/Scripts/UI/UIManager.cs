using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _setting;
    [SerializeField] private Button _rePlay;
    [SerializeField] private GameObject objSetting;
    [SerializeField] private TextMeshProUGUI timeItemPlayer;
    [SerializeField] private TextMeshProUGUI timeItemProtect;

    private void Start()
    {
        _setting.onClick.AddListener(()=>Setting());
        _rePlay.onClick.AddListener(()=>Replay());
        this.RegisterListener(EventID.OnChangePlayer,param =>changeTime(timeItemPlayer,(float)param));
        this.RegisterListener(EventID.OnProtect,param =>changeTime(timeItemProtect,(float)param));
    }

    private void changeTime(TextMeshProUGUI Text,float time)
    {
        if (time > 9f)
        {
            Text.text = $"0:{time}";
        }
        else
        {
            Text.text = $"0:0{time}";   
        }
    }

    private void Setting()
    {
        objSetting.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Replay()
    {
        GameManager.Instance.livePlayer = 3;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
