using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour {

	[SerializeField] float	spring					= 50.0f;
	[SerializeField] float	damper					= 5.0f;
	[SerializeField] float	drag					= 10.0f;
	[SerializeField] float	angularDrag				= 5.0f;
	[SerializeField] float	distance				= 0.2f;
	[SerializeField] bool	attachToCenterOfMass	= false;
	[SerializeField] Camera Camera;
	
	private SpringJoint springJoint;
	private bool		activeDrag = false;
	private GameObject  target;

	void Start ( ) {
		StartCoroutine ( init ( ) );
	}

	IEnumerator init ( ) {
		yield return new WaitForSeconds ( 2.5f );
		
		activeDrag = true;
	}
	
	void Update ()
	{
		if (activeDrag) {			
			// Make sure the user pressed the mouse down
			if (!Input.GetMouseButtonDown (0))
				return;
			
			Camera mainCamera = FindCamera ();
			
			// We need to actually hit an object
			RaycastHit hit = new RaycastHit ();

			Ray ray = Camera.ScreenPointToRay ( Input.mousePosition );
			Debug.DrawRay ( ray.origin, ray.direction * 40, Color.green );

			if ( !Physics.Raycast ( Camera.ScreenPointToRay ( Input.mousePosition ), out hit, 100 ) ) {
                return;
            }
			
			// We need to hit a rigidbody that is not kinematic
			if ( !hit.rigidbody || hit.rigidbody.isKinematic ) {
                return;
            }	
			
			if (!springJoint) {
				GameObject	go		= new GameObject ("Rigidbody dragger");
				Rigidbody	body	= go.AddComponent ("Rigidbody") as Rigidbody;
				
				springJoint			= go.AddComponent ("SpringJoint") as SpringJoint;
				body.isKinematic	= true;
			}
			
			springJoint.transform.position = hit.point;

			if (attachToCenterOfMass) {
				Vector3 anchor		= transform.TransformDirection (hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
				
				anchor				= springJoint.transform.InverseTransformPoint (anchor);
				springJoint.anchor	= anchor;
			} else {
				springJoint.anchor	= Vector3.zero;
			}
			
			springJoint.spring			= spring;
			springJoint.damper			= damper;
			springJoint.maxDistance		= distance;
			springJoint.connectedBody	= hit.rigidbody;

			target						= hit.collider.gameObject;

			StartCoroutine ( "dragObject", hit.distance );
		}
	}
	
	IEnumerator dragObject (float distance)
	{
		float	oldDrag			= springJoint.connectedBody.drag;
		float	oldAngularDrag	= springJoint.connectedBody.angularDrag;
		//Camera	mainCamera		= FindCamera ();
		Camera	mainCamera		= Camera;
		
		springJoint.connectedBody.drag			= drag;
		springJoint.connectedBody.angularDrag	= angularDrag;

		activeDrag = false;

		target.GetComponent<CrystalBehaviour> ( ).desactiveOrbit ( );
		
		while (Input.GetMouseButton (0)) {
			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			
			springJoint.transform.position = ray.GetPoint (distance);
			
			yield return null;
		}
		
		if (springJoint.connectedBody) {
			springJoint.connectedBody.drag			= oldDrag;
			springJoint.connectedBody.angularDrag	= oldAngularDrag;
			springJoint.connectedBody				= null;
		}

		StartCoroutine ( "onDrop", 1 );		
	}

	IEnumerator onDrop ( float time ) {
		yield return new WaitForSeconds ( time );

		if( time > 0 )
			target.GetComponent<CrystalBehaviour> ( ).initOrbit ( );

		activeDrag = true;
	}
	
	Camera FindCamera ()
	{
		if (camera)
			return camera;
		else
			return Camera.main;
	}
	
	public void setDrag(bool activ) {
		activeDrag = activ;
	}
	
}
