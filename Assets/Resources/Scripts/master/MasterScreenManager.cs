using UnityEngine;
using System.Collections;

public class MasterScreenManager : MonoBehaviour {

	void Start ( ) {
		DontDestroyOnLoad ( gameObject );


	}

	void Update ( ) {
		if ( Input.GetKeyDown ( KeyCode.H ) ) {
			Application.LoadLevel ( 0 );

			return;
		}

		if ( Input.GetKeyDown ( KeyCode.L ) ) {
			Application.LoadLevel ( 1 );

			return;
		}

		if ( Input.GetKeyDown ( KeyCode.S ) ) {
			Application.LoadLevel ( 2 );

			return;
		}

		if ( Input.GetKeyDown ( KeyCode.C ) ) {
			Application.LoadLevel ( 3 );

			return;
		}
	}
}
