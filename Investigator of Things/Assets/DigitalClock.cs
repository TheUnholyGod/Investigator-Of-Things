using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigitalClock : MonoBehaviour {

    public int hours = 12;
    public int minutes = 16;

    public float timeMultiplier = 1f;

    private float timer = 0f;

    private Text text;

	void Start ()
    {
        text = GetComponent<Text>();
	}


	void Update ()
    {
        timer += Time.deltaTime * timeMultiplier;
        if (timer >= 60f)
        {
            timer = 0f;

            if (minutes >= 60)
            {
                hours++;
                minutes = 0;
            }
            else
            {
                minutes++;       
            }

            if (hours >= 24)
            {
                hours = 0;
            }

        }

        string hourString;
        string minuteString;
        string secondString;

        if (hours < 10)
        {
            hourString = "0" + hours.ToString();
        }
        else
        {
            //Debug.Log("blää");
            hourString = hours.ToString();
        }

        if (minutes < 10)
        {
            minuteString = "0" + minutes.ToString();
        }
        else
        {
            minuteString = minutes.ToString();
        }

        if (timer < 10f)
        {
            secondString = "0" + ((int)timer).ToString();
        }
        else
        {
            secondString = ((int)timer).ToString();
        }

        text.text = hourString + ":" + minuteString + ":" + secondString;
	}
}
