using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEndScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLastScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("endscene");
    }
}
