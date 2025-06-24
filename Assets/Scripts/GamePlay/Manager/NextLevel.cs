using System;
using System.Collections;
using System.Collections.Generic;
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
      if (other.CompareTag(namePlayer))
      {
         loadNextLevel.SetActive(true);
         other.gameObject.GetComponent<Player_Controller>().enabled = false;
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
}
