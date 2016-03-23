using UnityEngine;
using System.Collections;

public class FPSBullet: MonoBehaviour {

	public float despawnTime;

	void Start () 
	{
		StartCoroutine (DespawnTime ());
	}

	IEnumerator DespawnTime () 
	{
		yield return new WaitForSeconds(despawnTime);

		Destroy (gameObject);
	}
}