using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObseverPattern : Singleton<ObseverPattern>
{
    public UnityAction unityAction;
    protected override void Awake()
    {
        base.KeepAlive(true);
        base.Awake();
    }
}
