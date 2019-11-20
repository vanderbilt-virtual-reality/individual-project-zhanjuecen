using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

	Factory myFactory;
	WindController windController; 

	void Start ()
	{ 
		if (myFactory == null) {
			myFactory = new Factory ();
		}
		myFactory.init ();
		if (windController == null) {
			windController = new WindController ();
		}
		windController.windChange ();
	}

	//every time check are there any arrows should be recycled and check the score
	void Update ()
	{
		myFactory.recycleCheck ();
		myFactory.scoreCheck ();
	}

	public void sendArrow (Vector3 direction, float forceRatio)
	{
		if (forceRatio == 0)
			forceRatio = 0.1f;
		Vector3 windDirection = windController.getDirection ();
		int wind = windController.getWind ();
		Debug.Log (wind);
		myFactory.sendArrow (direction, windDirection, transform.position, wind, forceRatio);
		windController.windChange ();        //after sending an arrow,change the wind
	}

	public int getScore ()
	{
		return myFactory.getScore ();
	}

	public int getWind ()
	{
		if (windController == null)
			windController = new WindController ();
		return windController.getWind ();
	}
}
