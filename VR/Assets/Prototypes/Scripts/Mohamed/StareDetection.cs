using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StareDetection : MonoBehaviour {
	private RaycastHit hit;
	private float coolDown,timer;
	public GameObject[] interactibleObjects = new GameObject[30];
	private Vector3 currentDestination,newDestination; 
	public Image LoadingCursorL,LoadingCursorR;
	private int currentIndex;
	//private bool selectedTaken;

	void Start () {
		coolDown = 2.0f;
		timer = 0.0f;
		LoadingCursorL.fillAmount = 0;LoadingCursorR.fillAmount = 0; //selectedTaken = false;
		}


	void Update () {
		
		if (Physics.Raycast(transform.position,transform.forward,out hit, 10.0f)) {
			if ((hit.transform.name == "Destination0") /* && (!selectedTaken)*/)  {
				interactibleObjects[0].GetComponent<InteractibleObject>().selected = true; Camera.main.WorldToScreenPoint(hit.point); // use hit.point to get the reticule pos set it with code
				} 
			else if ((hit.transform.name == "Destination1") /*&& (!selectedTaken)*/) {
				interactibleObjects[1].GetComponent<InteractibleObject>().selected = true;
				}
			else if ((hit.transform.name == "Destination2") /* && (!selectedTaken)*/)  { 
				interactibleObjects[2].GetComponent<InteractibleObject>().selected = true;
				}



				} else { //this else means the ray you're casting got out
			for (int i = 0; (interactibleObjects[i] != null) && (i < interactibleObjects.Length); i++)	{
				interactibleObjects[i].GetComponent<InteractibleObject>().selected = false;}//notice : make sure the value of selected stays for the travel
			if (timer < coolDown) {timer = 0.0f; LoadingCursorL.fillAmount = 0;LoadingCursorR.fillAmount = 0;}
				
				}


		//test if any IO from the list is hit by raycast, then call CoolDown
		for (int i = 0; (interactibleObjects[i] != null) && (i < interactibleObjects.Length); i++){ 
			if (interactibleObjects[i].GetComponent<InteractibleObject>().selected) {
			CoolDown(interactibleObjects[i]); currentIndex = i;}
		 }

		Debug.Log(interactibleObjects[currentIndex].GetComponent<InteractibleObject>().selected);
		//if (selectedTaken) interactibleObjects[currentIndex].GetComponent<InteractibleObject>().selected = true;
	
	}//closing Update()

	public void GoToIO (Vector3 Destination) {
		transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position,Destination, 2*Time.deltaTime);
		if (transform.parent.transform.position == Destination) {timer = 0; LoadingCursorL.fillAmount = 0;LoadingCursorR.fillAmount = 0;/*selectedTaken = false;*/}
	}

	public void CoolDown (GameObject theInteractibleObject) {
		if ((timer < coolDown) && (Vector3.Distance(transform.parent.transform.position,theInteractibleObject.transform.position) > 2)) {
			timer += Time.deltaTime;
			LoadingCursorL.fillAmount += 0.5f * Time.deltaTime;LoadingCursorR.fillAmount += 0.5f * Time.deltaTime;
		} else {//selectedTaken = true;
			newDestination = new Vector3(theInteractibleObject.transform.position.x, theInteractibleObject.transform.position.y, theInteractibleObject.transform.position.z-2);
			GoToIO(newDestination);
			//StartCoroutine(ResetTimer());
			} 
	}

	public IEnumerator ResetTimer() {
		yield return new WaitForSeconds(2f);
		timer = 0.0f;
		LoadingCursorL.fillAmount = 0;LoadingCursorR.fillAmount = 0;
	}

	void OnGUI () {//juste pour l'affichage pour debugage
		GUI.Label(new Rect(10,10,30,20), timer.ToString());
	}
}