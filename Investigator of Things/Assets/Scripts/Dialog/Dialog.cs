using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog : ScriptableObject {

    [SerializeField]
    string m_dialog;

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
}
