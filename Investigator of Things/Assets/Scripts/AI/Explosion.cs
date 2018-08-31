using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    Vector3 scaleFactor = new Vector3(10, 10, 10);
    float cooldowntimer = 0;
    float cooldownreset = 0.5f;

    private void Update()
    {
        cooldowntimer += Time.deltaTime;
        transform.localScale += scaleFactor * Time.deltaTime;
        if(cooldowntimer > cooldownreset)
        {
            Destroy(this.gameObject);
        }
    }

}
