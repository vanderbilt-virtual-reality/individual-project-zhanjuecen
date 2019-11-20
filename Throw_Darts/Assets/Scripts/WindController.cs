using System;
using UnityEngine;

public class WindController
{
	private int wind;
	private int windRange = 40;
	//the largest wind
	private Vector3 direction = new Vector3 (1, 0, 0);
	//ensure the wind is only on the X asix

	public int getWind ()
	{
		return wind;
	}

	public Vector3 getDirection ()
	{
		return direction;
	}

	public void windChange ()
	{
		wind = UnityEngine.Random.Range (-windRange, windRange);
	}
}

