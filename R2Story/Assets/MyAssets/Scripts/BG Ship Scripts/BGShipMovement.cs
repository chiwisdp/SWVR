using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGShipMovement : MonoBehaviour {

	// Use this for initialization
	public float speed = 100;
	void OnEnable()
	{
		GetComponent<Rigidbody>().AddForce(transform.forward*speed);
		Invoke("Kill", 6f);
	}
	void Kill()
	{
		gameObject.SetActive(false);
		GetComponent<Rigidbody>().velocity =Vector3.zero;
	}
	// Update is called once per frame
	void OnDisable()
	{
		CancelInvoke();
	}
}
