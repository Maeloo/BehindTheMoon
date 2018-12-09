using UnityEngine;
using System.Collections;

public class ScriptumRPC : MonoBehaviour {

	[SerializeField] GameObject MasterClient;

	private ScriptumMaster _master;

	void Awake ( ) {
		if ( gameObject.GetComponent<GameMaster> ( ).AmIMaster ( ) ) {
			_master = MasterClient.GetComponent<ScriptumMaster> ( );
		}
		else {
			this.enabled = false ;
		}
			
	}

	[RPC]
	void UnlockSymbol ( int ID, int player ) {
		_master.unlockSymbol ( ID, player );
	}

	[RPC]
	void RegisterPlayer ( int idPlayer ) {
		_master.registerPlayer ( idPlayer );
	}
}
