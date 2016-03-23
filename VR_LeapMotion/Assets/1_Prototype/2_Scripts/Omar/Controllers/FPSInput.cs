using UnityEngine;
using System.Collections;

namespace Quinq
{
	public class FPSInput : Singleton<FPSInput> 
	{
		#region Unity
//		void OnGUI()
//		{
//			float h = Screen.height / 2;
//			GUI.Label (new Rect (0, h, 100, 100), Input.anyKey.ToString());
//			GUI.Label (new Rect (0, h + 25, 100, 100), Input.GetButton ("Fire1").ToString());
//		}

		void Update()
		{
			if (Input.GetButtonDown ("Fire1")) 
			{
				FPSAudio.Instance.PlayGunAudio ();
				FPSWeapon.Instance.Fire ();
			}
		}
		#endregion
	}
}
