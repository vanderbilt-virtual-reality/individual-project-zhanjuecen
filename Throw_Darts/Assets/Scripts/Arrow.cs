using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	string colliderGuy;
	//record the name of collider
	private bool onTarget;
	//record whether the arrow is on the target
	private bool needScored;
	//record whether the arrow has been scored
	public float usedTime;
	//record the time when the arrow is used

	void Start ()
	{
		init ();
	}

	//initial the arrow
	public void init ()
	{
		colliderGuy = null;
		onTarget = false;
		needScored = false;
	}

	public string getColliderGuy ()
	{
		return colliderGuy;
	}

	public void setColliderGuy (string guy)
	{
		colliderGuy = guy;
	}

	public void resetColliderGuy ()
	{
		colliderGuy = null;
	}

	public bool getOnTarget ()
	{
		return onTarget;
	}

	public void setOnTarget ()
	{
		onTarget = true;
	}

	public void resetOnTarget ()
	{
		onTarget = false;
	}

	public bool getNeedScore ()
	{
		return needScored;
	}

	public void setNeedScore ()
	{
		needScored = true;
	}

	public void resetNeedScore ()
	{
		needScored = false;
	}

	public void destroyRigidBody ()
	{
		Destroy (this.GetComponent<Rigidbody> ());
	}

	//the check of collision
	void OnTriggerEnter (Collider other)
	{
		if (this.getColliderGuy () == null) {
			if (onTarget == false) {
				if (other.gameObject.tag.Contains ("circle")) {
					this.setColliderGuy (other.gameObject.name);      //record which circle the arrow on
					Debug.Log (other.gameObject.tag);
					onTarget = true;
					needScored = true;
				} else if (other.gameObject.tag.Equals ("panel")) {
					needScored = true;
				}
			}
			if (onTarget == true) {
				//destroy the component rigidbody of arrow so that it can stop on the target
				Destroy (this.GetComponent<Rigidbody> ());
				gameObject.transform.parent = other.transform.parent;
			}
		}
	}

}
