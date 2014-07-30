using UnityEngine;
using System.Collections;

public class BackToMainMenu : MonoBehaviour
{
    public Texture2D MenuButton_Down;
    public Texture2D MenuButton_Normal;

    public CameraFade cameraFade;

#if UNITY_EDITOR
    void OnMouseUp()
    {
        guiTexture.texture = MenuButton_Normal;
        Activate();
    }

    void OnMouseDown()
    {
        guiTexture.texture = MenuButton_Down;
    }
#endif

    private void Activate()
    {
        cameraFade.FadeOut();
        Invoke("BackToMenu", 1.5f);
    }

    public void BackToMenu()
    {
        Application.LoadLevel("Menu");
    }

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
                        Activate();
                        break;
                }
            }
        }
    }
}
