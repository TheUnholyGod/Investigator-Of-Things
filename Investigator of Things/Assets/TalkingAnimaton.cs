using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingAnimaton : MonoBehaviour {


    public bool talking = false;
    public Sprite mouthClosed;
    public Sprite mouthOpen;
    public float mouthStayOpenDuration = 0.3f;
    public float mouthStayClosedDuration = 0.3f;

    private Image image;
    private bool startedTalking = false;
    private float timer = 0f;

    void Start()
    {
        image = GetComponent<Image>();
    }


    void Update()
    {
        if (talking)
        {
            if (!startedTalking)
            {
                image.sprite = mouthOpen;
                timer = 0f;
                startedTalking = true;
            }

            // -------------------------------------------------------
            timer += Time.deltaTime;
            if (image.sprite == mouthOpen && timer >= mouthStayOpenDuration)
            {
                timer = 0f;
                image.sprite = mouthClosed;
            }
            if (image.enabled == mouthClosed && timer >= mouthStayClosedDuration)
            {
                timer = 0f;
                image.sprite = mouthOpen;
            }
            // ------------------------------------------------------
        }
        else if (image.sprite != mouthClosed)
        {
            image.sprite = mouthClosed;
            startedTalking = false;
        }
    }
}