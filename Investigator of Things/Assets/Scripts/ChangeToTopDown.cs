using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToTopDown : MonoBehaviour {
    [SerializeField]
    GameObject[] m_toggles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Trigger()
    {
        foreach (GameObject go in m_toggles)
            Destroy(go);
    }
}
