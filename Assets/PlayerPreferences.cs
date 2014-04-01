using UnityEngine;
using System.Collections;

public class PlayerPreferences : MonoBehaviour 
{
	public static int shipSelected;


	void Awake()
	{
		DontDestroyOnLoad (this);
	}
}
