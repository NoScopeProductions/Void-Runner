using UnityEngine;
using System.Collections;

public class BackToMainMenu : MonoBehaviour
{
    public Texture2D MenuButton_Down;
    public Texture2D MenuButton_Normal;

#if UNITY_EDITOR
    void OnMouseUp()
    {
        guiTexture.texture = MenuButton_Normal;
        Application.LoadLevel("Menu");
    }

    void OnMouseDown()
    {
        guiTexture.texture = MenuButton_Down;
    }
#endif

    public void Update()
    {
        if (Input.touchCount <= 0) return;

        foreach (var touch in Input.touches)
        {
            if (guiTexture.HitTest(touch.position))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began: //OnMouseDown
                        guiTexture.texture = MenuButton_Down;
                        break;
                    case TouchPhase.Ended: //OnMouseUp
                        guiTexture.texture = MenuButton_Normal;
                        Application.LoadLevel("Menu");
                        break;
                }
            }
        }
    }
}
