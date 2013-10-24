using UnityEngine;
using System.Collections;

public class LightOptions : MonoBehaviour 
{
	public float speed;
	public float destroyTime;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(0f,0f, speed * Time.deltaTime,Space.World);
		Destroy(gameObject,destroyTime);
	}
}
