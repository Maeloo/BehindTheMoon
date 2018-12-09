using UnityEngine;
using System.Collections;

public class LobbyManager : MonoBehaviour {

	void Start ( ) {
		LoadManager.getInstance ( ).levelLoaded ( );
	}

	void Update ( ) {
		if ( SwipeManager.swipeDirection != Swipe.None ) {
			SoundManager.getInstance ( ).playSoundLaunch ( );

			LoadManager.getInstance ( ).loadLevel ( 3 );
		}
	}

}
