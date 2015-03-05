using UnityEngine;
using System.Collections;

public class AsteroidMovement : MonoBehaviour {
	void Start () 
	{
        Vector3 target = new Vector3(0f,0f, transform.position.z -75);

        Vector3 moveDirection = target - transform.position;
        moveDirection.Normalize();
        GetComponent<Rigidbody>().AddForce(moveDirection * Random.Range(40,60), ForceMode.Impulse);
	}
		
	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
