using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fill : MonoBehaviour {
	private Image im_Fill;

	// Use this for initialization
	void Start () 
	{
		im_Fill = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
//		im_Fill.fillAmount = Mathf.Abs(Mathf.Cos(Time.time));
	}
}
