using UnityEngine;
using System.Collections;

public class LoadManager : MonoBehaviour {

	#region Singleton Stuff
	private static LoadManager		_instance		= null;
	private static readonly object	singletonLock	= new object ( );
	#endregion

	private static bool _created = false;

	private GameObject _fadeTexture		= null;
	private GameObject _loadAnimation	= null;
	private GameObject _loadBackground	= null;

	public static LoadManager getInstance ( ) {
		lock ( singletonLock ) {
			if ( _instance == null && !_created ) {
				_instance	= ( LoadManager ) GameObject.Find ( "_LoadManager" ).GetComponent<LoadManager> ( );
				_created	= true;
			}

			return _instance;
		}
	}

	/* Index list :
	 * 0 - SplashScreen
	 * 1 - Home
	 * 2 - Lobby
	 * 3 - Scriptum
	 * 4 - Moon
	 * 5 - LoadMaster
	 * 6 - LobbyMaster
	 * 7 - ScriptumMaster
	 * 8 - MoonMaster
	 */

	void Start ( ) {
		ImmersiveMode.AddCurrentActivity ( );

		if ( _created ) {
			Destroy ( gameObject );
			return;
		}

		DontDestroyOnLoad ( gameObject );

		levelLoaded ( );
	}

	void OnLevelWasLoaded ( int level ) {
		levelLoaded ( );
	}

	public void loadLevel ( int levelIdx ) {
		blurScreen ( 1 );
		playLoadingAnimation ( );

		Application.LoadLevelAsync ( levelIdx );
	}

	public void blurScreen ( float alpha ) {
		if ( checkFadeTexture ( ) ) {
			
			TweenAlpha.Begin ( _fadeTexture, .6f, alpha );
		}
	}

	public void levelLoaded ( ) {
		Debug.Log ( "Level loaded" );
		blurScreen ( 0 );
		playLoadingAnimation ( 0 );
	}

	private void playLoadingAnimation ( float alpha = 1 ) {
		if ( checkLoadAnimation ( ) ) {
			TweenAlpha.Begin ( _loadAnimation, .6f, alpha );
			TweenAlpha.Begin ( _loadBackground, .6f, alpha );
		}
		else { Debug.Log ( "1" ); }
	}

	private bool checkLoadAnimation ( ) {
		_loadAnimation	= _loadAnimation	== null ? ( GameObject ) GameObject.Find ( "load_animation" )	: _loadAnimation;
		_loadBackground = _loadBackground	== null ? ( GameObject ) GameObject.Find ( "load_background" )	: _loadBackground;

		return _loadAnimation != null && _loadBackground != null;
	}

	private bool checkFadeTexture ( ) {
		_fadeTexture = _fadeTexture == null ? ( GameObject ) GameObject.Find ( "fade_texture" ) : _fadeTexture;

		return _fadeTexture != null;
	}

}
