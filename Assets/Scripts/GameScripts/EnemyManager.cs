using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{

    public Transform Enemy;
    public int numberOfObjects;

    public float recycleOffset;
    public Vector3 startPosition;
    public float zOffset;

    public Vector3 nextPosition;
    private Queue<Transform> objectQueue;

    public float[] spawnRotations;
    private float curRotation;

    public Player PlayerObject;


    // Use this for initialization
    void Start()
    {
        objectQueue = new Queue<Transform>(numberOfObjects);
        nextPosition = startPosition;

        for (int i = 0; i < numberOfObjects; i++)
        {
            Transform o = (Transform)Instantiate(Enemy);

            o.parent = transform;
            //select one of the 8 spawn points and place this collectible there.
            curRotation = spawnRotations[Random.Range(0, 8)];

            o.localPosition = nextPosition;
            nextPosition.z += zOffset;
            o.transform.Rotate(0f, 0f, curRotation, Space.World);
            objectQueue.Enqueue(o);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (objectQueue.Peek().localPosition.z + recycleOffset < PlayerObject.transform.position.z)
        {
            Transform o = objectQueue.Dequeue();
            o.localPosition = nextPosition;
            //select one of the 8 spawn points and place this collectible there.
            curRotation = spawnRotations[Random.Range(0, 8)];

            o.transform.rotation = Quaternion.identity;
            o.localPosition = nextPosition;
            nextPosition.z += zOffset;
            o.transform.Rotate(0f, 0f, curRotation, Space.World);
            objectQueue.Enqueue(o);
        }
    }
}
