using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DangerFlash : MonoBehaviour {

    private bool Enabled = false;

    public float FadeTime;

    public void Enable() 
    {
        if (!Enabled)
        {
            Enabled = true;
            //tween here
            iTween.FadeTo(gameObject, iTween.Hash("alpha", 0.5f,
                                                  "time", FadeTime,
                                                  "transition", "linear",
                                                  "looptype", iTween.LoopType.pingPong,
                                                  "name", "danger_tween",
                                                  "ignoretimescale", true
                                                  ));
        }
    }

    public void Disable()
    {
        if (Enabled)
        {
            Enabled = false;
            iTween.StopByName("danger_tween");
            iTween.FadeTo(gameObject, iTween.Hash("alpha", 0f,
                                                  "time", FadeTime / 2,
                                                  "transition", "linear",
                                                  "ignoretimescale", true
                                                  ));
        }
    }

    
    public bool IsEnabled() 
    {
        return Enabled;
    }
}
