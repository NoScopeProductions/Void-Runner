using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour 
{
	private int maxFuel = 100;
	public float currentFuel = 100;
	private float fuelBarLength;

	// Use this for initialization
	void Start () 
	{
		fuelBarLength = Screen.width/2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentFuel = Player.instance.fuel;
	}
	
	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width / 2, 20, fuelBarLength, 20), Mathf.Round(currentFuel).ToString() + "/" + maxFuel);
		GUI.backgroundColor = Color.yellow;
		fuelBarLength = (Screen.width / 2) * (currentFuel / (float)maxFuel);
	}
	
	public void AdjustCurrentFuel(float adjustment)
	{
		Player.instance.fuel += adjustment;
		fuelBarLength = (Screen.width / 2) * (currentFuel / (float)maxFuel);
	}
}
