using UnityEngine;
using System;
using System.Collections;

public class LoadingSync : MonoBehaviour {

	[SerializeField] GameObject Loader;
	[SerializeField] GameObject Thumb;

	private UIFilledSprite UIFS_loader;
	private UIFilledSprite UIFS_self;


	void Start ( ) {
		UIFS_self	= gameObject.GetComponentInChildren<UIFilledSprite> ( );
	}

	void Update ( ) {
		if ( UIFS_loader != null ) {
			if ( !Thumb.activeSelf ) {
				Thumb.SetActive ( true );
			}
			else if ( UIFS_self.fillAmount == 0 ) {
				Thumb.SetActive ( false );
			}

			UIFS_self.fillAmount = UIFS_loader.fillAmount;
		}
		else {
			try {
				UIFS_loader = Loader.GetComponentInChildren<UIFilledSprite> ( );
			}
			catch ( Exception e ) {
				Debug.Log ( e.Message );
			}
		}		
	}
}
