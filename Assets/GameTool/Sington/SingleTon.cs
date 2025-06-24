using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<T>(true);
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)(MonoBehaviour)this;
        }
    }
}
