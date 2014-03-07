using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour 
{
	private int score;
	private int maxFuel = 100;
	private float currentFuel = 100;
	private float fuelBarLength;
	public Texture2D fuelBarGREEN;
	public Texture2D fuelBarORANGE;
	public Texture2D fuelBarRED;

    public Player PlayerObject;

	// Use this for initialization
	void Start () 
	{
		fuelBarLength = Screen.width/2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetFuel();
		GetScore();
	}
	
	void GetScore()
	{
        score = PlayerObject.Score;
	}
	void GetFuel()
	{
        currentFuel = PlayerObject.Fuel;
	}
	
	void OnGUI()
	{
		DrawFuelBar();
		DrawScore();
	}
	
	void DrawFuelBar()
	{
		
		GUIStyle fuelBarStyle = new GUIStyle();
		if(currentFuel > 50.0f)
		{
			fuelBarStyle.normal.background = fuelBarGREEN;
		}
		if(currentFuel < 50.0f)
		{
			fuelBarStyle.normal.background = fuelBarORANGE;
		}
		if(currentFuel < 30.0f)
		{
			fuelBarStyle.normal.background = fuelBarRED;
		}
		
		GUI.Box(new Rect(Screen.width / 2, 20, fuelBarLength, 20), Mathf.Round(currentFuel).ToString() + "/" + maxFuel, fuelBarStyle);
		GUI.contentColor = Color.yellow;
		fuelBarLength = (Screen.width / 2) * (currentFuel / (float)maxFuel);
	}
	
	void DrawScore()
	{
		//Figure out how to draw the score here!
	}
}
