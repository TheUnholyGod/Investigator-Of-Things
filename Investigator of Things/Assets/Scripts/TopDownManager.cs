using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownManager : MonoBehaviour {

    [SerializeField]
    MineArea wincon;

    [SerializeField]
    GameObject wintext;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (wincon.complete)
        {
            wintext.SetActive(true);
            if (Input.anyKey)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("murderscene");
            }
        }
	}
}
