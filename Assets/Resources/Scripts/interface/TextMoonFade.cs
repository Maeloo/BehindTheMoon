using UnityEngine;
using System.Collections;

public class TextMoonFade : MonoBehaviour {

	public float FadeDuration;

	public void fadeIn ( ) {
		foreach ( Transform child in transform ) {
			UILabel label = child.GetComponent<UILabel> ( );

			if ( label != null ) {
				label.alpha = 0;
				TweenAlpha.Begin ( child.gameObject, FadeDuration, 1 );
			}			
		}
	}

	public void fadeOut ( ) {
		foreach ( Transform child in transform ) {
			UILabel label = child.GetComponent<UILabel> ( );

			if ( label != null ) {
				label.alpha = 1;
				TweenAlpha.Begin ( child.gameObject, FadeDuration, 0 );
			}
		}
	}

}
