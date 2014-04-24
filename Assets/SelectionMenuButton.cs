using UnityEngine;
using System.Collections;

public class SelectionMenuButton : MonoBehaviour {

    public float rotateSpeed;
	
	void Update () {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);
	}
}
