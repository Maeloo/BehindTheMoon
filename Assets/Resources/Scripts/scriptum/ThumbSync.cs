using UnityEngine;
using System.Collections;

public class ThumbSync : MonoBehaviour {

	[SerializeField] GameObject LoadingSprite;

	private UIFilledSprite FSComponent;

	private Vector3 _origin;
	private float	_radius;
	private float	_angle;

	void Awake ( ) {
		_origin		= LoadingSprite.transform.localPosition;
		_radius		= LoadingSprite.transform.localScale.x / 2;

		FSComponent = LoadingSprite.GetComponent<UIFilledSprite> ( );
	}

	void Update ( ) {
		_updateThumbPosition ( );
	}

	private void _updateThumbPosition ( ) {
		_angle = FSComponent.fillAmount * 360;	// Degree
		_angle = Mathf.PI * _angle / 180;		// Radians
		_angle += Mathf.PI / 2;

		Vector3 coord = new Vector3 (
			_origin.x - _radius * Mathf.Cos ( _angle ),
			_origin.y + _radius * Mathf.Sin ( _angle ),
			transform.localPosition.z
			);

		transform.localPosition = coord;
	}
}
