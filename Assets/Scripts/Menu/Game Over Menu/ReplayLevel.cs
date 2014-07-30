using UnityEngine;
using System.Collections;

public class ReplayLevel : MonoBehaviour
{
    public Texture2D ReplayButton_Down;
    public Texture2D ReplayButton_Normal;

    public CameraFade cameraFade;

    #if UNITY_EDITOR
    void OnMouseUp()
    {
        guiTexture.texture = ReplayButton_Normal;
        Activate();
    }

    void OnMouseDown()
    {
        guiTexture.texture = ReplayButton_Down;
    }
    #endif

    private void Activate()
    {
        cameraFade.FadeOut();
        Invoke("ReloadGame", 1.5f);
    }

    public void ReloadGame()
    {
        Application.LoadLevel(Application.loadedLevel);
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
                        guiTexture.texture = ReplayButton_Down;
                        break;
                    case TouchPhase.Ended: //OnMouseUp
                        guiTexture.texture = ReplayButton_Normal;
                        Activate();
                        break;
                }
            }
        }
    }
}
