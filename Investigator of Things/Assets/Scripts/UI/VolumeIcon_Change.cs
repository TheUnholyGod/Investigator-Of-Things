using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeIcon_Change : MonoBehaviour {

    public GameObject VolumeIconObject;
    public Sprite VolumeIcon;
    public Sprite VolumeMutedIcon;

    private Slider slider;
    private Image image;

    void Start ()
    {
        slider = GetComponent<Slider>();
        image = VolumeIconObject.GetComponent<Image>();
	}
	
	void Update ()
    {
        if (slider.value == 0)
        {
            if (image.sprite == VolumeIcon)
            {
                image.sprite = VolumeMutedIcon;
            }
        }
        else if (image.sprite == VolumeMutedIcon)
        {
            image.sprite = VolumeIcon;
        }
	}
}
