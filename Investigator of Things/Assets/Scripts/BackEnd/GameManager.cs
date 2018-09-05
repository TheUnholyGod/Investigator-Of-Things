using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void FullScreen()
    {
        Screen.SetResolution(Screen.width, Screen.height, !Screen.fullScreen);
    }
}
