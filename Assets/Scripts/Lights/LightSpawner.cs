using UnityEngine;
using System.Collections;

public class LightSpawner : MonoBehaviour 
{
	public Transform[] lights;
    public Player PlayerObject;

    private bool sendingLights = false;
	// use this for initialization
	void Start() 
	{
		
	}

    void Update()
    {
        if (PlayerObject.State == Player.PlayerState.BOOSTING)
        {
            if (!sendingLights)
            {
                sendingLights = true;
                InvokeRepeating("SpawnLight", .5f, 1.5f);
            }
        }
        else
        {
            if (sendingLights)
            {
                sendingLights = false;
                CancelInvoke("SpawnLight");
            }
        }
    }

	void SpawnLight()
	{
		Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward;
		Transform light = (Transform)Instantiate(lights[Random.Range(0, lights.Length)], newPosition, transform.rotation);
		light.parent = transform;
	}
}
