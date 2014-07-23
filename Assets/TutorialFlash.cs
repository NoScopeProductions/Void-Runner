using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialFlash : MonoBehaviour {

    void OnEnable()
    {
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0f, 
                                              "time", 0.5f, 
                                              "transition", "linear", 
                                              "looptype", iTween.LoopType.pingPong, 
                                              "name", "tutorial_tween",
                                              "ignoretimescale", true
                                              ));
    }


    void OnDisable() 
    {
        iTween.StopByName("tutorial_tween");
    }
}
