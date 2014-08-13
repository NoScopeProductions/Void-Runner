using UnityEngine;
using System.Collections;

public class ShipNameChanger : MonoBehaviour 
{

	public dfCoverflow Scroller;

	public string[] ShipNames;

	private dfLabel label;

	void Start () 
	{
		this.label = GetComponent<dfLabel>();
	}

	void Update()
	{
		if( Scroller == null || ShipNames == null || ShipNames.Length == 0 )
			return;
		
		var index = Mathf.Max( 0, Mathf.Min( ShipNames.Length - 1, Scroller.selectedIndex ) );
		label.Text = ShipNames[ index ];
	}
}
