using UnityEngine;
using System.Collections;

public class PopupBehaviour : MonoBehaviour {

	static bool PopupActive = false;

	[SerializeField] ButtonCustom[]		Buttons;
	[SerializeField] UIFilledSprite[]	Lines;
	[SerializeField] GameObject			TexBig;
	[SerializeField] GameObject[]		ObjectToFade;

	void Start ( ) {
		foreach ( GameObject go in ObjectToFade ) {
			TweenAlpha.Begin ( go, 0, 0 );

			go.SetActive ( false );
		}
	}

	public void DisplayPopup ( ) {
		if ( !PopupActive ) {
			PopupActive = true;
			SwipeManager.Pause ( );

			showBackground1 ( );
		}		
	}

	public void HidePopup ( ) {
		hideButtons ( );
	}

	void showBackground1 ( ) {
		iTween.ValueTo ( gameObject, iTween.Hash ( 
			"from", 0,
			"to", 1,
			"time", .3f,
			"onupdate", "updateLines",
			"oncomplete", "showBackground2",
			"oncompletetarget", gameObject
			) );
	}

	void showBackground2 ( ) {
		TweenScale.Begin ( TexBig, .3f, new Vector3 ( 1280, 360, 1 ) );

		StartCoroutine ( wait0 ( ) );

		foreach ( UIFilledSprite line in Lines ) {
			line.gameObject.GetComponent<TweenPosition> ( ).Play ( true );
		}
	}

	void hideBackground2 ( ) {
		iTween.ValueTo ( gameObject, iTween.Hash (
			"from", 1,
			"to", 0,
			"time", .3f,
			"onupdate", "updateLines"
			) );
	}

	void hideBackground1 ( ) {
		TweenScale.Begin ( TexBig, .3f, new Vector3 ( 1280, 0, 1 ) );

		StartCoroutine ( wait1 ( ) );

		foreach ( UIFilledSprite line in Lines ) {
			line.gameObject.GetComponent<TweenPosition> ( ).Play ( false );
		}
	}

	void displayButtons ( ) {
		foreach ( ButtonCustom btn in Buttons ) {
			btn.Show ( );
		}

		foreach ( GameObject go in ObjectToFade ) {
			go.SetActive ( true );
			go.GetComponent<FixDepth> ( ).fixDepth ( );

			TweenAlpha.Begin ( go, .4f, 1 );			
		}
	}

	void hideButtons ( ) {
		foreach ( ButtonCustom btn in Buttons ) {
			btn.Hide ( );
		}

		foreach ( GameObject go in ObjectToFade ) {
			TweenAlpha.Begin ( go, .4f, 0 );

			go.GetComponent<FixDepth> ( ).fixDepth ( );
		}


		StartCoroutine ( wait ( ) );
	}

	IEnumerator wait0 ( ) {
		yield return new WaitForSeconds ( .3f );

		displayButtons ( );
	}

	IEnumerator wait1 ( ) {
		yield return new WaitForSeconds ( .3f );

		hideBackground2 ( );
	}

	IEnumerator wait ( ) {
		yield return new WaitForSeconds ( .4f );
		
		foreach ( GameObject go in ObjectToFade ) {
			go.SetActive ( false );
		}

		hideBackground1 ( );

		PopupActive = false;
		
		SwipeManager.Resume ( );
	}

	void updateLines ( float value ) {
		foreach ( UIFilledSprite line in Lines ) {
			line.fillAmount = value;
		}
	}
}
