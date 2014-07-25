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
            Transform curEnemy = (Transform)Instantiate(Enemy);

            curEnemy.parent = transform;
            
            curEnemy.localPosition = nextPosition;
            nextPosition.z += zOffset;

            StartCoroutine(SetNewRotation(curEnemy));
            objectQueue.Enqueue(curEnemy);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (objectQueue.Peek().localPosition.z + recycleOffset < PlayerObject.transform.position.z)
        {
            Transform nextEnemy = objectQueue.Dequeue();
            nextEnemy.localPosition = nextPosition;
            
            nextEnemy.localPosition = nextPosition;
            nextPosition.z += zOffset;

            StartCoroutine(SetNewRotation(nextEnemy));
            objectQueue.Enqueue(nextEnemy);
        }
    }

    private IEnumerator SetNewRotation(Transform Enemy)
    {
        while (true)
        {
            curRotation = spawnRotations[Random.Range(0, 8)];
            Enemy.transform.rotation = Quaternion.identity;
            Enemy.transform.Rotate(0f, 180f, curRotation, Space.World);

            RaycastHit hitInfo;
            if (Physics.Raycast(Enemy.position, -Enemy.up, out hitInfo))
            {
                if (hitInfo.transform.tag == "Tunnel") break;
            }

            yield return null;
        }
    }
}


/*
 * 
*/