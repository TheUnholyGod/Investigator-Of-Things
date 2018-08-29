using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {

    [SerializeField]
    DialogTree m_DialogTree;

    [SerializeField]
    Dialog m_currDialog;

    [SerializeField]
    TMPro.TextMeshProUGUI m_textbox;

    bool hasDialog = false;

    float m_dialogspeed = 0.025f;

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

    public DialogTree DialogTree
    {
        get
        {
            return m_DialogTree;
        }

        set
        {
            m_DialogTree = value;
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
        if (hasDialog)
            return;
        hasDialog = true;
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
            if (m_currDialog.DelegatePointer != null)
                m_currDialog.Invoke();
            while(lettercounter < m_currDialog.DialogText.Length)
            {
                m_textbox.text += m_currDialog.DialogText[lettercounter];
                lettercounter++;
                yield return new WaitForSeconds(m_dialogspeed);
            }
            while (m_DialogTree.Current == m_currDialog)
            {
                bool changed = false;
                foreach (int i in m_DialogTree.GetPaths())
                {
                    if (Input.GetKeyDown((i + 1).ToString()))
                    {
                        lettercounter = 0;
                        m_textbox.text = "";
                        m_DialogTree.MoveDown(i);
                        m_currDialog = m_DialogTree.Current;
                        changed = true;
                        break;
                    }
                }
                if (changed == true)
                    break;
                yield return 0;
            }
        }
        hasDialog = false;
    }
}
