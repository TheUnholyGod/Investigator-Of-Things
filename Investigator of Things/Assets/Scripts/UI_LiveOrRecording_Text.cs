using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LiveOrRecording_Text : MonoBehaviour {


    public bool viewingRecordedFootage = false;
    public string liveText = "LIVE";
    public string recordingText = "VIEWING RECORDED FOOTAGE";
    public float flickerStayOnDuration = 0.4f;
    public float flickerStayOffDuration = 0.1f;

    private Text text;
    private float timer = 0f;

	void Start ()
    {
        text = GetComponent<Text>();
	}
	

	void Update ()
    {
        if (viewingRecordedFootage)
        {
            if (text.text != recordingText)
            {
                text.text = recordingText;
                timer = 0f;
            }

            // "Recording" text flicker 
            timer += Time.deltaTime;
            if (text.enabled == true && timer >= flickerStayOnDuration)
            {
                timer = 0f;
                    text.enabled = false;
            }
            if (text.enabled == false && timer >= flickerStayOffDuration)
            {
                timer = 0f;
                text.enabled = true;
            }
            // ------------------------------------------------------
        }
        else if (text.text != liveText)
        {
            text.text = liveText;
        }
	}
}
