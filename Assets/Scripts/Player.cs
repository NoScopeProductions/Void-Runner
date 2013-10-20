using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public static float distanceTraveled;
	public float speed;
	// Use this for initialization
	void Start () {
		distanceTraveled = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0f, 0f, speed * Time.deltaTime);
		transform.RotateAround(Vector3.zero, transform.forward, Time.deltaTime * 20f);
		distanceTraveled = transform.localPosition.z;
	}
}
