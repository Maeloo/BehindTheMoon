using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;


public class ScriptumMaster : MonoBehaviour {

	[SerializeField] Transform		Root;
	[SerializeField] GameObject[]	Players;
	[SerializeField] int			MaxSymbols;
	[SerializeField] GameObject		Lock;
	[SerializeField] GameObject		DecoInter;
	[SerializeField] GameObject		FX;
	[SerializeField] GameObject		Circle;

	private int			_unlockedSymbols;
	private Transform[] _hieroglyphs;

	private Dictionary<int, int> playersIndex;

	private List<Vector3>	currentPath;
	private GameObject		currentFX;


	void Awake ( ) {
		_hieroglyphs = new Transform[MaxSymbols];

		Regex regex = new Regex ( "hyero(?<Numeric>[0-9]*)" );

		foreach ( Transform child in Root ) {
			Match match = regex.Match ( child.name );
			if ( match.Success ) {
				string	number	= match.Groups["Numeric"].Value;
				int		index	= int.Parse ( number );

				_hieroglyphs[index-1] = child;
			}
		}

		playersIndex = new Dictionary<int, int> ( );
	}

	public void registerPlayer ( int idPlayer ) {
		Debug.Log ( "Register Player " + idPlayer );
		int nextIndex = playersIndex.Count;

		if ( playersIndex.ContainsKey ( idPlayer ) ) {
			Debug.Log ( "Player " + idPlayer + " already registered." );
		}
		else {
			playersIndex.Add ( idPlayer, nextIndex );
		}		

		Players[nextIndex].SetActive ( true );		
	}

	public void unlockSymbol ( int ID, int player ) {
		Debug.Log ( "Unlock " + ID + "Player " + player );

		GameObject playerObject = Players[playersIndex[player]];
		GameObject unlockFX		= null;

		Vector3 startPoint		= playerObject.transform.position;
		Vector3 endPoint		= _hieroglyphs[ID].position;

		foreach ( Transform child in playerObject.transform ) {
			if ( child.name == "unlockFX" ) {
				unlockFX = ( GameObject ) Instantiate ( child.gameObject );
			}
		}

		List<Vector3> path = TweenTools.generateBezierPoints ( startPoint, endPoint, 5, .2f );

		unlockFX.SetActive ( true );
		unlockFX.GetComponent<UnlockAnimation> ( ).play ( path.ToArray ( ), "onAnimationComplete", gameObject, _hieroglyphs[ID] );

		_unlockedSymbols++;

		if ( _unlockedSymbols == MaxSymbols ) {
			TweenAlpha.Begin ( Lock, 1, 0 );
			TweenAlpha.Begin ( DecoInter, 1, 0 );
			TweenAlpha.Begin ( Circle, 1, .8f );
			
			FX.SetActive ( true );
		}
	}

	private void onAnimationComplete ( Transform hyeroglyph ) {
		foreach ( Transform child in hyeroglyph ) {
			if ( child.name == "Normal" ) {
				child.gameObject.SetActive ( false );
			}
			else if ( child.name == "Active" ) {
				child.gameObject.SetActive ( true );
			}
		}
	}


}
