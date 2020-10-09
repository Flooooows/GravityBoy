using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CdProjeCt.EventTypes
{
    [System.Serializable]
    public class IntEvent : UnityEvent<int>{}
    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }
    [System.Serializable]
    public class StringEvent : UnityEvent<string> { }
    [System.Serializable]
    public class TransformEvent : UnityEvent<Transform> { }
    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }
    [System.Serializable]
    public class GameObjectInteractionEvent : UnityEvent<GameObject, GameObject> { }
    
}