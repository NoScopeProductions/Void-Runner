using UnityEngine;
using System.Collections;

public class DebrisSpawner : MonoBehaviour 
{
	public float invokeTime;
	public float invokeRepeat;
	public Transform[] prefabs;
	public float distance;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("SpawnDebris", invokeTime, invokeRepeat);
	}
	
	void SpawnDebris()
	{
		Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * distance;

        float angle = Random.Range(0, 2 * Mathf.PI);

        newPosition.x = 80 * Mathf.Cos(angle);
        newPosition.y = 80 * Mathf.Sin(angle);

		Transform debris = (Transform)Instantiate(prefabs[Random.Range(0,5)], newPosition, transform.rotation);
		
		debris.parent = transform;
	}
}
