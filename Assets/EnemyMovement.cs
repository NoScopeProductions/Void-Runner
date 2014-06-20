using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour 
{
    public float Speed;

    public bool toggle = true;

    public float travelDistance = 13f;

    public float localX;

	void Update () 
    {
        if (toggle)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime, Space.Self);

        }
        else
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime, Space.Self);
        }
        CheckBounds();

        localX = transform.localPosition.x;
	}

    private void CheckBounds()
    {
        if (transform.localPosition.x >= 13f ||
            transform.localPosition.x <= -13f)
        {
            toggle = !toggle;
        }

        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -travelDistance, travelDistance), transform.localPosition.y, transform.localPosition.z);
    }
}
