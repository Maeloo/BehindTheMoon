using UnityEngine;
using System.Collections;

public class FixDepth : MonoBehaviour {

	void Awake ( ) { fixDepth ( ); }

	public void fixDepth ( ) {
		Vector3 pos = transform.localPosition;
		pos.z -= .01f;

		TweenPosition.Begin ( gameObject, .5f, pos );
	}

}
