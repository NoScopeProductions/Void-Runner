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

    public Player PlayerObject;

	void Start () {
		objectQueue = new Queue<Transform>(numberOfObjects);
		nextPosition = startPosition;
		int nextTube;
		for (int i = 0; i < numberOfObjects; i++) {
			nextTube = Random.Range(0,10);
			Transform o = (Transform)Instantiate(prefabs[nextTube]);
			o.parent = transform;
			o.localPosition = nextPosition;
			nextPosition.z += zOffset;
			objectQueue.Enqueue(o);
		}
	}

	void Update () {
        if (objectQueue.Peek().localPosition.z + recycleOffset < PlayerObject.distanceTraveled)
        {
			Transform o = objectQueue.Dequeue();
			o.localPosition = nextPosition;
			nextPosition.z += zOffset;
			objectQueue.Enqueue(o);
		}
	}
}