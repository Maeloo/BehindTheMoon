using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class GroupElements : MonoBehaviour {

	public string PlayerPrefChoiceName;

	private Dictionary<string, GameObject> Elements;
	private string _activeElement;
	
	void Start () {
		Elements = new Dictionary<string, GameObject> ( );

		foreach ( Transform child in transform ) {
			Elements.Add ( child.name, child.gameObject );
		}
	}

	[Signal]
	void activeMoonCircle ( ) { _activeElement = "lune_circulaire"; activeElement ( ); }

	[Signal]
	void activeMoonTriangle ( ) { _activeElement = "lune_triangle"; activeElement ( ); }

	[Signal]
	void activeMountain ( ) { _activeElement = "deco_mountain"; activeElement ( );  }

	[Signal]
	void activeWater ( ) { _activeElement = "deco_water"; activeElement ( );  }

	[Signal]
	void activeConstellation ( ) { _activeElement = "deco_constellation"; activeElement ( );  }

	[Signal]
	void activeStar ( ) { _activeElement = "deco_night"; activeElement ( ); }

	[Signal]
	void activeBlue ( ) { _activeElement = "blue_light"; activeElement ( ); }

	[Signal]
	void activeYellow ( ) { _activeElement = "yellow_light"; activeElement ( ); }

	[Signal]
	void activeRed ( ) { _activeElement = "red_light"; activeElement ( ); }

	[Signal]
	void activeViolet ( ) { _activeElement = "purple_light"; activeElement ( ); }

	public void activeElement ( bool start = false ) {
		if ( string.IsNullOrEmpty ( _activeElement ) ) {
			_activeElement = PlayerPrefs.GetString ( PlayerPrefChoiceName );
		}

		PlayerPrefs.SetString ( PlayerPrefChoiceName, _activeElement );

		foreach ( KeyValuePair<string, GameObject> elt in Elements ) {
			string[] digits = Regex.Split ( elt.Key, @"\D+" );

			if ( elt.Key == _activeElement + digits[1] ) {			
				elt.Value.GetComponent<ElementBehaviour> ( ).show ( );					
			}
			else {
				if( start )
					TweenAlpha.Begin ( elt.Value, .5f, 0 );
				else
					elt.Value.GetComponent<ElementBehaviour> ( ).hide ( );
			}
		}
	}
	
	
}
