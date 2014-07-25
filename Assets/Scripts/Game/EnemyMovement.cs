using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour 
{
    public float TravelSpeed;

    private Vector3[] path;

    public void Start()
    {
        path = new[] { 
                       new Vector3(-13f, transform.localPosition.y, transform.localPosition.z), 
                       new Vector3( 13f, transform.localPosition.y, transform.localPosition.z) 
                     };

        TravelSpeed = Random.Range(1f, 3f);
    }
	void Update () 
    {
        transform.localPosition = Vector3.Lerp(path[0], path[1], (Mathf.Sin(TravelSpeed * Time.time) + 1.0f) / 2.0f);
	}
}
