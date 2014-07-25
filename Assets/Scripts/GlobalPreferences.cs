using UnityEngine;
using System.Collections;

public class GlobalPreferences : MonoBehaviour 
{
    public enum SHIP { REMAKER, DSK, DEFAULT };

	public static SHIP shipSelected = SHIP.DEFAULT;

    public static bool SkipTutorial = false;

    public static float CurrentTimeScale = 1f;

	void Awake()
	{
		DontDestroyOnLoad (this);
	}

    public static void SetDefaultTimeScale()
    {
        Time.timeScale = 1f;
        CurrentTimeScale = 1f;
    }

    public static void SetTimeScale(float newScale)
    {
        Time.timeScale = newScale;
        CurrentTimeScale = newScale;
    }
}
