using UnityEngine;
using System.Collections;

public class TutorialCheckBox : MonoBehaviour 
{

    public Texture Unchecked;
    public Texture Checked;

	void OnMouseUp()
	{
        PlayerPreferences.SkipTutorial = !PlayerPreferences.SkipTutorial;
        gameObject.guiTexture.texture = PlayerPreferences.SkipTutorial ? Checked : Unchecked;
	}
}
