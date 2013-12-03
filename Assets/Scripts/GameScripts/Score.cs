using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    public GUIText GUI_scoreText;

    void Update()
    {
        GUI_scoreText.text = "Score: " + Player.instance.score.ToString();
    }
}
