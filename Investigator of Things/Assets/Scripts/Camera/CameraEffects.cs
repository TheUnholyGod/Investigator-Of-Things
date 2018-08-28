using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraEffects : MonoBehaviour {

    Image image;
    CameraStaticEffect effect;
    public float effectDuration = 2.0f;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.enabled = false;
        effect = GetComponent<CameraStaticEffect>();
        effect.enabled = false;
    }

	// Update is called once per frame
	void Update () {
		if (CameraManager.GetInstance().cameraTransition)
        {
            image.enabled = true;
            effect.enabled = true;
            image.color = Color.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, 0.5f), effectDuration * 0.5f);

            if (image.color.a == 0.5f)
                CameraManager.GetInstance().cameraTransition = false; ;
        }
        else
        {
            image.color = Color.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, 0.12f), effectDuration * 0.5f);

            if (image.color.a == 0.12f)
            {
                image.enabled = false;
                effect.enabled = false;
            }
        }
	}
}
