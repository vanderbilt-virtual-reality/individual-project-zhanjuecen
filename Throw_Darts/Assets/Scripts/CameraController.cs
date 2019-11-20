using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public static CameraController instance;
     
	public float sensitivityX = 3F;
	public float sensitivityY = 3F;
 
	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;
	int speed = 10;

	void Awake ()
	{
		instance = this; 
		enabled = false;
	}

	void Update ()
	{ 
		float rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;
 
		rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;  
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
 
		transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);

		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (new Vector3 (0, 0, speed * Time.deltaTime));
		} 
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (new Vector3 (0, 0, -1 * speed * Time.deltaTime));
		} 
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (new Vector3 (-1 * speed * Time.deltaTime, 0, 0));
		} 
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		} 
	}

	void Start ()
	{ 
		if (GetComponent<Rigidbody> ())
			GetComponent<Rigidbody> ().freezeRotation = true;
	}

	public void setEnable (bool enable)
	{
		enabled = enable;
	}
}
