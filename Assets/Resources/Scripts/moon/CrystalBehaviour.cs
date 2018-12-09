using UnityEngine;
using System.Collections;

public class CrystalBehaviour : MonoBehaviour {

	private static int next_id = 0;

	[SerializeField] GameObject	Manager;

	private int			ID;

	private Vector3[]	_path;
	private float		_percent;
	private bool		_orbitActive = false;

	public bool			FollowPath;
	public float		Speed;
	
	void Start ( ) {
		next_id++;

		ID			= next_id;		
		_path		= GetComponent<iTweenPath> ( ).nodes.ToArray ( );
		_percent	= Random.value;

		if(Manager.GetComponent<PlayerNetwork> ( ) == null)
			Manager = transform.parent.gameObject;

		Manager.GetComponent<PlayerNetwork> ( ).registerCrystal ( ID, gameObject );

		if ( FollowPath )
			initOrbit ( );
	}
	
	public void transferToMainScreen() {
		Manager.GetComponent<PlayerNetwork> ( ).registerForTransfer ( ID );

		transform.parent.GetComponent<DragObject> ( ).StartCoroutine ( "onDrop", 0 );
	}

	public void desintegrateForMainScreen ( ) { 
		Manager.GetComponent<PlayerNetwork> ( ).registerForDesintegration ( ID );

		transform.parent.GetComponent<DragObject> ( ).StartCoroutine ( "onDrop", 0 );
	}

	public void removeCrystal ( float delay ) {
		StartCoroutine ( "_removeCrystal", delay );
	}

	public void activeOrbit ( ) {
		_orbitActive = true;
	}

	public void desactiveOrbit ( ) {
		_orbitActive = false;
	}

    IEnumerator _removeCrystal ( float delay ) {
		yield return new WaitForSeconds ( delay );
		
		gameObject.SetActive ( false );
	}

	public void initOrbit ( ) {
		//int idx		= ( int ) Mathf.Round ( _percent * ( _path.Length - 1 ) );
		//int idx		= ID - 1;
		int idx;

		Vector3 targetPos	= TweenTools.closestPointOnPath ( _path, transform.position, out idx );

		_percent			= ( float ) idx / ( _path.Length - 1 );

		Hashtable args = iTween.Hash (
			"position", targetPos,
			"time", 2.5f,
			"ease", iTween.EaseType.punch,
			"oncomplete", "activeOrbit",
			"oncompletetarget", gameObject
			);

		iTween.MoveTo ( gameObject, args );
	}

	void LateUpdate ( ) {
		if ( FollowPath && _orbitActive ) {
			_percent = _percent <= 1 ? _percent + Speed : 0;
			_percent = _percent < 0 ? 1 : _percent;

			iTween.PutOnPath ( gameObject, _path, _percent );
		}
	}
}

