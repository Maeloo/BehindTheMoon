using UnityEngine;
using System.Collections;

public class ButtonCustom : MonoBehaviour {

	[SerializeField] GameObject Active;
	[SerializeField] GameObject Inactive;

	private bool		_isActive = false;
	private UITweener	_tweenScale;

	public Signal Press = new Signal ( );

	private BoxCollider box;


	void Start ( ) {
		_tweenScale = gameObject.GetComponent<TweenScale> ( );

		if ( Inactive != null && Active != null ) {
			TweenAlpha.Begin ( Active, 0, 0 );
			TweenAlpha.Begin ( Inactive, 0, 0 );
		}

		box = gameObject.GetComponent<BoxCollider> ( );

		box.enabled = false;
	}

	public void Switch ( ) {
		_isActive = true;

		Press.Invoke (  );

		updateButton ( );

		Debug.Log ( "click" );
	}

	public void Show ( ) {
		box.enabled = true;

		if ( _isActive ) {
			TweenAlpha.Begin ( Active, .5f, 1 );
			_tweenScale.Play ( true );
		}
		else {
			TweenAlpha.Begin ( Inactive, .5f, 1 );
			_tweenScale.Play ( true );
		}
	}

	public void Hide ( ) {
		if ( _isActive ) {
			TweenAlpha.Begin ( Active, .5f, 0 );
			_tweenScale.Play ( false );
		}
		else {
			TweenAlpha.Begin ( Inactive, .5f, 0 );
			_tweenScale.Play ( false );
		}

		box.enabled = false;
	}

	public void forceActive ( ) {
		_isActive = true;
	}

	public void forceInactive ( ) {
		_isActive = false;
		
		updateButton ( );
	}

	private void updateButton ( ) {
		if ( _isActive ) {
			TweenAlpha.Begin ( Active, .5f, 1 );
			TweenAlpha.Begin ( Inactive, .5f, 0 );
		}
		else {
			TweenAlpha.Begin ( Inactive, .5f, 1 );
			TweenAlpha.Begin ( Active, .5f, 0 );
		}

		Inactive.GetComponent<FixDepth> ( ).fixDepth ( );
		Active.GetComponent<FixDepth> ( ).fixDepth ( );
	}
}

