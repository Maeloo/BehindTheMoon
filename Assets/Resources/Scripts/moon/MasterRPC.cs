using UnityEngine;
using System.Collections;

public class MasterRPC : MonoBehaviour {

	private GameObject Master;

	void Start ( ) {
		Master = GameObject.Find ( "CrystalMoonMaster" );
	}

	[RPC]
	void SendCrystal ( Vector3 velocity ) {
		Master.GetComponent<MasterNetwork> ( ).instantiateCrystal ( velocity );
	}

	[RPC]
	void SendDust ( int id, string col ) {
		Color color = Color.white;

		switch ( col ) { 
			case "white":
				color = Color.white;
				break;
		}

		Master.GetComponent<MasterNetwork> ( ).addPlayerDust ( id, color );
	}
}
