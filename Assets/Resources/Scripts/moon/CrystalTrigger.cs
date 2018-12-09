using UnityEngine;
using System.Collections;

public class CrystalTrigger : MonoBehaviour {

	public bool		Transfer;
	public bool		Desintegration;
	public Signal	CrystalTriggered;

	void OnTriggerEnter(Collider collider) {
		CrystalBehaviour cb = collider.gameObject.GetComponent<CrystalBehaviour> ( );

		if ( cb != null ) {
			CrystalTriggered.Invoke ( );

			if ( Transfer ) {
				SoundManager.getInstance ( ).playSoundVacuum ( );

				cb.transferToMainScreen ( );			
			}
			else if ( Desintegration ) {
				SoundManager.getInstance ( ).playSoundCrack ( );

				cb.desintegrateForMainScreen ( );
			}
		}
	}
}
