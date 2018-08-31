using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    [SerializeField]
    int m_DamageVal = 1;

    public int DamageVal
    {
        get
        {
            return m_DamageVal;
        }

        set
        {
            m_DamageVal = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
