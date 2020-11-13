using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEventListener : MonoBehaviour
{
    public string triggerName;
    public UnityEngine.Events.UnityEvent eventCallback;

    // Start is called before the first frame update
    void Start()
    {
        if (triggerName != null && eventCallback != null)
            EventManager.StartListening(triggerName, Callback);
    }

    void OnDisable()
    {
        if (triggerName != null && eventCallback != null)
                EventManager.StopListening(triggerName, Callback);
    }

    void Callback()
    {
        eventCallback.Invoke();
    }
}
