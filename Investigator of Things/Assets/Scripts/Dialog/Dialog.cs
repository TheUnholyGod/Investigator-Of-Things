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

    public static Dialog CreateInstance(Dialog _dialog)
    {
        Dialog ret = CreateInstance<Dialog>();
        ret.m_dialog = _dialog.m_dialog;
        if(_dialog.DelegatePointer != null)
            ret.m_delegatePointer = DelegatePointer.CreateInstance(_dialog.DelegatePointer);
        return ret;
    }
}
