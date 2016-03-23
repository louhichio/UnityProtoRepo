using UnityEngine;
using System.Collections;

namespace Quinq
{
	public class FPSPlayer : Singleton<FPSPlayer>  
	{
		#region Properties
		public float speed = 1.0f;
		public float jumpForce = 5.0f;

		private Rigidbody rb;
		private Transform cam;
		#endregion

		#region Unity
//		void OnDrawGizmosSelected() 
//		{
//			cam = Camera.main.transform;
//			Gizmos.color = Color.blue;
//			Gizmos.DrawLine(transform.position, Vector3.Normalize(cam.rotation * Vector3.forward) * 100);
//			print (Vector3.Normalize (cam.rotation * Vector3.forward));
//		}

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

			Vector3 forward = Vector3.Normalize(cam.rotation * Vector3.forward);
			forward.y = 0;
			forward *= v;

			Vector3 right = Vector3.Normalize(cam.rotation * Vector3.right);
			right.y = 0;
			right *= h;

			transform.position += (forward + right) * Time.deltaTime * speed;

			// Jump 
			if (Input.GetButtonDown ("Jump")) 
				rb.velocity +=  Vector3.up * jumpForce;
		}
		#endregion
	}
}
