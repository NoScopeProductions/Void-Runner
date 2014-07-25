using UnityEngine;
using System.Collections;

public class TutorialCheckBox : MonoBehaviour 
{

    public Texture Unchecked;
    public Texture Checked;

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
