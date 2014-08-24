using UnityEngine;
using System.Collections;

public class DisplayTextTrigger : MonoBehaviour {


    public dfLabel[] Messages;


    void OnTriggerEnter(Collider col) 
    {
        if (col.tag != "Player") { return; }
        foreach (dfLabel label in Messages)
        {
            label.IsVisible = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag != "Player") { return; }
        foreach (dfLabel label in Messages)
        {
            label.IsVisible = false;
        }
    }
}
