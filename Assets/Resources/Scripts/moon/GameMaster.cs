using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	[SerializeField]
	bool IsMaster = false;

	public bool AmIMaster ( ) {
		return IsMaster;
	}
}
