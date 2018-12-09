using UnityEngine;
using System.Collections;

public class CompassRotation : MonoBehaviour {

	[SerializeField] UILabel DebugLabel;
	[SerializeField] float   MinAngleRotation;

	float _prevValue = 0;

	void Awake ( ) {
		Input.compass.enabled = true;
	}

	void LateUpdate ( ) {
		if ( Mathf.Abs ( _prevValue - Input.compass.magneticHeading ) > MinAngleRotation ) {
			//Vector3 rot = Vector3.zero;
			//rot.z		= Input.compass.magneticHeading;
			_prevValue  = Input.compass.magneticHeading;


			transform.rotation = new Quaternion ( 0, 0, -Input.compass.magneticHeading, 0 );
			//iTween.RotateUpdate ( gameObject, rot, 0 );

			for ( int i  = 0; i < transform.childCount; i++ ) {
				Transform child = transform.GetChild ( i );

				child.transform.rotation = new Quaternion ( 0, 0, Input.compass.magneticHeading, 0 );
			}

			DebugLabel.text = Input.compass.magneticHeading.ToString ( );
		}		
	}
}
