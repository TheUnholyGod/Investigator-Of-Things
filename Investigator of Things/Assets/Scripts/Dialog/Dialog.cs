using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog : ScriptableObject {

    [SerializeField]
    string m_dialog;

    [SerializeField]
    DelegatePointer m_delegatePointer;

    public string DialogText
    {
        get
        {
            return m_dialog;
        }

        set
        {
            m_dialog = value;
        }
    }

    public DelegatePointer DelegatePointer
    {
        get
        {
            return m_delegatePointer;
        }

        set
        {
            m_delegatePointer = value;
        }
    }

    public void Invoke()
    {
        m_delegatePointer.Invoke();
    }
}
