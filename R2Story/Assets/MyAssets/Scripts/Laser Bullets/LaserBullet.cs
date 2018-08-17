using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour {

	public float bulletForce = 10000;
	float yVelOffset = 0;
	public void AddYVelocity(float yOffset){
		yVelOffset = yOffset;
		GetComponent<Rigidbody>().AddForce(transform.up*yVelOffset);

	}

	void OnEnable ()
	{
		GetComponent<Rigidbody>().AddForce(transform.forward*bulletForce);

		Invoke("Kill", 10f);
		Debug.Log(this.name+" Awake");
	}

	void Kill()
	{
		gameObject.SetActive(false);
		GetComponent<Rigidbody>().velocity =Vector3.zero;
		Debug.Log(this.name+" Dead");
	}
	void OnCollisionEnter(Collision collision)
	{
		Kill();
	}

	void OnDisable()
	{
		CancelInvoke();
	}
}
