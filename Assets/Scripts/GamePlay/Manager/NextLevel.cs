using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
   public GameObject loadNextLevel;
   public string namePlayer = "Player";
   private void Start()
   {
      Transform camera = GameObject.Find("Main Camera").transform;
      loadNextLevel = camera.Find("LoadNextPlay").gameObject;
   }
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (!CheckCanNextLevel()) return;
      if (other.CompareTag(namePlayer))
      {
         loadNextLevel.SetActive(true);
         other.gameObject.GetComponent<Player_Controller>().enabled = false;
         other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
         Invoke("loadNext",1.3f);
      }
   }
   private void loadNext()
   {
       GameManager.Instance.livePlayer = 3;
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
       UnlockNewLevel();
   }

   private void UnlockNewLevel()
   {
      if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("UnlockLevel",1))
      {
         PlayerPrefs.SetInt("UnlockLevel",PlayerPrefs.GetInt("UnlockLevel",1)+1);
         PlayerPrefs.Save();
      }
   }
    private bool CheckCanNextLevel()
    {
        var allCoins = FindObjectsOfType<DestroyObject>();
        return allCoins.All(x => x.isReceived);
    }
}
