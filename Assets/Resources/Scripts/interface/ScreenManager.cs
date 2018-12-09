using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {

	[SerializeField] GameObject[] Screens;

	private int			currentScreen	= 0;
	private bool		launch			= true;
	private AudioSource slideSound;

	void Awake () {
		LoadManager.getInstance ( ).levelLoaded ( );
		SwipeManager.Pause ( );

		slideSound = gameObject.GetComponent<AudioSource> ( );

		goToScreen ( 0, true );
	}

	void goToScreen ( int id, bool forward )  {
		SwipeManager.Pause ( );

		Screens[currentScreen].GetComponent<ScreenBase> ( ).unloadScreen ( forward );

		if ( launch ) {
			Screens[currentScreen].GetComponent<ScreenBase> ( ).forceTransition ( );
			launch = false;
		}		
		
		currentScreen = id;

		PuceManager.getInstance ( ).changeCurrentScreen ( id );

		StartCoroutine ( loadScreen ( forward ) );
	}

	IEnumerator loadScreen ( bool forward ) {
		yield return new WaitForSeconds ( .8f );

		Screens[currentScreen].GetComponent<ScreenBase> ( ).loadScreen ( forward );
	}

	void Update ( ) {
		if ( SwipeManager.swipeDirection != Swipe.None ) {
			if ( SwipeManager.swipeDirection == Swipe.Up ) {
				if ( currentScreen + 1 < Screens.Length ) {
					Debug.Log ( "Go to" + (currentScreen + 1) );
					goToScreen ( currentScreen + 1, false );
					
					slideSound.Play ( );
				}
			}
			else if ( SwipeManager.swipeDirection == Swipe.Down ) {
				if ( currentScreen - 1 >= 0 ) {
					Debug.Log ( "Go to" + (currentScreen - 1) );
					goToScreen ( currentScreen - 1, true );

					slideSound.Play ( );
				}
			}
		}
	}
	
}
