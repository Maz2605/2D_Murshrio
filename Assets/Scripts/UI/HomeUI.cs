using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    public Button playNewGame;
    public Button[] buttons;
    public GameObject currentUI;
    public GameObject uiIntro;
    private void Awake()
    {
       MenuChoice();
    }

    private void Start()
    {
        if(playNewGame != null)
        {
            playNewGame.onClick.AddListener((() => PlayNewGame()));
        }
    }

    private void MenuChoice()
    {
        int unclockedLevel = PlayerPrefs.GetInt("UnlockLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < unclockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelID)
    {
        string levelName = "Level" + levelID;
        if(levelID == 1 && PlayerPrefs.GetInt("FirstPlay", 1) == 1)
        {
            ShowIntro();
        }
        else
        {
            SceneManager.LoadScene(levelName);
        }
    }

    public void PlayNewGame()
    {
        PlayerPrefs.SetInt("UnlockLevel",1);
        PlayerPrefs.SetInt("FirstPlay", 1);
        ShowIntro();
        PlayerPrefs.Save();
        MenuChoice();
    }
    private void ShowIntro()
    {
        currentUI.SetActive(false);
        uiIntro.SetActive(true);
        PlayerPrefs.SetInt("FirstPlay", 0);
    }
    public void BtnTapTheScreen()
    {
        LoadingScreen.Instance.ShowLoading(() => SceneManager.LoadScene("Level" + 1), null, 1f);
    }
}
