using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimedTextTrigger : MonoBehaviour {


    public dfLabel[] Messages;


    void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Player") { return; }

        GlobalPreferences.SetTimeScale(0.2f);

        foreach (dfLabel label in Messages)
        {
            label.IsVisible = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag != "Player") { return; }

        GlobalPreferences.SetTimeScale(1f);

        foreach (dfLabel label in Messages)
        {
            label.IsVisible = false;
        }
    }
}
