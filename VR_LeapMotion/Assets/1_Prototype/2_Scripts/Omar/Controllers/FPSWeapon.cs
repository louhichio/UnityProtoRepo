using UnityEngine;
using System.Collections;

namespace Quinq
{
	public class FPSWeapon : Singleton<FPSWeapon> 
	{	
		#region Properties

		public GameObject prefabHitFX;
		public GameObject prefabHitBloodFX;

		[SerializeField]
		private ParticleSystem ps;

		private Transform cam;
		#endregion

		#region Unity
		void Start()
		{
			cam = Camera.main.transform;
		}
		#endregion

		#region Public
		public void Fire()
		{
			ps.Play ();
			CheckHit ();
		}
		#endregion

		#region Private
		private void CheckHit()
		{
			RaycastHit hit;
			Ray ray = new Ray (cam.position, cam.forward);

			//ignore Layer Player 8
			int layer = ~((1 << 8)|(1 << 9));

			if (Physics.Raycast (ray, out hit, 100, layer)) 
			{
	//			print (hit.collider);
				Vector3 bulletPos = (cam.forward.normalized * -0.001f) + hit.point;

				if (hit.collider.tag == "Enemy") 
				{
					Instantiate (prefabHitBloodFX, bulletPos, Quaternion.FromToRotation (Vector3.forward, hit.normal));
					hit.collider.GetComponentInParent<StatePatternEnemy> ().isHit = true;
				}
				else
					Instantiate (prefabHitFX, bulletPos, Quaternion.FromToRotation(Vector3.forward, hit.normal));
			}
		}
		#endregion
	}
}
