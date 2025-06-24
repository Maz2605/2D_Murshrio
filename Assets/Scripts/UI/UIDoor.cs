using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDoor : MonoBehaviour
{
    public Button buttonOpenDoor;

    private void Start()
    {
        buttonOpenDoor.onClick.AddListener(()=>Open(GameManager.Instance.isKey));
    }

    private void Open(bool value)
    {
        if (PlayerPrefs.GetInt("IsFirst", 0) != 0) return;
        if (value)
        {
            gameObject.SetActive(false);
            GameManager.Instance.isKey = false;
        }
        else
        {
            Debug.Log("You hasn't key");
        }
    }
}
