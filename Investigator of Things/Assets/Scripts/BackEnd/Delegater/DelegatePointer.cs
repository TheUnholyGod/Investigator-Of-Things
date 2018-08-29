using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable, CreateAssetMenu(fileName = "Delegate", menuName = "Function/Delegate")]
public class DelegatePointer : ScriptableObject {

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
