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

    public Texture2D energyIcon;

    public GUIText Tutorial_Fuel;
    public GUIText Tutorial_Shield;
    public GUIText Tutorial_Speed;
    public GUIText Tutorial_GoodLuck;
    public GUIText Tutorial_Welcome;

    public GameObject Tutorial_Tunnel;

    public GUIText GUI_scoreText;

    public Player PlayerObject;

    private bool TutorialEnabled = true;

	// Use this for initialization
	void Start () 
	{
		fuelBarLength = Screen.width - 20;
	}
	
	// Update is called once per frame
	void Update () 
	{
        currentFuel = PlayerObject.Fuel;
        score = PlayerObject.Score;
	}
	
	void OnGUI()
	{
		DrawFuelBar();
		DrawScore();

        DrawTutorial();
	}

    private void DrawTutorial()
    {
        if (!TutorialEnabled) { return; }

        if (PlayerObject.distanceTraveled > 185)
        {
            Tutorial_Welcome.gameObject.SetActive(false);
            Tutorial_Fuel.gameObject.SetActive(true);
        }

        if (PlayerObject.distanceTraveled > 300)
        {
            Tutorial_Fuel.gameObject.SetActive(false);
            Tutorial_Shield.gameObject.SetActive(true);
        }

        if (PlayerObject.distanceTraveled > 395)
        {
            Tutorial_Shield.gameObject.SetActive(false);
            Tutorial_Speed.gameObject.SetActive(true);
        }

        if (PlayerObject.distanceTraveled > 575)
        {
            Tutorial_Speed.gameObject.SetActive(false);
            Tutorial_GoodLuck.gameObject.SetActive(true);
        }

        if (PlayerObject.distanceTraveled > 740)
        {
            Tutorial_GoodLuck.gameObject.SetActive(false);
            TutorialEnabled = false;
            Destroy(Tutorial_Tunnel);
        }
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
		
		GUI.Box(new Rect(10, 10, fuelBarLength, 40), "", fuelBarStyle);
        GUI.DrawTexture(new Rect(13, 14, 61, 32), energyIcon);
        
		fuelBarLength = (Screen.width - 20) * (currentFuel / (float)maxFuel);
	}
	
	void DrawScore()
	{
        GUI_scoreText.text = "Score: " + PlayerObject.Score.ToString();
	}
}
