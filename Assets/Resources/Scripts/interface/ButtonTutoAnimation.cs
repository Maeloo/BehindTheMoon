using UnityEngine;
using System.Collections;

public class ButtonTutoAnimation : MonoBehaviour {

	[SerializeField]
	GameObject ExternalCircle;

	public Vector3	_finalScale;

	private Vector3 _initScale;
	private bool	_tutoActive = false;

	void Awake ( ) {
		_initScale	= ExternalCircle.transform.localScale;
		_tutoActive = true;

		StartCoroutine ( "playAnimation" );
	}

	IEnumerator playAnimation ( ) {
		yield return new WaitForSeconds ( .5f );

		TweenScale.Begin ( ExternalCircle, .6f, _finalScale );
		TweenAlpha.Begin ( ExternalCircle, .6f, 0 );

		StartCoroutine ( "resetAnimation" );
	}

	IEnumerator resetAnimation ( ) {
		yield return new WaitForSeconds ( 1.8f );

		ExternalCircle.transform.localScale = _initScale;

		TweenAlpha.Begin ( ExternalCircle, .1f, 1 );

		if ( _tutoActive )
			StartCoroutine ( "playAnimation" );
	}

	void OnDisable ( ) {
		_tutoActive = false;
	}	
}
