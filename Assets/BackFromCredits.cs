using UnityEngine;
using System.Collections;

public class BackFromCredits : MonoBehaviour {

	public GameObject FadeTo;
	public GameObject FadeFrom;

	private void Activate()
	{
		FadeTo.SetActive(true);
		iTween.FadeTo(FadeTo, 1f, 0.4f);
		
		iTween.FadeTo(FadeFrom, iTween.Hash("time", 0.3f, "alpha", 0f, "oncomplete", "HideFrom", "oncompletetarget", gameObject));
	}

	private void HideFrom()
	{
		FadeFrom.SetActive(false);
	}
	
	#if UNITY_ANDROID
	public void Update()
	{
		if (Input.touchCount <= 0) return;
		
		foreach (var touch in Input.touches)
		{
			switch (touch.phase)
			{
			case TouchPhase.Began: //OnMouseDown
				break;
			case TouchPhase.Ended: //OnMouseUp
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
				RaycastHit hit ;
				if (Physics.Raycast (ray, out hit)) {
					Debug.Log(hit.collider.name);
				}
				//Activate();
				break;
			}
		}
	}
	#endif
	
	#if UNITY_EDITOR
	public void OnMouseUp()
	{
		Activate();
	}
	#endif
}
