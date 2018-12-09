using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DustBehaviour : MonoBehaviour {

	[SerializeField] GameObject		DustPrefab;
	[SerializeField] GameObject		End;
	[SerializeField] float			InactivityTime;
	[SerializeField] ParticleSystem	Fx;

	private Vector3			EndPoint;
	private Vector3			StartPoint;
	private GameObject		Avatar;
	private float			LastActivity = 0;

	void Start ( ) {
		EndPoint	= End.transform.position;
		StartPoint	= transform.position;

		Avatar		= transform.FindChild ( "user_avatar" ).gameObject;
		//Fx			= transform.FindChild ( "HaloFx" ).GetComponent<ParticleSystem> ( );
		
		Fx.Stop ( );
	}

	public void fillTheMoon ( int amount, Color col ) {
		Fx.Play ( );

		Avatar.GetComponent<TweenScale> ( ).Reset ( );
		Avatar.GetComponent<TweenScale> ( ).Play ( true );

		StartCoroutine ( nextDust ( amount, col ) );
	}

	void Update ( ) {
		if ( Time.time - LastActivity > InactivityTime ) {
			Avatar.GetComponent<TweenScale> ( ).Play ( false );
		}
	}

	IEnumerator nextDust ( int amount, Color col ) {
		if ( amount > 0 ) {
			yield return StartCoroutine ( nextDust ( amount - 1, col ) );
		}

		GameObject	dust	= ( GameObject ) GameObject.Instantiate ( DustPrefab );
		float		rand	= Random.value;
		float		rand2	= Random.Range ( .5f, 2 );
		Vector3		scale	= dust.transform.lossyScale;

		scale.x *= rand2;
		scale.y *= rand2;

		dust.transform.Rotate ( new Vector3 ( 0, 0, 360* rand ) );
		dust.transform.parent					= transform;
		dust.GetComponent<UISprite> ( ).MakePixelPerfect ( );
		dust.transform.localScale				= scale;
		//dust.GetComponent<UISprite> ( ).color	= col;

		Hashtable args = new Hashtable ( );
		args.Add ( "from", 0 );
		args.Add ( "to", 1 );
		args.Add ( "time", .6f );
		args.Add ( "easetype", iTween.EaseType.linear );
		args.Add ( "onupdate", "UpdatePath" );

		dust.GetComponent<Dust> ( ).addPath ( StartPoint, EndPoint );

		iTween.ValueTo ( dust, args);

		LastActivity = Time.time;

		TweenAlpha.Begin ( dust, .4f, 0 );

		yield return new WaitForSeconds ( .025f );
	}
}
