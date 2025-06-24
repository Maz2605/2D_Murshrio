using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    public Button playNewGame;
    public Button[] buttons;

    private void Awake()
    {
       MenuChoice();
    }

    private void Start()
    {
        playNewGame.onClick.AddListener((() => PlayNewGame()));
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
        SceneManager.LoadScene(levelName);
    }

    public void PlayNewGame()
    {
        SceneManager.LoadScene("Level1");
        PlayerPrefs.SetInt("UnlockLevel",1);
        PlayerPrefs.Save();
        MenuChoice();
    }
}
