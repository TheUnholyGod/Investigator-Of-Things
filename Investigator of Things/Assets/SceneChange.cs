using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene 2");
    }

    public void LoadMainMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainmenu");
    }
}
