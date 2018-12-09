using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// CODE OBSOLETE
public class TextsTutoAnimation : MonoBehaviour {

	public float	MaxTime;
	public int		MaxFlash;

	public bool		activeTuto;
	public bool		desactiveTuto;

	private int		_remainingFlash;

	void Awake ( ) {
		_remainingFlash = MaxFlash * transform.childCount * 2;

		activeTuto		= true;
		desactiveTuto	= false;

		playAnimation ( );
	}

	void playAnimation (  ) {
		foreach ( Transform child in transform ) {
			child.GetComponent<TextTutoBehaviour> ( ).startAnimation ( );
		}
	}

	public int remainingFlash ( ) {
		return _remainingFlash;
	}

	public void decrementFlash ( ) {
		_remainingFlash--;
	}
	
	
}
