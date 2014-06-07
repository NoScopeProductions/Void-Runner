using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectibleManager : MonoBehaviour 
{
	
	public Transform[] prefabs;
	public int numberOfObjects;
	
	public float recycleOffset;
	public Vector3 startPosition;
	public float zOffset;

	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;

	public Vector2[] spawnPoints;
	private Vector2 curSpawn;

	private const int TYPE_FUEL = 0;
	private const int TYPE_SHIELD = 1;
	private const int TYPE_SPEED = 2;

    public Player PlayerObject;


	// Use this for initialization
	void Start () 
	{
		objectQueue = new Queue<Transform>(numberOfObjects);
		nextPosition = startPosition;

		for (int i = 0; i < numberOfObjects; i++) 
		{
			int index = getPowerupType();

			Transform o = (Transform)Instantiate(prefabs[index]);
		
			o.parent = transform;
			//select one of the 8 spawn points and place this collectible there.
			curSpawn = spawnPoints[Random.Range(0,8)];
			nextPosition.x = curSpawn.x;
			nextPosition.y = curSpawn.y;
			
			o.localPosition = nextPosition;
			nextPosition.z += zOffset;
			objectQueue.Enqueue(o);
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (objectQueue.Peek().localPosition.z + recycleOffset < PlayerObject.distanceTraveled) 
		{
			Transform o = objectQueue.Dequeue();
			o.localPosition = nextPosition;
			//select one of the 8 spawn points and place this collectible there.
			curSpawn = spawnPoints[Random.Range(0,8)];
			nextPosition.x = curSpawn.x;
			nextPosition.y = curSpawn.y;
			nextPosition.z += zOffset;
			objectQueue.Enqueue(o);
		}
	}

	private int getPowerupType() 
	{
        //return TYPE_SPEED;
		int chance = Random.Range(1,101);

		//There is a 75% chance of the type being fuel.
		if(chance < 75) 
		{
			return TYPE_FUEL;
		}
		else 
		{
			//otherwise, we roll the dice again
			chance = Random.Range(1,101);

			//There is now a 10% chance of the type being a speed boost
			if(chance < 10) 
			{
				return TYPE_SPEED;
			}
			//otherwise, we give a shield.
			else {
				return TYPE_SHIELD;
			}
		}
	}
}
