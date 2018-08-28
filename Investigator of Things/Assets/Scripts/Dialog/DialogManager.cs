﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {

    [SerializeField]
    DialogTree m_DialogTree;

    [SerializeField]
    Dialog m_currDialog;

    [SerializeField]
    TMPro.TextMeshProUGUI m_textbox;

    float m_dialogspeed = 0.25f;

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

    public void TriggerDialog()
    {
        m_textbox.text = "";
        m_DialogTree.MoveToRoot();
        m_currDialog = m_DialogTree.Current;
        StartCoroutine(DialogFunc());
    }

    IEnumerator DialogFunc()
    {
        yield return null;
        int lettercounter = 0;
        
        while (m_currDialog.DialogText != "END")
        {
            while(lettercounter < m_currDialog.DialogText.Length)
            {
                m_textbox.text += m_currDialog.DialogText[lettercounter];
                lettercounter++;
                yield return new WaitForSeconds(m_dialogspeed);
            }
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    m_textbox.text = "";
                    lettercounter = 0;
                    m_DialogTree.MoveDown(0);
                    m_currDialog = m_DialogTree.Current;
                    break;
                }
                yield return null;

            }
            yield return null;

        }
    }
}
