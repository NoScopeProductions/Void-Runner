using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectibleManager : MonoBehaviour {
	
	public Transform prefab;
	public int numberOfObjects;
	
	public float recycleOffset;
	public Vector3 startPosition;
	public float zOffset;

	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	
	public Vector2[] spawnPoints;
	private Vector2 curSpawn;
	// Use this for initialization
	void Start () {
		objectQueue = new Queue<Transform>(numberOfObjects);
		nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++) {
			Transform o = (Transform)Instantiate(prefab);
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
	void Update () {
		
		if (objectQueue.Peek().localPosition.z + recycleOffset < Player.distanceTraveled) {
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
}
