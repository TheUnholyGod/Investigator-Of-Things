using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {

    [SerializeField]
    Dialog m_currDialog;

    public Dialog Dialog
    {
        get
        {
            return m_currDialog;
        }

        set
        {
            m_currDialog = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
