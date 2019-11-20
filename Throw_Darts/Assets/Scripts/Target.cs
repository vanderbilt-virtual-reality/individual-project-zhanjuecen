using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

	float radian = 0;
	//
	float perRadian = 0.13f;
	//
	float radius = 0.8f;
	Vector3 oldPos;

	// Use this for initialization
	void Start ()
	{
		oldPos = transform.position; //  
	}

	// Update is called once per frame
	void Update ()
	{
		radian += perRadian; //  
		float dy = Mathf.Cos (radian) * radius; // 
		transform.position = oldPos + new Vector3 (0, dy, 0);
	}
}
