using UnityEngine;
using System.Collections;

public class MoonTutoAnimation : MonoBehaviour {

	[SerializeField] GameObject		Crystal;
	[SerializeField] GameObject		DropZone;
	[SerializeField] ParticleSystem FX;

	private bool	_tutoActive = false;
	private Vector3 _initPos;
	private Vector3 _finalPos;

	void Awake ( ) {
		_tutoActive = true;
		_initPos	= Crystal.transform.position;
		_finalPos	= DropZone.transform.localPosition;

		FX.Stop ( );

		StartCoroutine ( "playAnimation" );
	}

	IEnumerator playAnimation ( ) {
		yield return new WaitForSeconds ( 2.5f );

		Crystal.transform.position = _initPos;

		TweenAlpha.Begin ( Crystal, .1f, 1 );
		TweenPosition.Begin ( Crystal, .75f, _finalPos );

		StartCoroutine ( "resetAnimation" );
	}

	IEnumerator resetAnimation ( ) {
		yield return new WaitForSeconds ( .75f );

		TweenAlpha.Begin ( Crystal, .3f, 0 );
		FX.Play ( );

		if ( _tutoActive )
			StartCoroutine ( "playAnimation" );
	}

	public void hide ( ) {
		_tutoActive = false;

		StopCoroutine ( "resetAnimation" );
		StopCoroutine ( "playAnimation" );

		foreach ( Transform child in transform ) {
			if ( child.gameObject != FX.gameObject )
				TweenAlpha.Begin ( child.gameObject, .4f, 0 );
			else
				FX.gameObject.SetActive ( true );
		}
	}

	void OnDisable ( ) {
		_tutoActive = false;
	}
}
