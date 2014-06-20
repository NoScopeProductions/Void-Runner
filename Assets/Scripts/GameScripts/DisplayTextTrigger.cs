using UnityEngine;
using System.Collections;

public class DisplayTextTrigger : MonoBehaviour {


    public GameObject[] Messages;


    void OnTriggerEnter(Collider col) 
    {
        if (col.tag != "Player") { return; }
        foreach (GameObject gameObj in Messages)
        {
            gameObj.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag != "Player") { return; }
        foreach (GameObject gameObj in Messages)
        {
            gameObj.SetActive(false);
        }
    }
}
