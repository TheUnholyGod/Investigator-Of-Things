using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable, CreateAssetMenu(fileName = "Delegate", menuName = "Function/Delegate")]
public class DelegatePointer : ScriptableObject {

    [SerializeField]
    UnityEvent m_function;

    public UnityEvent Function
    {
        get
        {
            return m_function;
        }

        set
        {
            m_function = value;
        }
    }

    private void Awake()
    {
        
    }

    public void Invoke()
    {
        Function.Invoke();
    }

    public static DelegatePointer CreateInstance(DelegatePointer _pointer)
    {
        DelegatePointer ret = CreateInstance<DelegatePointer>();
        ret.Function = _pointer.Function;
        return ret;
    }

}
