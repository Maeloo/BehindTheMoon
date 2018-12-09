using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerNetwork : MonoBehaviour {

	[SerializeField] GameObject[]	Triggers;
	[SerializeField] GameObject		Explosion;
	
	private bool	_sendToMainScreen	= false;
	private int		_nextCrystal;
	private int		_id;

	private Dictionary<int, GameObject> PlayerCrystals;

	void Awake ( ) {
		PlayerCrystals = new Dictionary<int, GameObject> ( );

		_id = GameObject.Find ( "_PhotonNetwork" ).GetComponent<PhotonConnexion> ( ).getMyID ( );

		StartCoroutine ( initTriggers( ) );
	}

	IEnumerator initTriggers ( ) {
		yield return new WaitForSeconds ( 2 );

		foreach ( GameObject trigger in Triggers ) {
			trigger.SetActive ( true );
		}
	}

	/*void OnPhotonSerializeView ( PhotonStream stream, PhotonMessageInfo info ) {
		if ( stream.isWriting ) {
			if ( _sendToMainScreen ) {
				Debug.Log ( "Sending Crystal" );

				_sendToMainScreen = false;

				stream.SendNext ( true );
				stream.SendNext ( _nextCrystal );
				stream.SendNext ( PlayerCrystals[_nextCrystal] );
			}
			else {
				Debug.Log ( "Crystal Send : Remove" );

				List<int> crystalToRemove = ( List<int> ) stream.ReceiveNext ( );

				if ( crystalToRemove != null ) {
					foreach ( int id in crystalToRemove ) {
						PlayerCrystals[id].removeCrystal ( 0.1f );
					}
				}
			}
		}
	}*/

	public void registerCrystal (int id,  GameObject crystal ) {
		PlayerCrystals.Add ( id, crystal );	
	}

	public void registerForTransfer ( int id ) {
		Debug.Log ( "Register for Transfer" );

		GameObject.Find ( "_PhotonNetwork" ).GetComponent<PhotonView> ( ).RPC ( 
			"SendCrystal", 
			PhotonTargets.MasterClient, 
			new object[] { PlayerCrystals[id].rigidbody.velocity } );

		PlayerCrystals[id].GetComponent<CrystalBehaviour> ( ).removeCrystal ( 0.2f );	
	}

	public void registerForDesintegration ( int id ) {
		Debug.Log ( "Register for Desintegration" );

		GameObject.Find ( "_PhotonNetwork" ).GetComponent<PhotonView> ( ).RPC (
			"SendDust",
			PhotonTargets.MasterClient,
			new object[] { _id, "white" } );

		PlayerCrystals[id].GetComponent<CrystalBehaviour> ( ).removeCrystal ( 0.2f );

		Explosion.SetActive ( true );
		Explosion.GetComponent<UISpriteAnimation> ( ).Reset ( );
	}
}
