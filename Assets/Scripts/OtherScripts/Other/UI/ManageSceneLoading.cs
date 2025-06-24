using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageSceneLoading : MonoBehaviour
{
    [SerializeField] private CanvasGroup bg;
    [SerializeField] private GameObject introGame;
    public void ActionButtonPlayClick()
    {
        bg.DOFade(0, 0.5f).OnComplete(delegate
        {
            bg.gameObject.SetActive(false);
            introGame.SetActive(true);
        });
    }
    public void ActionExitClick()
    {
        Application.Quit();
    }
    public void ActionPlayWhenFinishIntro()
    {
        LoadingScreen.Instance.ShowLoading(()=> SceneManager.LoadSceneAsync("Main"), null, 1f);
    }
}
