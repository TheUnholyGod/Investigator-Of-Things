using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraStaticEffect : MonoBehaviour
{

    public List<Sprite> sprites = new List<Sprite>();
    public float animDelay = 0.05f;

    private Image image;
    private int count = 1;
    private float timer = 0;


    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprites[0];
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= animDelay)
        {
            count++;
            if (count > sprites.Count)
            {
                count = 1;
            }
            image.sprite = sprites[count - 1];
            timer = 0;
        }
    }
}
