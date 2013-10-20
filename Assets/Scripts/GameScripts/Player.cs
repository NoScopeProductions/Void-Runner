using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public static float distanceTraveled;
	public float speed;
	public float turnSpeed;
	
	// Use this for initialization
	void Start () {
		distanceTraveled = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0f, 0f, speed * Time.deltaTime);
		distanceTraveled = transform.localPosition.z;
		checkInput();
	}
	
	void checkInput() {
		if(Input.GetKey(KeyCode.LeftArrow)) {
			transform.RotateAround(Vector3.zero, transform.forward, Time.deltaTime * -turnSpeed);	
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			transform.RotateAround(Vector3.zero, transform.forward, Time.deltaTime * turnSpeed);	
		}
	}
}


