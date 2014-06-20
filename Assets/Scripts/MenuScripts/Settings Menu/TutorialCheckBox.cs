using UnityEngine;
using System.Collections;

public class TutorialCheckBox : MonoBehaviour 
{

    public Texture Unchecked;
    public Texture Checked;

    public void Start()
    {
        gameObject.guiTexture.texture = PlayerPreferences.SkipTutorial ? Checked : Unchecked;
    }

	void OnMouseUp()
	{
        PlayerPreferences.SkipTutorial = !PlayerPreferences.SkipTutorial;
        gameObject.guiTexture.texture = PlayerPreferences.SkipTutorial ? Checked : Unchecked;
	}
}
