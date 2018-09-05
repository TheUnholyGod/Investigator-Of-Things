using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodEvent : MonoBehaviour {
    public ParticleSystem Blood;
public void BloodEvent ()
    {
        Blood.Play();
    }
    public void BloodPois ()
    {
        Blood.Stop();

    }

}
