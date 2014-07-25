﻿using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour 
{
	private int maxFuel = 100;
	private float currentFuel = 100;
	private float fuelBarLength;

	public Texture2D fuelBarGREEN;
	public Texture2D fuelBarORANGE;
	public Texture2D fuelBarRED;

    public Texture2D energyIcon;

    public GUIText GUI_scoreText;

    public Player PlayerObject;

    public GameOverMenu endGameMenu;

    public GameObject[] DisableOnDeath;

	// Use this for initialization
	void Start () 
	{
		fuelBarLength = Screen.width - 20;
	}
	
	// Update is called once per frame
	void Update () 
	{
        currentFuel = PlayerObject.Fuel;
	}

    public void ShowGameOverMenu()
    {
        endGameMenu.score = PlayerObject.Score;
        endGameMenu.boostPickupCount = PlayerObject.redPickupCount;
        endGameMenu.fuelPickupCount = PlayerObject.greenPickupCount;
        endGameMenu.shieldPickupCount = PlayerObject.bluePickupCount;
        endGameMenu.DistanceTraveled = PlayerObject.DistanceTraveled;

        endGameMenu.gameObject.SetActive(true);
        GUI_scoreText.gameObject.SetActive(false);

        foreach (var obj in DisableOnDeath)
        {
            obj.SetActive(false);
        }

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
		
		GUI.Box(new Rect(10, 10, fuelBarLength, 70), "", fuelBarStyle);
        GUI.DrawTexture(new Rect(13, 14, 85, 60), energyIcon);
        
		fuelBarLength = (Screen.width - 20) * (currentFuel / (float)maxFuel);
	}
	
	void DrawScore()
	{
        GUI_scoreText.text = "Score: " + Mathf.Round(PlayerObject.Score).ToString();
	}
}