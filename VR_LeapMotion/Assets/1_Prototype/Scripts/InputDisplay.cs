using UnityEngine;
using System.Collections;

public class InputDisplay : MonoBehaviour 
{
	void OnGUI()
	{
		float h = Screen.height / 2;
		GUI.Label (new Rect (0, h, 100, 100), Input.GetAxis("Horizontal").ToString());
		GUI.Label (new Rect (0, h + 25, 100, 100), Input.GetAxis("Vertical").ToString());
	}	
}
