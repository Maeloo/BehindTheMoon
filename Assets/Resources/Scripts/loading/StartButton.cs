using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	public void StartGame ( ) {
		SoundManager.getInstance ( ).playSoundStart ( );
		LoadManager.getInstance ( ).loadLevel ( 1 );
	}

}
