using UnityEngine;
using System.Collections;

public class MainMenuMusic : MonoBehaviour {

    public AudioClip MenuMusicBegin;
    public AudioClip MenuMusicLoop;

    public AudioSource SoundManager;

	void Start () {
        StartCoroutine("PlayMusic");
	}

    IEnumerator PlayMusic()
    {
        SoundManager.PlayOneShot(MenuMusicBegin);
        yield return new WaitForSeconds(MenuMusicBegin.length);
        SoundManager.clip = MenuMusicLoop;
        SoundManager.loop = true;
        SoundManager.Play();
    }
}
