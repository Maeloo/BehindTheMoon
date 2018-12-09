using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Boite à outils pour iTween
public class TweenTools : MonoBehaviour {

	// Permet la création des chemins de points utilisables avec une courbe de Bézier
	static public List<Vector3> generateBezierPoints (Vector3 start, Vector3 end, int amountOfPoints, float noise ) {

		List<Vector3> path = new List<Vector3> ( );

		path.Add ( start );

		for ( int i = 0; i < amountOfPoints; i++ ) {
			float rand = Random.Range( -noise, noise );

			float l  =    Mathf.Sqrt ( Mathf.Pow ( ( end.x - start.x ), 2 ) +
									Mathf.Pow ( ( end.y - start.y ), 2 ) );
			float r  =    l * ( i + 1 ) / (amountOfPoints + 1);
			float t  =    l - r;

			Vector3 point = new Vector3 (
				( r * end.x + t * start.x ) / l,
				( r * end.y + t * start.y ) / l + rand,
				end.z );

			path.Add ( point );
		}

		path.Add ( end );

		return path;
	}

	static public Vector3 closestPointOnPath ( Vector3[] path, Vector3 point, out int idx ) {
		Vector3 closest = path[0];
		float dist		= Vector3.Distance ( closest, point );
		idx = 0;

		for ( int i = 1; i < path.Length; i++ ) {
			if ( dist > Vector3.Distance ( path[i], point ) ) {
				dist	= Vector3.Distance ( path[i], point );
				closest = path[i];
				idx		= i;
			}
		}

		return closest;
	}
}
