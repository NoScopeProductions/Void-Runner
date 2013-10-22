using UnityEngine;
using System.Collections;

public class SplashManager : MonoBehaviour 
{
	public float wait = 1.0f;
	public GameObject no, scope, productions;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine("DisplaySplash");
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	
	IEnumerator DisplaySplash()
	{
		yield return new WaitForSeconds(wait);
		no.SetActive(true);

		
		yield return new WaitForSeconds(0.5f);
		scope.SetActive(true);

		
		yield return new WaitForSeconds(0.5f);
		productions.SetActive(true);
		
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel("Menu");

	}
}
