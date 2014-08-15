using UnityEngine;
using System.Collections;

public class LoadCredits : MonoBehaviour {

    public TextAsset CreditsFile;

    public dfRichTextLabel TextLabel;

	// Use this for initialization
	void Start () 
    {
        TextLabel.Text = CreditsFile.text;
	}
}
