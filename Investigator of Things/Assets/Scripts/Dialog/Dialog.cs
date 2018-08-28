using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable,CreateAssetMenu(fileName = "NewDialog")]
public class Dialog : ScriptableObject {

    [SerializeField]
    string[] m_dialogs;

}
