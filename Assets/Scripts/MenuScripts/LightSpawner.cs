using UnityEngine;
using System.Collections;

public class LightSpawner : MonoBehaviour 
{
	public Transform[] lights;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("SpawnLight", 1.5f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void SpawnLight()
	{
		Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward;
		Transform light = (Transform)Instantiate(lights[Random.Range(0,3)], newPosition, transform.rotation);
		//debris.transform.position = Vector3(Random.Range(-20,20) , Random.Range(-20, 20), 0);
		light.parent = transform;
	}
}
