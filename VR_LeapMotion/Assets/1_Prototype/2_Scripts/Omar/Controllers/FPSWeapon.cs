using UnityEngine;
using System.Collections;

public class FPSWeapon : Singleton<FPSWeapon> 
{	
	#region Properties

	public GameObject prefabHitFX;

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

		if (Physics.Raycast (ray, out hit, 100)) 
		{
			print (hit.collider.gameObject);
			Vector3 bulletPos = (cam.forward.normalized * -0.1f) + hit.point;
			Instantiate (prefabHitFX, bulletPos, Quaternion.FromToRotation(Vector3.forward, hit.normal));
		}
	}
	#endregion
}
