using UnityEngine;
using System.Collections;

namespace Quinq
{
	public class FPSCamera : Singleton<FPSCamera> 
	{
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
}
