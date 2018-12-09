using UnityEngine;
using System.Collections;

public class LiquidDetector : MonoBehaviour {

	void OnTriggerEnter ( Collider Hit ) {
		if ( Hit.rigidbody != null ) {
			transform.parent.GetComponent<Liquid> ( ).Splash ( transform.position.x, Hit.rigidbody.velocity.y * Hit.rigidbody.mass / 40f );
		}
	}

}
