using UnityEngine;
using System.Collections;

public class EndScreen : ScreenBase {

	[SerializeField] GameObject Button;

	void Start ( ) {
		TweenAlpha.Begin ( Button, 0, 0 );

		Button.SetActive ( false );
	}

	public override void loadScreen ( bool forward ) {
		Button.SetActive ( true );

		TweenAlpha.Begin ( Button, .5f, 1 );
		Button.GetComponent<FixDepth> ( ).fixDepth ( );

		StartCoroutine ( loadPresumeComplete ( ) );
	}

	public override void unloadScreen ( bool forward ) {
		SwipeManager.Pause ( );

		TweenAlpha.Begin ( Button, .5f, 0 );

		StartCoroutine ( disable ( ) );
	}

	IEnumerator disable ( ) {
		yield return new WaitForSeconds ( .5f );
		Button.SetActive ( false );
	}

	public void onClick ( ) {
		SoundManager.getInstance ( ).playSoundLaunch ( );

		LoadManager.getInstance ( ).loadLevel ( 0 );
	}

}
