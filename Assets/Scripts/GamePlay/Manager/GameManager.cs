using System;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int livePlayer = 3;
    public int idAnimator = 0;
    public bool isProtect = false;
    public bool isKey = false;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livePlayer = 3;
        idAnimator = 0;
        isProtect = false;
    }
}