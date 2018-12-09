using UnityEngine;
using System.Collections;

// Rotate un objet toujours face à la caméra
public class BillBoard : MonoBehaviour
{
	public Camera m_Camera;
	
	void Update()
	{
		transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.down,
		                 m_Camera.transform.rotation * Vector3.up);
	}
}