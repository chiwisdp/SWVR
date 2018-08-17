using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGExplosions : MonoBehaviour {

	// Use this for initialization
	float rng_spawnTime;
	float timer;

	void Awake () {
		gameObject.SetActive (false);
		rng_spawnTime = Random.Range (15f, 180f);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > rng_spawnTime) {
			Explode ();
		}
		
	}

	void Explode(){
		rng_spawnTime = Random.Range (15f, 180f);

	}

}
