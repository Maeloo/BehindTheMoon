using UnityEngine;
using System.Collections;

public class UnlockAnimation : MonoBehaviour {

	[SerializeField]
	float Time;

	Vector3[]	_path;
	GameObject	_target;
	string		_callback;
	Transform	_arg;

	public void play ( Vector3[] path, string callback, GameObject target, Transform arg ) {
		_path = path;

		Hashtable args = iTween.Hash (
			"from", 0,
			"to", 1,
			"time", Time,
			"easetype", iTween.EaseType.linear,
			"onupdate", "updateAnimation",
			"onupdatetarget", gameObject,
			"oncomplete", "onAnimationComplete",
			"oncompletetarget", gameObject
			);

		_target		= target;
		_callback	= callback;
		_arg		= arg;

		iTween.ValueTo ( gameObject, args );
	}

	private void onAnimationComplete ( ) {
		transform.position = new Vector3 ( 10000, 10000, 10000 );

		_target.SendMessage ( ( string ) _callback, ( object ) _arg, SendMessageOptions.DontRequireReceiver );
	}

	void updateAnimation ( float value ) {
		iTween.PutOnPath ( gameObject, _path, value );
	}

}
