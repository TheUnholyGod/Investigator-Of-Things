using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RuntimeDelegatePointer : MonoBehaviour {

    [SerializeField]
    UnityEvent m_function;

    private void Awake()
    {

    }

    public void Invoke()
    {
        m_function.Invoke();
    }
}
