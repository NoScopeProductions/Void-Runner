using UnityEngine;
using System.Collections.Generic;

public class TubeManager : MonoBehaviour {

	public Transform[] prefabs;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public float zOffset;

	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;

	void Start () {
		objectQueue = new Queue<Transform>(numberOfObjects);
		nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++) {
			Transform o = (Transform)Instantiate(prefabs[Random.Range(0,3)]);
			o.Rotate(0,0,22.5f);
			//set random rotation here
			o.localPosition = nextPosition;
			nextPosition.z += zOffset;
			objectQueue.Enqueue(o);
		}
	}

	void Update () {
		if (objectQueue.Peek().localPosition.z + recycleOffset < Player.distanceTraveled) {
			Transform o = objectQueue.Dequeue();
			o.localPosition = nextPosition;
			nextPosition.z += zOffset;
			objectQueue.Enqueue(o);
		}
	}
}