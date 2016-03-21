using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform player;
	public float height;

	void FixedUpdate () 
	{
		if(player)
		{
			this.transform.transform.position = player.transform.position + Vector3.up * height;
		}
	}
}
