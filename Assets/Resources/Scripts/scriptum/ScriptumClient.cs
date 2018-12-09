using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptumClient : MonoBehaviour {

	public enum State { 
		SEARCHING, 
		LOADING
	}

	// singleton instance
	public static ScriptumClient instance;

	private static bool			_created = false;
	private static State		_playerState;
	private static List<int>	_unlocked;
	private static int			_id;

	private PhotonView			PhotonNetwork;


	private void Awake ( ) {
		if ( !_created  ) {
			//DontDestroyOnLoad ( this );
			instance = this;
			
			_created = true;

			PhotonNetwork = GameObject.Find ( "_PhotonNetwork" ).GetComponent<PhotonView> ( );

			_id = gameObject.GetComponent<PhotonView>().viewID;
		}
		else {
			Destroy ( gameObject );
		}
	}

	void Start ( ) {
		_unlocked		= new List<int> ( );
		
		object[] args	= { _id };

		PhotonNetwork.RPC (
			"RegisterPlayer",
			PhotonTargets.MasterClient,
			args );
	}

	public void unlockHyeroglyph ( int ID ) {
		Debug.Log ( "Unlock" );
		SoundManager.getInstance ( ).playSoundUnlock ( );

		_unlocked.Add ( ID );

		object[] args = { ID, _id };

		PhotonNetwork.RPC (
			"UnlockSymbol",
			PhotonTargets.MasterClient,
			args );

		if ( _unlocked.Count == 6 ) {
			Debug.Log ( "Game Over" );
			ScriptumManager.instance.loadEndPanel ( );
		}
	}

	void OnDestroy ( ) {
		_created = false;
	}

	public void searchActive ( ) {
		_playerState = State.SEARCHING;
	}

	public void loadActive ( ) {
		_playerState = State.LOADING;
	}

	public bool isSearching ( ) {
		return _playerState == State.SEARCHING;
	}

	public bool isLoading ( ) {
		return _playerState == State.LOADING;
	}

}
