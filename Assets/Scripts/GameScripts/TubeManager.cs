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
		int nextTube;
		float offset = 0.005f;
		for (int i = 0; i < numberOfObjects; i++) {
			nextTube = Random.Range(0,10);
			if(i <= 1) nextTube = 0;
			Transform o = (Transform)Instantiate(prefabs[nextTube]);
			o.parent = transform;
			o.Rotate(0,0,22.5f);
			//set random rotation here
			o.transform.Rotate(new Vector3(0f,0f,Random.Range(-2, 2)*45));
			if(i % 2 == 0) nextPosition.y += offset;
			else nextPosition.y -= offset;
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
			//set random rotation?
			o.transform.Rotate(new Vector3(0f,0f,Random.Range(-2, 2)*45));
			objectQueue.Enqueue(o);
		}
	}
}