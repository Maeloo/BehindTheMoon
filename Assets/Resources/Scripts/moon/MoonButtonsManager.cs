using UnityEngine;
using System.Collections;

public class MoonButtonsManager : MonoBehaviour {

	// Retour sur l'écran principale
	public void back ( ) {
		SoundManager.getInstance ( ).playSoundLaunch ( );

		LoadManager.getInstance ( ).loadLevel ( 1 );
	}

}
