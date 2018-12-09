using UnityEngine;
using System.Collections;

public enum Swipe		{ None, Up, Down, Left, Right };
public enum SwipeMode	{ ByMouse, ByTouch }

public class SwipeManager : MonoBehaviour{

	public SwipeMode Mode = SwipeMode.ByMouse; 
	// default to mouse, can be changed in the editor.

	public float	minSwipeLength = 200f;
	Vector2			firstPressPos;
	Vector2			secondPressPos;
	Vector2			currentSwipe;

	private static bool pause = false;

	public static Swipe swipeDirection;

	public GameObject touch;

	void Start(){
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID )
		Mode = SwipeMode.ByTouch;
#else	
		Mode = SwipeMode.ByMouse;
#endif
	}

	void Update (){
		if ( Mode == SwipeMode.ByMouse ){
			DetectSwipeByMouse();
		}
		else if ( Mode == SwipeMode.ByTouch ){
			DetectSwipe();
		}
	}

	public static void Pause ( ) {
		Debug.Log ( "pause" );
		pause = true;
	}

	public static void Resume ( ) {
		Debug.Log ( "resume" );
		pause = false;
	}

	void displayTouch ( ) {
		Debug.Log ( "disaply" );
		TweenAlpha.Begin ( touch, .2f, .65f );
		TweenScale.Begin ( touch, .2f, new Vector3 ( .09f, .09f, 1 ) );
	}

	void hideTouch ( ) {
		Debug.Log ( "hide" );
		TweenAlpha.Begin ( touch, .2f, 0f );
		TweenScale.Begin ( touch, .2f, new Vector3 ( .14f, .14f, 1 ) );
	}

	public void DetectSwipeByMouse() {

		if ( Input.GetMouseButtonDown ( 0 ) ) {
			//save began touch 2d point
			firstPressPos = new Vector2 ( Input.mousePosition.x, Input.mousePosition.y );

			displayTouch ( );
		}

		Vector3 pos = Camera.mainCamera.ScreenToWorldPoint ( new Vector3 ( Input.mousePosition.x, Input.mousePosition.y, -1 ) );
		touch.transform.localPosition = pos;
		
		if(Input.GetMouseButtonUp(0) && !pause){
			hideTouch ( );

			//save ended touch 2d point
			secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);

			//create vector from the two points
			currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y); 

			//normalize the 2d vector
			currentSwipe.Normalize();

			//swipe upwards
			if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f){
				swipeDirection = Swipe.Up;
			}
			
			//swipe down
			else if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f){
				swipeDirection = Swipe.Down;
			}
			
			//swipe left
			else if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f){
				swipeDirection = Swipe.Left;
			}
			
			//swipe right
			else if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f){
				swipeDirection = Swipe.Right;
			}

			else{
				swipeDirection = Swipe.None;
			}
		}
		else {
			swipeDirection = Swipe.None;    
		}
	}
	
	public void DetectSwipe (){
		if ( Input.touches.Length > 0 && !pause ) {
			Touch t = Input.GetTouch(0);
			if (t.phase == TouchPhase.Began) {
				firstPressPos = new Vector2(t.position.x, t.position.y);

				displayTouch ( );
			}

			Vector3 pos = Camera.mainCamera.ScreenToWorldPoint ( new Vector3 ( t.position.x, t.position.y, -1 ) );
			touch.transform.localPosition = pos;
			
			if (t.phase == TouchPhase.Ended) {
				hideTouch ( );

				secondPressPos = new Vector2(t.position.x, t.position.y);
				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

				// Make sure it was a legit swipe, not a tap
				if (currentSwipe.magnitude < minSwipeLength) {
					swipeDirection = Swipe.None;
					return;
				}

				currentSwipe.Normalize();

				// Swipe up
				if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
					swipeDirection = Swipe.Up;
					
				// Swipe down
				} 
				else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
					swipeDirection = Swipe.Down;
					
				// Swipe left
				} 
				else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
					swipeDirection = Swipe.Left;
					
				// Swipe right
				} 
				else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
					swipeDirection = Swipe.Right;
					
				}
			}	
		} 
		else {
			swipeDirection = Swipe.None;    
		}
	}
}