using UnityEngine;
using System.Collections;

public class CubeNetwork : Photon.MonoBehaviour {

	private Vector3		correctCubePos = Vector3.zero; // We lerp towards this
	private Quaternion	correctCubeRot = Quaternion.identity; // We lerp towards this
	
	void Start() {
		Debug.Log ( "Start" );
		correctCubePos = transform.position;
		correctCubeRot = transform.rotation;
	}
	
	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, this.correctCubePos, Time.deltaTime * 5);
		transform.rotation = Quaternion.Lerp(transform.rotation, this.correctCubeRot, Time.deltaTime * 5);
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			Debug.Log ( "Writing" );
			// We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			Debug.Log ( "Reading" );
			// Network player, receive data
			this.correctCubePos = (Vector3)stream.ReceiveNext();
			this.correctCubeRot = (Quaternion)stream.ReceiveNext();
		}
	}
}
