using UnityEngine;
using System.Collections;

public class SymboleBehaviour : MonoBehaviour {

	private GameObject		_circle1;
	private GameObject		_circle2;
	private GameObject		_iconeNormal;
	private GameObject		_iconeActive;
	private ParticleSystem	_fx;
	private float			_newFillValue;
	private float			_fill;

	public int amountOfCrystals;

	void Start () {
		_circle1		= transform.FindChild ( "circular_loading_01" ).gameObject;
		_circle2		= transform.FindChild ( "circular_loading_02" ).gameObject;
		_iconeNormal	= transform.FindChild ( "icon_center_moon_normal" ).gameObject;
		_iconeActive	= transform.FindChild ( "icon_center_moon_active" ).gameObject;
		_fx				= transform.FindChild ( "HaloFX" ).gameObject.GetComponent<ParticleSystem> ( );

		_fx.Stop ( );

		_fill			= ( float ) 1 / ( amountOfCrystals + 1 );
		_newFillValue	= _fill;

		updateAmount ( _newFillValue );
	}

	[Signal]
	void onCrystalRemove ( ) {
		_fx.Play ( );

		_iconeNormal.GetComponent<TweenAlpha> ( ).Play ( true );
		_iconeActive.GetComponent<TweenAlpha> ( ).Play ( false );

		iTween.ValueTo ( gameObject, iTween.Hash (
			"from", _newFillValue,
			"to", _newFillValue + _fill,
			"time", 1,
			"onupdate", "updateAmount",
			"oncomplete", "onComplete"
			) );
	}

	void onComplete ( ) {
		_iconeNormal.GetComponent<TweenAlpha> ( ).Play ( false );
		_iconeActive.GetComponent<TweenAlpha> ( ).Play ( true );
	}

	void updateAmount ( float value ) {
		_newFillValue = value;

		_circle1.GetComponent<UIFilledSprite> ( ).fillAmount = value;
		_circle2.GetComponent<UIFilledSprite> ( ).fillAmount = value;
	}

}
