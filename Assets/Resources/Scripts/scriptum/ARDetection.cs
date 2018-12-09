using UnityEngine;
using System.Collections;

// Gère la détection pour la réalité augmentée
public class ARDetection : MonoBehaviour, ITrackableEventHandler {

	//[SerializeField] GUIText		_textInfo;
	[SerializeField] GameObject		_boxCollision;
	[SerializeField] GameObject		_loader;
	[SerializeField] GameObject		_crystal;

	private bool detected = false;

	//public Signal Detected;
	public Signal Lost =  new Signal ( );
	
	private TrackableBehaviour mTrackableBehaviour;	
	
	void Start () {		
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		
		if(mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	// Changement d'état de la détection
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if ( 
			(newStatus == TrackableBehaviour.Status.DETECTED ||
		     newStatus == TrackableBehaviour.Status.TRACKED  ||
		     newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) &&
			 ScriptumClient.instance.isSearching() ) {
			//_textInfo.text = "DETECTED";

			//playFX ( );

			detected = true;
		}
		else {
			//_textInfo.text = "LOST";

			if ( detected ) {
				detected = false;

				//stopFx ( );

				Lost.Invoke ( );
			}			
		}
	}
	
	void Update() {
		if(Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			Ray ray = Camera.main.ScreenPointToRay(touch.position);
			RaycastHit hit = new RaycastHit();
			
			// Détection d'un touch avec l'objet en RA
			if( Physics.Raycast(ray, out hit, Mathf.Infinity) ) {
				if(hit.collider.gameObject == _boxCollision)	{
					//_textInfo.text = "TOUCH";

					_loader.SetActive ( true );
				}
			}
		}		
	}

	private void playFX ( ) {
		TweenScale.Begin ( _crystal, 1.75f, new Vector3 ( .3f, .3f, .3f ) );
	}

	private void stopFx ( ) {
		_crystal.transform.localScale = new Vector3 ( 0, 0, 0 );
	}
	
}
