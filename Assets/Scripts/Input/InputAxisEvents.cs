using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputAxisEvents : MonoBehaviour
{
    public List<AxisEventHandler> eventHandlers;

    // Update is called once per frame
    void Update()
    {
        float axisValue;
        foreach (var item in eventHandlers)
        {
            axisValue = item.isRawValue ? Input.GetAxisRaw(item.axisName) : Input.GetAxis(item.axisName);
            item.handlers.Invoke(axisValue);
        }
    }
}

[System.Serializable]
public struct AxisEventHandler
{
    public string axisName;
    public bool isRawValue;
    public MyFloatEvent handlers;
}

[System.Serializable]
public class MyFloatEvent : UnityEvent<float> { }