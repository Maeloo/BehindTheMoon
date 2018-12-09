using UnityEngine;
using System.Collections;

public class NumberAnimation : MonoBehaviour {

	public string	Char = "";
	public int		From;
	public int		To;
	public float	Speed;

	private int		_currentValue;
	private int		_maxValue;
	private UILabel _text;

	void Awake ( ) {
		_text = gameObject.GetComponent<UILabel> ( );
	}

	public void runNumber ( ) {
		_currentValue = From;
		_maxValue = To;

		StartCoroutine ( updateText ( ) );
	}

	public void runNumber ( int from, int to ) {
		_currentValue	= from;
		_maxValue		= to;

		StartCoroutine ( updateText ( ) );
	}

	IEnumerator updateText ( ) {
		yield return new WaitForSeconds ( Speed );

		if ( _currentValue < _maxValue ) {
			_currentValue++;

			setText ( _currentValue );

			StartCoroutine ( updateText ( ) );
		}
	}

	void setText ( int value ) {
		if ( value < 10 ) {
			_text.text = "0" + value.ToString ( ) + Char;
		}
		else {
			_text.text = value.ToString ( ) + Char;
		}
	}

}
