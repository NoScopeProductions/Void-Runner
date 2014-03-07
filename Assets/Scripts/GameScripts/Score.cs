using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    public GUIText GUI_scoreText;
	public GUIText GUI_fuelText;
    public Player PlayerObject;

    void Update()
    {
        GUI_scoreText.text = "Score: " + PlayerObject.Score.ToString();
        GUI_fuelText.text = "Fuel: " + Mathf.Round(PlayerObject.Fuel).ToString() + "%";
    }
}
