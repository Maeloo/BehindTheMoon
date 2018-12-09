using UnityEngine;
using System.Collections;

// CODE OBSOLETE
public class TextTutoBehaviour : MonoBehaviour {

	private TextsTutoAnimation	_animation;
	private float				_maxTime;

	void Awake ( ) {
		_animation	= ( TextsTutoAnimation ) transform.parent.GetComponent<TextsTutoAnimation> ( );
		_maxTime	= _animation.MaxTime;
	}

	public void startAnimation ( ) {
		StartCoroutine ( fadeChild ( 0, 0, gameObject ) );
	}

	IEnumerator fadeChild ( float time, float alpha, GameObject child ) {
		yield return new WaitForSeconds ( time );

		_animation.decrementFlash ( );
		if ( _animation.remainingFlash ( ) < 0 ) {
			if ( _animation.activeTuto ) {
				TweenAlpha.Begin ( gameObject, 0, 1 );
			}
			else if ( _animation.desactiveTuto ) {
				TweenAlpha.Begin ( gameObject, 0, 0 );
			}

			StopAllCoroutines ( );
			yield return null;
		}

		float randTime = Random.RandomRange ( 0, _maxTime );

		TweenAlpha.Begin ( gameObject, randTime, alpha );

		StartCoroutine ( fadeChild ( randTime, alpha == 0 ? 1 : 0, child ) );
	}

}
