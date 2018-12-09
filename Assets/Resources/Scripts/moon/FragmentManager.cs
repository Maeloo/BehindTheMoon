using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FragmentManager : MonoBehaviour {

	private Transform	InitialPosition;

	[SerializeField] int			MaxInstances;
	[SerializeField] GameObject		CrystalPrefab;

	private bool _IAmMaster;
	
	List<GameObject>	crystalPool;
	
	void Start() {
		_IAmMaster		= GameObject.Find ( "PhotonNetwork" ).GetComponent<GameMaster> ( ).AmIMaster ( );
		InitialPosition = GameObject.Find ( "InitialPosition" ).transform;		

		if ( !_IAmMaster ) {
			crystalPool = new List<GameObject> ( );

			//StartCoroutine ( "instantiateCrystal", Vector3.zero );
		}
	}

	IEnumerator instantiateCrystal(Vector3 velocity) {
		yield return new WaitForSeconds(0.2f);

        if ( !PhotonConnexion.instance.isConnected ( ) ) {
            StartCoroutine ( "instantiateCrystal", velocity );
            
			yield return new WaitForSeconds ( 1 );
        }
		
		if(crystalPool.Count < MaxInstances) {
			GameObject crystal = ( GameObject ) GameObject.Instantiate ( CrystalPrefab );


			if ( velocity == Vector3.zero )
				crystal.rigidbody.AddForce ( Random.Range ( -10, 10 ), 0, 0 );
			else
				crystal.rigidbody.AddForce ( velocity );
			
            crystal.transform.parent = transform;
			
			crystalPool.Add(crystal);

			StartCoroutine ( "instantiateCrystal", Vector3.zero );
		}
	}
}
