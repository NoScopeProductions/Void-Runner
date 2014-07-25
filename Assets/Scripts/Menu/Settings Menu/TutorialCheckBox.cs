using UnityEngine;
using System.Collections;

public class TutorialCheckBox : MonoBehaviour 
{

    public Texture2D Unchecked;
    public Texture2D Checked;

    public void Start()
    {
        gameObject.guiTexture.texture = GlobalPreferences.SkipTutorial ? Checked : Unchecked;
    }

	public void Toggle()
	{
        GlobalPreferences.SkipTutorial = !GlobalPreferences.SkipTutorial;
        gameObject.guiTexture.texture = GlobalPreferences.SkipTutorial ? Checked : Unchecked;
	}
    public void Update()
    {

        #if UNITY_ANDROID
        if (Input.touchCount <= 0) return;

        foreach (var touch in Input.touches)
        {
            if (guiTexture.HitTest(touch.position))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began: //OnMouseDown
                        break;
                    case TouchPhase.Stationary: //OnMouseEnter
                        break;
                    case TouchPhase.Ended: //OnMouseUp
                        Toggle();
                        break;
                }
            }
        }
        #endif
    }

    #if UNITY_EDITOR
    public void OnMouseUp() 
    {
        Toggle();
    }
    #endif

}
