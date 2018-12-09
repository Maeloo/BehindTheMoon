using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dust : MonoBehaviour {

	List<Vector3> _path;

	public void UpdatePath ( float val ) {
		try {
			if ( _path != null )
				iTween.PutOnPath ( gameObject, _path.ToArray ( ), val );
		}
		catch ( System.Exception e ) {
			Debug.Log ( "Exception : " + e.Message );
		}
		
	}

	public void addPath ( Vector3 StartPoint, Vector3 EndPoint ) {
		_path = TweenTools.generateBezierPoints ( StartPoint, EndPoint, 5, .1f );
	}
}
