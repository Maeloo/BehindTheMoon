using UnityEngine;
using System.Collections;

public class SymboleAnimation : MonoBehaviour {

	[SerializeField] GameObject Symbole;

	private Vector3[] _path;

	void Start ( ) {
		_path = gameObject.GetComponent<iTweenPath> ( ).nodes.ToArray ( );
	}

	public void initAnimation ( float delay ) {
		StartCoroutine ( startAnimation ( delay ) );
	}

	IEnumerator startAnimation ( float delay  ) {
		yield return new WaitForSeconds ( delay );

		iTween.ValueTo ( gameObject, iTween.Hash ( 
			"from", 0,
			"to", 1,
			"time", 1,
			"ease", iTween.EaseType.easeOutExpo,
			"onupdate", "updatePosition"
			) );
	}

	void updatePosition ( float value ) {
		iTween.PutOnPath ( gameObject, _path, value );
	}

	public void ForceActivation ( ) {
		Symbole.SetActive ( true );
	}
}
