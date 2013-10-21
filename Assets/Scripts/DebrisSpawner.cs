using UnityEngine;
using System.Collections;

public class DebrisSpawner : MonoBehaviour 
{
	public Transform[] prefabs;
	public float distance;


	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("SpawnDebris", 1.0f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void SpawnDebris()
	{
		
		Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * distance;
		newPosition.x = Random.Range(-5, 5);
		newPosition.y = Random.Range(-5, 5);
		Transform debris = (Transform)Instantiate(prefabs[Random.Range(0,5)], newPosition, transform.rotation);
		//debris.transform.position = Vector3(Random.Range(-20,20) , Random.Range(-20, 20), 0);
		debris.parent = transform;
	}
}
