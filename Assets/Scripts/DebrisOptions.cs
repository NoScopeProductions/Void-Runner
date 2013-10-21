using UnityEngine;
using System.Collections;

public class DebrisOptions : MonoBehaviour {
	public float speed;
	float xSpin, ySpin, zSpin;

	// Use this for initialization
	void Start () 
	{
		xSpin = Random.Range(0,360);
		ySpin = Random.Range(0,360);
		zSpin = Random.Range(0,360);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(0f,0f, -speed * Time.deltaTime,Space.World);
		transform.rotation = Quaternion.Euler(xSpin,ySpin,zSpin);
	}
	//destroy when it gets out of view of camera
		
	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
