using UnityEngine;
using System.Collections;

public class TutoScriptumBehaviour : MonoBehaviour {

	public void startScriptum ( ) {
		foreach ( Transform child in transform ) {
			TweenAlpha.Begin ( child.gameObject, .3f, 0 );
		}

		ScriptumClient.instance.searchActive ( );

		StartCoroutine ( desactive ( ) );
	}

	IEnumerator desactive ( ) {
		yield return new WaitForSeconds ( 1 );

		gameObject.SetActive ( false );
	}
}
