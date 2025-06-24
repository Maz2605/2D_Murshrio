using System;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher : MonoBehaviour
{
    private static EventDispatcher instance;

    public static EventDispatcher Instance
    {
        get
        {
            // Nếu chưa có EventDispatcher trên Scene thì tạo 1 cái mới và AddComponent
            if (instance == null)
            {
                GameObject singletonObject = new GameObject();
                instance = singletonObject.AddComponent<EventDispatcher>();
                singletonObject.name = "EventDispatcher (Singleton)";
            }

            return instance;
        }
    }

    private void Awake()
    {
        // Nếu trên Scene có 1 GameObject khác cũng tên là EventDispatcher (Singleton) mà khác InstanceID thì huỷ
        // cái đó đi chỉ giữ lại 1 thể hiện thôi, còn trùng ID thì gán thành instance
        if (instance != null && instance.GetInstanceID() != this.GetInstanceID())
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private Dictionary<EventID, Action<object>> gameEvent = new Dictionary<EventID, Action<object>>();
    // Đăng kí lắng nghe sự kiện
    public void RegisterListener(EventID eventID,Action<object> callback)
    {
        // nếu gameEvent chứa eventID
        if (gameEvent.ContainsKey(eventID))
        {
            gameEvent[eventID] += callback;
        }
        else
        {
            gameEvent.Add(eventID,null);
            gameEvent[eventID] += callback;
        }
    }
    // Bắn sự kiện cho các object đăng kí lắng nghe
    public void PostEvent(EventID eventID,object param = null)
    {
        // nếu không có object nào đăng kí lắng nghe trong Dictional thì thông báo không có object nào đk 
        if (!gameEvent.ContainsKey(eventID))
        {
            Debug.Log("Event has not Register");
            return;
        }

        var callbacks = gameEvent[eventID];
        // nếu không có đối tượng bắt sự kiện thì thôi
        if (callbacks != null)
        {
            callbacks(param);
        }
        else
        {
            gameEvent.Remove(eventID);
        }
    }
    // dùng Unregister để không lắng nghe sự kiện nữa
    public void RemoveListener(EventID eventID, Action<object> callback)
    {
        if (gameEvent.ContainsKey(eventID))
        {
            gameEvent[eventID] -= callback;
        }
        else
        {
            Debug.Log("Event has not ");
            return;
        }
    }
}

public static class EventDispatcherExtension
{
    public static void RegisterListener(this MonoBehaviour listener, EventID eventID, Action<object> callback)
    {
        EventDispatcher.Instance.RegisterListener(eventID,callback);
    }

    public static void PostEvent(this MonoBehaviour listener, EventID eventID, object param)
    {
        EventDispatcher.Instance.PostEvent(eventID,param);
    }
}