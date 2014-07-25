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

	void OnMouseUp()
	{
        GlobalPreferences.SkipTutorial = !GlobalPreferences.SkipTutorial;
        gameObject.guiTexture.texture = GlobalPreferences.SkipTutorial ? Checked : Unchecked;
	}
}
