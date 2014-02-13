using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour 
{
	public bool isLoading = false;
	public Texture loadingCircle;
	public float size = 100.0f;
	private float rotationAngle = 0.0f;
	public float rotationSpeed = 200.0f;

	
	// Update is called once per frame
	void Update () 
	{
		if(isLoading)
		{
			rotationAngle += rotationSpeed * Time.deltaTime;
		}
	}

	void OnGUI()
	{
		Vector2 ceneterPoint = new Vector2(Screen.width/2, Screen.height/2);
		GUIUtility.RotateAroundPivot(rotationAngle, ceneterPoint);
		GUI.DrawTexture(new Rect ((Screen.width - size)/2 , (Screen.height - size)/2, size, size), loadingCircle);
	}
}
