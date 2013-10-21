using UnityEngine;
using System.Collections;

public class DebrisOptions : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	//destroy when it gets out of view of camera
	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
