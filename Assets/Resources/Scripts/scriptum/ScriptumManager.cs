using UnityEngine;
using System.Collections;

// Gère le comportement du HUD Utilisateur
public class ScriptumManager : MonoBehaviour {

	[SerializeField] Transform[] Symboles;
	[SerializeField] GameObject  EndPanel;
	[SerializeField] GameObject  GamePanel;
	[SerializeField] GameObject  TutoPanel;

	// singleton instance
	public static ScriptumManager instance;

	private static bool	_created = false;

	private void Awake ( ) {
		if ( !_created ) {
			//DontDestroyOnLoad ( this );
			instance = this;
			_created = true;
		}
		else {
			Destroy ( gameObject );
		}

		LoadManager.getInstance ( ).levelLoaded ( );
	}

	public void initAnimation ( ) {
		LoadManager.getInstance ( ).blurScreen ( 0 );

		TutoPanel.GetComponent<ScriptumPanelAnimation> ( ).hide ( );

		GamePanel.SetActive ( true );

		float delay = 0;

		foreach ( Transform symbole in Symboles ) {
			symbole.GetComponent<SymboleAnimation> ( ).initAnimation ( delay );
			
			delay += 0.1f;
		}
	}

	public void loadEndPanel ( ) {
		LoadManager.getInstance ( ).blurScreen ( .95f );

		EndPanel.SetActive ( true );
	}

	public void onContinue ( ) {
		LoadManager.getInstance ( ).loadLevel ( 1 );
	}

	void OnDestroy ( ) {
		_created = false;
	}

	// Activation d'un symbole
	public void activeSymbole ( int ID ) {
		foreach ( Transform child in Symboles[ID] ) {
		  if ( child.name == "Normal" ) {
			  child.gameObject.SetActive ( false );
		  }
		  else if ( child.name == "Active" ) {
			  child.gameObject.SetActive ( true );
		  }
	  }
	}
}
