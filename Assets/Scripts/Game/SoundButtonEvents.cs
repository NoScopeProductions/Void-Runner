using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundButtonEvents : MonoBehaviour 
{
	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        AudioListener.pause = !AudioListener.pause;
        AudioListener.volume = 1 - AudioListener.volume;
	}
}
