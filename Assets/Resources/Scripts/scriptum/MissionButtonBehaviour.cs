using UnityEngine;
using System.Collections;

public class MissionButtonBehaviour : MonoBehaviour {

	[SerializeField] ParticleSystem fx;

	void Start ( ) {
		fx.Stop ( );
	}

	void Awake ( ) {
		foreach ( Transform child in transform ) {
			TweenAlpha.Begin ( child.gameObject, 0, 0 );
		}
	}

	public void startScriptum ( ) {
		fx.Play ( );

		SoundManager.getInstance ( ).playSoundLaunch ( );

		LoadManager.getInstance ( ).loadLevel ( 2 );
	}

	public void startMoon ( ) {
		fx.Play ( );
		
		SoundManager.getInstance ( ).playSoundLaunch ( );

		LoadManager.getInstance ( ).loadLevel ( 4 );
	}

	public void show ( ) {
		if( !gameObject.activeSelf )
			gameObject.SetActive ( true );
		
		gameObject.GetComponent<BoxCollider> ( ).active = true;

		foreach ( Transform child in transform ) {
			TweenAlpha.Begin ( child.gameObject, .4f, 1 );
			TweenPosition.Begin ( child.gameObject, .4f, child.localPosition );
		}
	}

	public void hide ( ) {
		gameObject.GetComponent<BoxCollider> ( ).active = false;

		foreach ( Transform child in transform ) {
			TweenAlpha.Begin ( child.gameObject, .4f, 1 );
			TweenPosition.Begin ( child.gameObject, .4f, child.position );
		}
	}
}
