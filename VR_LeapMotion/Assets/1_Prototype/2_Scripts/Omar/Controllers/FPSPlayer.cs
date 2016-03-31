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

		private Vector3 initPos;
		private Vector3 initRot;

		public int lifePoints = 3;
		private int maxLifePoints = 3;
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

			initPos = transform.position;
			initRot = transform.eulerAngles;
		}

		void FixedUpdate () 
		{
			Move ();
		}
		#endregion

		#region Public
		public void Reset()
		{
			transform.position = initPos;
			transform.eulerAngles = initRot;

			lifePoints = maxLifePoints;
		}

		public void IsHIt()
		{
			lifePoints--;
			if (lifePoints < 0)
				FPSGameManager.Instance.GameOver ();
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
