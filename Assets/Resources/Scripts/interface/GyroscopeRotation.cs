using UnityEngine;
using System.Collections;

public class GyroscopeRotation : MonoBehaviour {

	void Update () {
		transform.rotation = Input.gyro.attitude;
	}

}
