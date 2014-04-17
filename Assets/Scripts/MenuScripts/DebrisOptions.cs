using UnityEngine;
using System.Collections;

public class DebrisOptions : MonoBehaviour {
	void Start () 
	{
        Vector3 target = new Vector3(0f,0f, transform.position.z -75);

        Vector3 moveDirection = target - transform.position;
        moveDirection.Normalize();
        rigidbody.AddForce(moveDirection * Random.Range(40,60), ForceMode.Impulse);
	}
		
	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
