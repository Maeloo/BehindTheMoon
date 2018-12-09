using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	[SerializeField] AudioSource[] Sounds;
	[SerializeField] AudioSource[] VacuumSounds;

	#region Singleton Stuff
	private static SoundManager		_instance		= null;
	private static readonly object singletonLock	= new object ( );
	#endregion

	private static bool _created = false;

	public static SoundManager getInstance ( ) {
		lock ( singletonLock ) {
			if ( _instance == null && !_created ) {
				_instance = ( SoundManager ) GameObject.Find ( "_SoundManager" ).GetComponent<SoundManager> ( );
				_created = true;
			}

			return _instance;
		}
	}

	private void Start ( ) {
		if ( _created ) {
			Destroy ( gameObject );
			return;
		}

		DontDestroyOnLoad ( gameObject );
	}

	public void playSoundStart ( )	{ Sounds[0].Play ( ); }
	public void playSoundLaunch ( ) { Sounds[1].Play ( ); }
	public void playSoundUnlock( )	{ Sounds[2].Play ( ); }
	public void playSoundCrack ( )	{ Sounds[3].Play ( ); }

	public void playSoundVacuum ( ) {
		int rand = ( int ) Random.value * VacuumSounds.Length;

		VacuumSounds[rand].Play ( ); 
	}

}
