using UnityEngine;
using System.Collections;

public class ScriptumPanelAnimation : MonoBehaviour {

	[SerializeField] UITweener[]	Tweens;
	[SerializeField] GameObject		ButtonGO;
	[SerializeField] GameObject[]	FadeOnHide;

	private bool start = true;


	void Awake ( ) {
		foreach ( GameObject go in FadeOnHide ) {
			TweenAlpha.Begin ( go, 0, 0 );
		}

		StartCoroutine ( initDisplay ( ) );
	}

	IEnumerator initDisplay ( ) {
		yield return new WaitForSeconds ( .75f );
		
		LoadManager.getInstance ( ).blurScreen ( .90f );

		gameObject.GetComponent<FixDepth> ( ).fixDepth ( );

		foreach ( GameObject go in FadeOnHide ) {
			TweenAlpha.Begin ( go, .3f, 1 );
		}

		show ( );
	}

	public void show ( ) {
		foreach ( UITweener tween in Tweens ) {
			tween.Play ( true );
		}

		StartCoroutine ( displayButton ( true ) );
	}

	public void hide ( ) {
		
		foreach ( UITweener tween in Tweens ) {
			if ( tween.delay == 0 )
				tween.delay = .5f;
			else
				tween.delay = 0;

			tween.Play ( false );
		}

		StartCoroutine ( displayButton ( false ) );
	}

	IEnumerator displayButton ( bool forward ) {
		yield return new WaitForSeconds ( .5f );

		if ( !start ) {
			foreach ( GameObject go in FadeOnHide ) {
				TweenAlpha.Begin ( go, .3f, 0 );
			}
		}
		
		start = false;

		ButtonGO.GetComponent<TweenPosition> ( ).Play ( forward );
	}

}
