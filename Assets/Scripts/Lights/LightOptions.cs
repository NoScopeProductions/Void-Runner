using UnityEngine;
using System.Collections;

public class LightOptions : MonoBehaviour 
{
	public float speed;
	public float destroyTime;
	
	void Update () 
	{
		transform.Translate(0f,0f, speed * Time.deltaTime,Space.World);
		Destroy(gameObject,destroyTime);
	}
}
