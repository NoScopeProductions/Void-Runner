﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimedTextTrigger : MonoBehaviour {


    public GameObject[] Messages;


    void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Player") { return; }

        PlayerPreferences.SetTimeScale(0.2f);

        foreach (GameObject gameObj in Messages)
        {
            gameObj.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag != "Player") { return; }

        PlayerPreferences.SetTimeScale(1f);

        foreach (GameObject gameObj in Messages)
        {
            gameObj.SetActive(false);
        }
    }
}
