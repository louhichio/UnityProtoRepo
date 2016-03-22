using UnityEngine;
using System.Collections;

namespace Quinq
{
	public class FPSInput : Singleton<FPSInput> 
	{
		void OnGUI()
		{
			float h = Screen.height / 2;
			GUI.Label (new Rect (0, h, 100, 100), Input.GetAxis("Horizontal").ToString());
			GUI.Label (new Rect (0, h + 25, 100, 100), Input.GetAxis("Vertical").ToString());
		}	
	}
}
