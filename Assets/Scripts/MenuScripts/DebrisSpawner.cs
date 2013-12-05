using UnityEngine;
using System.Collections;

public class DebrisSpawner : MonoBehaviour 
{
	public float invokeTime;
	public float invokeRepeat;
	public Transform[] prefabs;
	public float distance;
	public int min_X;
	public int max_X;
	public int min_Y;
	public int max_Y;


	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("SpawnDebris", invokeTime, invokeRepeat);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void SpawnDebris()
	{
		
		Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * distance;
		newPosition.x = Random.Range(min_X, max_X);
		newPosition.y = Random.Range(min_Y, max_Y);
		Transform debris = (Transform)Instantiate(prefabs[Random.Range(0,5)], newPosition, transform.rotation);
		//debris.transform.position = Vector3(Random.Range(-20,20) , Random.Range(-20, 20), 0);
		debris.parent = transform;
	}
}
