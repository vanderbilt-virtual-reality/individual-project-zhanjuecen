using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Factory
{
	List<GameObject> freeArrow = new List<GameObject> ();
	List<GameObject> usedArrow = new List<GameObject> ();

	int score;

	Vector3 targetPosition = new Vector3 (0, 3, 0);
	Vector3 planePosition = new Vector3 (0, -3, 0); 
	Vector3 lightPosition = new Vector3 (0, 3, -2);

	public void init ()
	{
		GameObject.Instantiate (Resources.Load ("Prefabs/target"), targetPosition, Quaternion.identity);
		GameObject.Instantiate (Resources.Load ("Prefabs/Plane"), planePosition, Quaternion.identity);
		GameObject light = GameObject.Instantiate (Resources.Load ("Prefabs/Directional Light")) as GameObject;
		light.transform.position = lightPosition;

		score = 0;
	} 

	public void sendArrow (Vector3 direction, Vector3 windDirection, Vector3 startArrowPosition, int wind, float forceRatio)
	{
		if (!UserController.instance.IsStart ())
			return;

		GameObject currentArrow;
		if (freeArrow.Count == 0) {
			currentArrow = GameObject.Instantiate (Resources.Load ("Prefabs/arrow")) as GameObject;
		} else {
			currentArrow = freeArrow [0];
			freeArrow.RemoveAt (0);
			currentArrow.AddComponent<Rigidbody> ();
		}
		currentArrow.SetActive (true);
		currentArrow.transform.position = startArrowPosition;
		currentArrow.transform.up = direction;                                                      
		currentArrow.GetComponent<Rigidbody> ().AddForce (direction * 50 * forceRatio, ForceMode.Impulse);     
		currentArrow.GetComponent<Rigidbody> ().AddForce (windDirection * wind, ForceMode.Force);     
		usedArrow.Add (currentArrow);
		currentArrow.GetComponent<Arrow> ().usedTime = Time.time;
	}

	public void recycleCheck ()
	{
		if (usedArrow.Count != 0) {
			float present = Time.time;
			if (present - usedArrow [0].GetComponent<Arrow> ().usedTime >= 5) {
				recycleOneArrow ();
			}
		}
	}

	void recycleOneArrow ()
	{
		GameObject currentArrow = usedArrow [0];
		usedArrow.RemoveAt (0);
		currentArrow.GetComponent<Arrow> ().init ();
		currentArrow.SetActive (false);
		currentArrow.transform.SetParent (null); //remove parent relationship
 
		if (currentArrow.GetComponent<Arrow> ().getOnTarget () == false) {
			currentArrow.GetComponent<Arrow> ().destroyRigidBody ();
		}

		freeArrow.Add (currentArrow);
	}

	public void scoreCheck ()
	{
		for (int i = 0; i < usedArrow.Count; i++) {
			if (usedArrow [i].GetComponent<Arrow> ().getNeedScore ()) {
				String colliderGuy = usedArrow [i].GetComponent<Arrow> ().getColliderGuy ();
				if (colliderGuy == "circle1") {
					score += 10;
				} else if (colliderGuy == "circle2") {
					score += 8;
				} else if (colliderGuy == "circle3") {
					score += 6;
				} else if (colliderGuy == "circle4") {
					score += 4;
				} else if (colliderGuy == "circle5") {
					score += 2;
				} else if (colliderGuy == "circle6") {
					score += 1;
				} else {
					score -= 1;
				}
				usedArrow [i].GetComponent<Arrow> ().resetNeedScore ();

				UIController.instance.setScore (score);
				if (score >= 100) {
					score = 0;
					UIController.instance.ShowEndGameState ("You Win!");
				} else if (score <= -10) {
					score = 0;
					UIController.instance.ShowEndGameState ("You Lost!!");
				}
			}
		}
	}

	public int getScore ()
	{
		return score;
	}
}
