using UnityEngine;
using System.Collections;

public class CustomScreen : ScreenBase {

	[SerializeField] GameObject[]		Tweens;
	[SerializeField] GameObject[]		Sky;
	[SerializeField] GroupElements[]	Groups;
	[SerializeField] GameObject[]		Spoon;
	[SerializeField] ParticleSystem[]	Halo;


	void Start ( ) {
		foreach ( GameObject spoon in Spoon ) {
			TweenAlpha.Begin ( spoon, 0, 0 );
		}

		foreach ( GameObject sky in Sky ) {
			TweenAlpha.Begin ( sky, 0, 0 );
		}

		foreach ( ParticleSystem particule in Halo ) {
			particule.Stop ( );
		}
	}


	public override void loadScreen ( bool forward ) {
		isLoading = true;

		foreach ( GameObject tween in Tweens ) {
			tween.GetComponent<UITweener> ( ).Play ( true );
		}

		foreach ( ParticleSystem particule in Halo ) {
			particule.Play ( );
		}

		foreach ( GroupElements group in Groups ) {
			group.activeElement ( true );
		}

		StartCoroutine ( loadPresumeComplete ( ) );
	}

	public override void unloadScreen ( bool forward ) {
		SwipeManager.Pause ( );

		foreach ( GameObject tween in Tweens ) {
			tween.GetComponent<UITweener> ( ).Play ( false );
		}

		foreach ( GameObject sky in Sky ) {
			TweenAlpha.Begin ( sky, 1, 0 );
		}

		foreach ( ParticleSystem particule in Halo ) {
			particule.Stop ( );
		}

		foreach ( GameObject spoon in Spoon ) {
			TweenAlpha.Begin ( spoon, 1, 0 );
		}
	}

}










