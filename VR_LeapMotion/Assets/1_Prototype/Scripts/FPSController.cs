using UnityEngine;
using System.Collections;

namespace Quinq
{
	public class FPSController : MonoBehaviour 
	{
		#region Properties
		public float speed = 1.0f;
		public float jumpForce = 5.0f;

		private Rigidbody rb;
		private Transform cam;
		#endregion

		#region Unity
		void Start () 
		{
			rb = this.GetComponent<Rigidbody> ();	
			cam = Camera.main.transform;
		}

		void FixedUpdate () 
		{
			Move ();
		}
		#endregion

		#region Private
		private void Move()
		{
			// Horizontal And Vertical movements
			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");

//			Vector3 camDir = new Vector3 (cam.rotation.x * h, 0, cam.rotation.z * v).normalized;
//			print (camDir);
//			Vector3 force = new Vector3 (h * camDir.x, 0, v * camDir.z);
//			force = transform.TransformDirection (force);
			Vector3 forward = Vector3.Normalize(cam.rotation * Vector3.forward) * v;
			Vector3 right = Vector3.Normalize(cam.rotation * Vector3.right) * h;
			transform.position += (forward + right) * Time.deltaTime * speed;

			// Jump 
			if (Input.GetButtonDown ("Jump")) 
				rb.velocity +=  Vector3.up * jumpForce;
		}
		#endregion
	}
}
