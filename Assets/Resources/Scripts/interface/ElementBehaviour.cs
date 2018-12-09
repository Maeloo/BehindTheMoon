using UnityEngine;
using System.Collections;

public class ElementBehaviour : MonoBehaviour {

	[SerializeField] GameObject Button;

	public void hide ( ) {
		Debug.Log ( "hide" + gameObject.name );
		TweenAlpha.Begin ( gameObject, .5f, 0 );
		gameObject.GetComponent<FixDepth> ( ).fixDepth ( );

		Button.GetComponent<ButtonCustom> ( ).forceInactive ( );
	}

	public void show ( ) {
		TweenAlpha.Begin ( gameObject, .5f, 1 );
		gameObject.GetComponent<FixDepth> ( ).fixDepth ( );

		Button.GetComponent<ButtonCustom> ( ).forceActive ( );
	}

}
