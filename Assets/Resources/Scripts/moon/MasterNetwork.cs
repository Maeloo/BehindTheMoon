using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterNetwork : Photon.MonoBehaviour {

	[SerializeField] GameObject		CrystalPrefab;
	[SerializeField] GameObject[]	AvatarPlayer;
	
	private Transform			InitialPosition;

	private List<GameObject>	crystalPool;


	void Awake ( ) {
		InitialPosition = GameObject.Find ( "InitialPosition" ).transform;

		crystalPool = new List<GameObject> ( );
	}

	//void OnPhotonSerializeView ( PhotonStream stream, PhotonMessageInfo info ) {
	//	if ( stream.isReading ) {
	//		if ( ( bool ) stream.ReceiveNext ( ) ) {
	//			int		id	= ( int ) stream.ReceiveNext ( );
	//			Vector3 vel = ( Vector3 ) stream.ReceiveNext ( );

	//			vel = new Vector3 ( -vel.x, -vel.y, -vel.z );

	//			Debug.Log ( "Add Crystal" );
	//			StartCoroutine ( instantiateCrystal ( vel, id ) );
	//		}
	//	}
	//	else {
	//		stream.SendNext ( "Hello From Master" );

	//		if ( crystalReceived.Count != 0 )
	//			stream.SendNext ( crystalReceived );
	//	}		
	//}

	public void instantiateCrystal ( Vector3 velocity ) {
		//GameObject crystal = ( GameObject ) PhotonNetwork.Instantiate ( "Crystal", InitialPosition.position, Quaternion.identity, 0 );
		GameObject crystal = ( GameObject ) GameObject.Instantiate ( CrystalPrefab );

		if ( velocity == Vector3.zero )
			crystal.rigidbody.AddForce ( Random.Range ( -10, 10 ), 0, 0 );
		else
			crystal.rigidbody.AddForce ( -velocity, ForceMode.VelocityChange );

		crystal.transform.parent = transform;

		crystalPool.Add ( crystal );
	}

	public void addPlayerDust ( int id, Color col ) {
		Debug.Log ( id );
		AvatarPlayer[id].GetComponent<DustBehaviour> ( ).fillTheMoon ( 50, col );
	}
}
