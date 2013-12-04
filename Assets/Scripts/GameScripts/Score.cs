using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    public GUIText GUI_scoreText;
	public GUIText GUI_fuelText;

    void Update()
    {
        GUI_scoreText.text = "Score: " + Player.instance.score.ToString();
		GUI_fuelText.text = "Fuel: " + Mathf.Round(Player.instance.fuel).ToString() + "%";
    }
}
