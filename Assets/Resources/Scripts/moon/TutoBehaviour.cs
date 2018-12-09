using UnityEngine;
using System.Collections;

public class TutoBehaviour : MonoBehaviour {

	[SerializeField] GameObject DecoTop;
	[SerializeField] GameObject DecoBot;
	[SerializeField] GameObject Texts;
	[SerializeField] GameObject TutoAnimation;
	[SerializeField] GameObject ButtonGO;


	void Awake ( ) {
		LoadManager.getInstance ( ).levelLoaded ( );

		show ( );
	}

	public void show ( ) {
		TweenPosition tween1 = DecoBot.GetComponent<TweenPosition> ( );
		TweenPosition tween2 = DecoTop.GetComponent<TweenPosition> ( );

		tween1.eventReceiver	= gameObject;
		tween1.callWhenFinished = "activeTuto";

		tween2.eventReceiver = gameObject;
		tween2.callWhenFinished = "activeTexts";
		
		tween1.Play ( true );
		tween2.Play ( true );

		StartCoroutine ( displayButton ( true ) );
	}

	public void hide ( ) {
		desactivateTexts ( );

		TweenPosition tween1 = DecoBot.GetComponent<TweenPosition> ( );
		TweenPosition tween2 = DecoTop.GetComponent<TweenPosition> ( );

		tween1.eventReceiver = null;
		tween1.callWhenFinished = "";
		tween1.delay = .3f;

		tween2.eventReceiver = null;
		tween2.callWhenFinished = "";
		tween2.delay = .3f;

		tween1.Play ( false );
		tween2.Play ( false );

		ButtonGO.GetComponent<TweenPosition> ( ).Play ( false );

		TweenAlpha.Begin ( DecoTop, .7f, 0 );
		TweenAlpha.Begin ( DecoBot, .7f, 0 );

		TutoAnimation.GetComponent<MoonTutoAnimation> ( ).hide ( );

		LoadManager.getInstance ( ).blurScreen ( 0 );
	}

	IEnumerator displayButton ( bool forward ) {
		yield return new WaitForSeconds ( .65f );

		LoadManager.getInstance ( ).blurScreen ( .95f );

		ButtonGO.GetComponent<TweenPosition> ( ).Play ( forward );
	}

	private void activeTuto ( ) { TutoAnimation.SetActive ( true ); }

	private void activeTexts ( ) {
		foreach ( Transform child in Texts.transform ) {
			TweenAlpha.Begin ( child.gameObject, .4f, 1 );
		}
	}

	private void desactivateTexts ( ) {
		foreach ( Transform child in Texts.transform ) {
			TweenAlpha.Begin ( child.gameObject, .4f, 0 );
		}
	}
}
