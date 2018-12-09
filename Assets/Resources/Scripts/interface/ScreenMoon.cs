using UnityEngine;
using System.Collections;

public class ScreenMoon : ScreenBase {

	[SerializeField] UITweener[]	Tweens;
	[SerializeField] GameObject		Button;

	void Awake ( ) {
		foreach ( UITweener tween in Tweens ) {
			tween.Reset ( );
		}
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

		isLoading = true;

		foreach ( UITweener tween in Tweens ) {
			tween.Play ( true );
		}

		Button.GetComponent<MissionButtonBehaviour> ( ).show ( );

		StartCoroutine ( loadPresumeComplete ( ) );
	}

	public override void unloadScreen ( bool forward ) {
		SwipeManager.Pause ( );

		foreach ( UITweener tween in Tweens ) {
			tween.Play ( false );
		}

		Button.GetComponent<MissionButtonBehaviour> ( ).hide ( );
	}
	
}
