using UnityEngine;
using System.Collections;

public class ReplayButtonScript : MonoBehaviour 
{
    void OnMouseUp()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
