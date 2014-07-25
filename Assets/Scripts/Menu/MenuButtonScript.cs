using UnityEngine;
using System.Collections;

public class MenuButtonScript : MonoBehaviour 
{
    void OnMouseUp()
    {
        Application.LoadLevel("Menu");
    }
}
