using UnityEngine;
using System.Collections;

public class Main_Play : MonoBehaviour {
	
	private GameObject rockmain;
	public Transform MultiExample;
	
	void Awake() {
	rockmain = GameObject.Find("Rock_Main");
		
	}

	void moveBody(){	
		iTween.MoveBy(rockmain,iTween.Hash("z",100,"time",3.0f,"space",Space.World));
	}
	
	void boom(){
		Instantiate(MultiExample,rockmain.transform.position,Quaternion.identity);
		rockmain.renderer.enabled = false;
		Destroy(GameObject.Find("TextCharSelect"));
		Destroy(GameObject.Find("TextPlay"));
		Destroy(GameObject.Find("TextSettings"));
	}
	
    IEnumerator DoMoving()
    {
		moveBody();
		yield return new WaitForSeconds(1.0f);
		boom ();
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel("Game");
	}
	
	void OnMouseUp() {
		StartCoroutine(DoMoving());
	
	}
}
