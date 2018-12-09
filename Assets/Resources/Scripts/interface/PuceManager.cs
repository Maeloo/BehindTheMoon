using UnityEngine;
using System.Collections;

public class PuceManager : MonoBehaviour {

	#region Singleton Stuff
	private static PuceManager _instance = null;
	private static readonly object singletonLock = new object ( );
	#endregion

	[SerializeField] GameObject[] PucesBig;
	[SerializeField] GameObject[] PucesSmall;
	[SerializeField] GameObject[] Texts;
	[SerializeField] float minScale;
	[SerializeField] float maxScale;

	public static PuceManager getInstance ( ) {
		lock ( singletonLock ) {
			if ( _instance == null ) {
				_instance = ( PuceManager ) GameObject.Find ( "PuceManager" ).GetComponent<PuceManager> ( );
			}
			return _instance;
		}
	}	

	public void changeCurrentScreen ( int number ) {
		for ( int i = 0; i < PucesBig.Length; i++ ) {
			if ( number == i ) {
				TweenAlpha.Begin ( PucesSmall[i], .15f, 0 );
				TweenAlpha.Begin ( PucesBig[i], .15f, 1 );

				iTween.ScaleTo ( PucesBig[i], iTween.Hash ( "x", maxScale, "time", .3f ) );
				
				if ( Texts[i] != null )
					TweenAlpha.Begin ( Texts[i], .3f, 1 );
			}
			else {
				iTween.ScaleTo ( PucesBig[i], iTween.Hash ( "x", minScale, "time", .3f ) );

				TweenAlpha.Begin ( PucesSmall[i], .15f, 1 );
				TweenAlpha.Begin ( PucesBig[i], .15f, 0 );
				
				if ( Texts[i] != null )
					TweenAlpha.Begin ( Texts[i], .3f, 0 );
			}
		}
	}
}
