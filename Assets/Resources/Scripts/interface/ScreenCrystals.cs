using UnityEngine;
using System.Collections;

public class ScreenCrystals : ScreenBase {

	[SerializeField] UITweener[]	Tweens;
	[SerializeField] GameObject		Info1;
	[SerializeField] GameObject		Info2;
	[SerializeField] TextMoonFade[]	TextsToFade;
	[SerializeField] GameObject		Button;
	[SerializeField] GameObject[]	NumberToAnimate;

	private Vector3[] path1;
	private Vector3[] path2;

	private float percent1;
	private float nextStep1;
	private float percent2;
	private float nextStep2;

	void Awake ( ) {
		foreach ( UITweener tween in Tweens ) {
			tween.Reset ( );
		}

		path1 = Info1.GetComponent<iTweenPath> ( ).nodes.ToArray ( );
		path2 = Info2.GetComponent<iTweenPath> ( ).nodes.ToArray ( );

		nextStep1 = percent1 = 0;
		nextStep2 = percent2 = 1;

		isLoading = false;
	}

	IEnumerator WaitEndLoading ( bool forward ) {
		yield return new WaitForSeconds ( .1f );
		
		loadScreen ( forward );
	}

	public override void loadScreen ( bool forward ) {
		if ( isLoading ) {
			StartCoroutine ( WaitEndLoading ( forward ) );
			return;
		}

		SwipeManager.Pause ( );

		isLoading = true;

		foreach ( UITweener tween in Tweens ) {
			try {
				tween.gameObject.GetComponent<FixDepth> ( ).fixDepth ( );
			}
			catch ( System.Exception error ) {
				Debug.Log ( "No fix depth available" );
			}

			tween.Play ( true );
		}		

		foreach ( TextMoonFade text in TextsToFade ) {
			text.fadeIn ( );
		}

		foreach ( GameObject number in NumberToAnimate ) {
			number.GetComponent<NumberAnimation> ( ).runNumber ( );
		}

		nextStep1 = forward ? nextStep1 - .5f : nextStep1 + .5f;
		nextStep1 = nextStep1 < 0 ? 0 : nextStep1;
		nextStep1 = nextStep1 > 1 ? 1 : nextStep1;

		Hashtable args1 = iTween.Hash (
			"from", percent1,
			"to", nextStep1,
			"time", .3f,
			"onupdate", "updateInfosPosition1"
			);

		iTween.ValueTo ( gameObject, args1 );

		nextStep2 = forward ? nextStep2 + .5f : nextStep2 - .5f;
		nextStep2 = nextStep2 < 0 ? 0 : nextStep2;
		nextStep2 = nextStep2 > 1 ? 1 : nextStep2;

		Hashtable args2 = iTween.Hash (
			"from", percent2,
			"to", nextStep2,
			"time", .3f,
			"onupdate", "updateInfosPosition2"
			);

		iTween.ValueTo ( gameObject, args2 );

		Button.GetComponent<MissionButtonBehaviour> ( ).show ( );

		StartCoroutine ( loadPresumeComplete ( ) );
	}

	public override void unloadScreen ( bool forward ) {
		SwipeManager.Pause ( );

		foreach ( UITweener tween in Tweens ) {
			tween.Play ( false );
		}

		foreach ( TextMoonFade text in TextsToFade ) {
			text.fadeOut ( );
		}

		nextStep1 = forward ? nextStep1 - .5f : nextStep1 + .5f;
		nextStep1 = nextStep1 < 0 ? 0 : nextStep1;
		nextStep1 = nextStep1 > 1 ? 1 : nextStep1;

		Hashtable args1 = iTween.Hash (
			"from", percent1,
			"to", nextStep1,
			"time", .3f,
			"onupdate", "updateInfosPosition1"
			);

		iTween.ValueTo ( gameObject, args1 );

		nextStep2 = forward ? nextStep2 + .5f : nextStep2 - .5f;
		nextStep2 = nextStep2 < 0 ? 0 : nextStep2;
		nextStep2 = nextStep2 > 1 ? 1 : nextStep2;

		Hashtable args2 = iTween.Hash (
			"from", percent2,
			"to", nextStep2,
			"time", .3f,
			"onupdate", "updateInfosPosition2"
			);

		iTween.ValueTo ( gameObject, args2 );

		Button.GetComponent<MissionButtonBehaviour> ( ).hide ( );
	}

	private void updateInfosPosition1 ( float value ) { percent1 = value;  iTween.PutOnPath ( Info1, path1, value ); }
	private void updateInfosPosition2 ( float value ) { percent2 = value;  iTween.PutOnPath ( Info2, path2, value ); }
	
}
