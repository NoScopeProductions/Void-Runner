using UnityEngine;
using System.Collections;

public class PlayerPreferences : MonoBehaviour 
{
    public enum SHIP { REMAKER, DSK, DEFAULT };

	public static SHIP shipSelected = SHIP.DEFAULT;

    public static bool SkipTutorial = false;


	void Awake()
	{
		DontDestroyOnLoad (this);
	}
}
