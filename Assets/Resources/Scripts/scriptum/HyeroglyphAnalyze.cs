using UnityEngine;
using System.Collections;

// Gère les animations d'analyse des hyéroglyphes
public class HyeroglyphAnalyze : MonoBehaviour {
	
	[SerializeField] int			ID;
	//[SerializeField] GUIText		DebugText;
	[SerializeField] UIFilledSprite Loader;
	[SerializeField] float			Time;
	[SerializeField] GameObject		Target;


	void OnEnable ( ) {
		//DebugText.text = "ENABLE";

		Hashtable args = iTween.Hash ( 
			"from", 0, 
			"to", 1, 
			"time", Time, 
			"onupdate", "updateFill",
			"oncomplete", "symbolDecoded",
			"oncompletetarget", gameObject);

		ScriptumClient.instance.loadActive ( );

		iTween.ValueTo ( gameObject, args );
	}

	private void updateFill ( float value ) {
		Loader.fillAmount = value;
	}

	// Comportement lorsque le héroglyphe a été complètement analysé
	private void symbolDecoded ( ) {
		Debug.Log( "Symbol Decoded" );

		Hashtable args = iTween.Hash (
			"time", .3f,
			"alpha", 0,
			"oncomplete", "disable",
			"oncompletetarget", gameObject );

		iTween.FadeTo ( gameObject, args );

		disable ( );

		Target.SetActive ( false );

		ScriptumClient.instance.searchActive ( );
		ScriptumClient.instance.unlockHyeroglyph ( ID );

		ScriptumManager.instance.activeSymbole ( ID );
	}

	private void disable ( ) {
		Debug.Log ( "Disable" );

		gameObject.SetActive ( false );
	}

	[Signal]
	private void onLostDetection ( ) {
		//DebugText.text = "SIGNAL LOST";

		iTween.Stop ( );
		Loader.fillAmount = 0;		
		gameObject.SetActive ( false );		
	}
}
